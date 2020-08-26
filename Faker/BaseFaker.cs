using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Faker
{
    internal interface IFaker
    {
        internal object Generate(object instance);
    }
    public class BaseFaker<TClass> : IFaker where TClass : class
    {
        /// <summary>
        /// source of pseudo-random entities
        /// </summary>
        public RandomGenerator Random { get; }
        /// <summary>
        /// ctor to be used to create an instance of TClass when faker is used as inner faker <br/>
        /// (and Generate method is called automatically)
        /// </summary>
        public InnerFakerConstructorUsage CtorUsageFlag { get; protected set; } = InnerFakerConstructorUsage.Parameterless;
        /// <summary>
        /// Parameters for TClass constructor used when CtorUsageFlag = InnerFakerConstructorUsage.GivenParameters
        /// </summary>
        public object[] CtorParametrs { get; set; } = new object[] { };
        /// <summary>
        /// How should the members with no RuleFor or InnerFaker set for them be treated
        /// </summary>
        public UnfilledMembers FillEmptyMembers { get; protected set; } = UnfilledMembers.LeaveBlank;
        /// <summary>
        /// Generate call on a faker calls Generate on all its innerFakers 
        /// </summary>
        internal IDictionary<MemberInfo, IFaker> InnerFakers { get; } = new Dictionary<MemberInfo, IFaker>(); 
        /// <summary>
        /// rules used to generate pseudo-random content
        /// </summary>
        internal IDictionary<MemberInfo, Func<object>> Rules { get; } = new Dictionary<MemberInfo, Func<object>>();
        /// <summary>
        /// new instance of BaseFaker that creates a new instance of the RandomGenerator and produces its seed automatically
        /// </summary>
        public BaseFaker()
        {
            this.Random = new RandomGenerator();
        }
        /// <summary>
        /// new instance of BaseFaker that creates a new instance of RandomGenerator with a given seed
        /// </summary>
        /// <param name="seed"></param>
        public BaseFaker(ulong seed)
        {
            this.Random = new RandomGenerator(seed);
        }
        /// <summary>
        /// new instance of BaseFaker that uses existing instance of RandomGenerator <br/>
        /// one instance of random generator can be shared by multiple fakers to save memory <br/>
        /// recommended for innerFakers 
        /// </summary>
        /// <param name="randomGenerator"></param>
        public BaseFaker(RandomGenerator randomGenerator)
        {
            this.Random = randomGenerator;
        }
        /// <summary>
        /// Adds Rule for how to generate a random content of particular member <br/>
        /// selector and setter must have the same return type
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="selector"></param>
        /// <param name="setter"></param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to set a RuleFor a member that already has a Rule or InnerFaker set</exception>
        public void RuleFor<TMember>(
            Expression<Func<TClass, TMember>> selector,
            Func<RandomGenerator, TMember> setter)
        {
            MemberInfo memberInfo = this.GetMemberFromExpression(selector);
            if (this.InnerFakers.ContainsKey(memberInfo))
            {
                throw new FakerException("You stated a RuleFor a member that already has a InnerFaker set for it.");
            }
            try
            {
                //magical line
                this.Rules.Add(memberInfo, () => setter(this.Random));
            }
            catch (ArgumentException)
            {
                throw new FakerException("You have stated multiple rules for the same member.");
            }
        }
        /// <summary>
        /// sets InnerFaker for a member of TInnerClass type
        /// </summary>
        /// <typeparam name="TInnerClass"></typeparam>
        /// <param name="selector"></param>
        /// <param name="faker"></param>
        /// /// <exception cref="FakerException">Throws FakerException, when you are trying to SetFaker for a member that already has a Rule or InnerFaker set</exception>
        public void SetFaker<TInnerClass>(Expression<Func<TClass, TInnerClass>> selector, 
            BaseFaker<TInnerClass> faker) where TInnerClass : class
        {
            MemberInfo memberInfo = this.GetMemberFromExpression(selector);
            if (this.Rules.ContainsKey(memberInfo))
            {
                throw new FakerException("You tried to SetFaker for a member that already has a Rule for it.");
            }
            try
            {
                // Store to list of known fakers
                InnerFakers.Add(memberInfo, faker);
            }
            catch (ArgumentException)
            {
                throw new FakerException("You tried to set multiple InnerFakers for the same member.");
            }
        }
        /// <summary>
        /// Called when faker is used as an innerFaker
        /// </summary>
        /// <returns></returns>
        object IFaker.Generate(object instance)
        {
            switch (this.CtorUsageFlag)
            {
                case InnerFakerConstructorUsage.Parameterless:
                    return this.Generate();
                case InnerFakerConstructorUsage.GivenParameters:
                    return this.Generate(this.CtorParametrs);
                case InnerFakerConstructorUsage.PopulateExistingInstance:
                    if(instance is null)
                    {
                        throw new FakerException("InnerFaker set to PopulateExisting instance can only be used to fill member, that is already initialized by instance of particular type.");
                    }
                    return this.Populate((TClass)instance);
                default:
                    throw new NotImplementedException();
            }
        }
        /// <summary>
        /// Creates new instance of TClass using parameterless constructor, generates a random content base on RulesFor and InnerFakers stated for this Faker<br/>
        /// it is recommended to created instance by ctor and than call Populate() on existing instance - avoids using reflexion to create an instance
        /// </summary>
        /// <returns>New TClass instance with random content</returns>
        /// <exception cref="ArgumentException">Throws ArgumentException, when TClass does not have a parameterless constructor</exception>
        public TClass Generate()
        {
            Type type = typeof(TClass);
            ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
            if(ctor is null)
            {
                throw new ArgumentException("Your class does not have a parameterless constructor, use other overload of generate");
            }
            //call constructor
            TClass instance = (TClass)ctor.Invoke(null);

            // Use rules
            instance = this.Populate(instance);
            return instance;
        }
        /// <summary>
        /// Creates new instance of TClass using constructor with corresponding param. types, generates a random content base on RulesFor and InnerFakers stated for this Faker<br/>
        /// it is recommended to created instance by ctor and than call Populate() on existing instance - avoids using reflexion to create an instance
        /// </summary>
        /// <returns>New TClass instance with random content</returns>
        /// <exception cref="ArgumentException">Throws ArgumentException, when TClass does not have a corresponding constructor</exception>
        public TClass Generate(object[] CtorParams)
        {
            //get array of param. types
            Type[] paramTypes = new Type[CtorParams.Length];
            for (int i = 0; i < CtorParams.Length; i++)
            {
                paramTypes[i] = CtorParams[i].GetType();
            }
            Type type = typeof(TClass);
            //check whether the class has corresponding constructor 
            ConstructorInfo ctor = type.GetConstructor(paramTypes);
            if (ctor is null)
            {
                throw new ArgumentException("Your class does not have a constructor with corresponding parameters");
            }
            TClass instance = (TClass)ctor.Invoke(CtorParams);
            //apply rules
            instance = Populate(instance);
            return instance;
        }
        /// <summary>
        /// Use rules to fill the instance with a random content
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>The same instance filled with a random content based on rules</returns>
        public TClass Populate(TClass instance)
        {
            // Use rules
            foreach (var rule in this.Rules)
            {
                this.UseRule(instance, rule.Key, rule.Value);
            }
            // Use InnerFakers
            foreach (var innerFaker in this.InnerFakers)
            {
                this.UseInnerFaker(instance, innerFaker.Key, innerFaker.Value);
            }
            //if required, fill remaining members with default random functions corresponding to their types
            if (this.FillEmptyMembers == UnfilledMembers.DefaultRandomFunc)
            {
                this.RandomlyFillRemainingMembers(instance);
            }
            return instance;
        }
        /// <summary>
        /// Fill one member with a random content
        /// </summary>
        /// <param name="instance">instance, whose member is to be filled</param>
        /// <param name="MemberInfo">info about member to be filled</param>
        /// <param name="setter">Function, that generates a content to be used as the member value</param>
        internal void UseRule(TClass instance, MemberInfo MemberInfo, Func<object> setter)
        {
            //member is a property
            if(MemberInfo is PropertyInfo propertyInfo)
            {
                Type propertyType = propertyInfo.PropertyType;
                var o = setter();
                var value = Convert.ChangeType(o, propertyType);
                propertyInfo.SetValue(instance, value);
            }
            //member is a field
            else if(MemberInfo is FieldInfo fieldInfo)
            {
                Type fieldType = fieldInfo.FieldType;
                var o = setter();
                var value = Convert.ChangeType(o, fieldType);
                fieldInfo.SetValue(instance, value);
            }
            else
            {
                throw new ArgumentException("Member is not a property nor a field.");
            }
        }
        /// <summary>
        /// Fill one member using InnerFaker
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="memberInfo"></param>
        /// <param name="innerFaker"></param>
        internal void UseInnerFaker(TClass instance, MemberInfo memberInfo, IFaker innerFaker)
        {
            if(memberInfo is PropertyInfo propertyInfo)
            {
                Type propertyType = propertyInfo.PropertyType;
                var instanceInProperty =  propertyInfo.GetValue(instance);
                // null is valid value of instanceInProperty when innerFaker does not have CtorUsageFlag set to PopulateExistingInstance
                // whether instanceInProperty is not null is checked if IFaker.Generate, when necessary
                var o = innerFaker.Generate(instanceInProperty);
                var innerClass = Convert.ChangeType(o, propertyType);
                propertyInfo.SetValue(instance, innerClass);
            }
            if (memberInfo is FieldInfo fieldInfo)
            {
                Type propertyType = fieldInfo.FieldType;
                var instanceInField = fieldInfo.GetValue(instance);
                // null is valid value of instanceInProperty when innerFaker does not have CtorUsageFlag set to PopulateExistingInstance
                // whether instanceInProperty is not null is checked if IFaker.Generate, when necessary
                var o = innerFaker.Generate(instanceInField);
                var innerClass = Convert.ChangeType(o, propertyType);
                fieldInfo.SetValue(instance, innerClass);
            }
        }
        /// <summary>
        /// Gets MemberInfo about a member that GetMemberLambda returns
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="GetMemberLambda"></param>
        /// <returns></returns>
        internal MemberInfo GetMemberFromExpression<TMember>(Expression<Func<TClass, TMember>> GetMemberLambda)
        {
            MemberExpression expression;

            //this line is necessary, because sometimes the expression comes in as Convert(originalexpression)
            if (GetMemberLambda.Body is UnaryExpression unaryExpression)
            {
                if (unaryExpression.Operand is MemberExpression)
                {
                    expression = (MemberExpression)unaryExpression.Operand;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else if (GetMemberLambda.Body is MemberExpression)
            {
                expression = (MemberExpression)GetMemberLambda.Body;
            }
            else
            {
                throw new ArgumentException();
            }

            return (MemberInfo)expression.Member;
        }
        /// <summary>
        /// Fills members with no RuleFor or InnerFaker set for them with random values provided by default random functions corresponding to member types
        /// </summary>
        /// <param name="instance"></param>
        internal void RandomlyFillRemainingMembers(TClass instance)
        {
            HashSet<MemberInfo> membersToFill = this.GetSetOfMembersWithNoRuleOrFaker();
            foreach (var member in membersToFill)
            {
                if(member is PropertyInfo propertyInfo)
                {
                    Type propertyType = propertyInfo.PropertyType;
                    var sampleInstance = propertyType.GetSampleInstance(); // get sample instance of type to be used in GetDefaultRandomFuncForType()
                    var fillingFunc = this.Random.GetDefaultRandomFuncForType(sampleInstance);
                    if(fillingFunc is null)
                    {
                        //no Default Random Func for this type of property (property is not of supported basic type)
                        continue;
                    }
                    var o = fillingFunc();
                    var value = Convert.ChangeType(o, propertyType);
                    propertyInfo.SetValue(instance, value);
                }
                if (member is FieldInfo fieldInfo)
                {
                    Type fieldType = fieldInfo.FieldType;
                    var sampleInstance = fieldType.GetSampleInstance();
                    var fillingFunc = this.Random.GetDefaultRandomFuncForType(sampleInstance);
                    if (fillingFunc is null)
                    {
                        //no Default Random Func for this type of field (field is not of supported basic type)
                        continue;
                    }
                    var o = fillingFunc();
                    var value = Convert.ChangeType(o, fieldType);
                    fieldInfo.SetValue(instance, value);
                }
            }
        }
        /// <summary>
        /// returns HashSet of all fields and properties of TClass that does not have RuleFor or InnerFaker set in this Faker
        /// </summary>
        /// <returns></returns>
        internal HashSet<MemberInfo> GetSetOfMembersWithNoRuleOrFaker()
        {
            Type type = typeof(TClass);
            HashSet < MemberInfo > memberInfos = type.GetMembers().Where(memberInfo => (memberInfo is PropertyInfo || memberInfo is FieldInfo)).ToHashSet();

            HashSet<MemberInfo> HasRulefor = this.Rules.Keys.ToHashSet();
            HashSet<MemberInfo> HasSetFaker = this.InnerFakers.Keys.ToHashSet();
            memberInfos.ExceptWith(HasRulefor);
            memberInfos.ExceptWith(HasSetFaker);

            return memberInfos;
        }
        /// <summary>
        /// which ctor should be used to create instances of TClass when faker is used as inner faker
        /// </summary>
        public enum InnerFakerConstructorUsage
        {
            Parameterless,
            GivenParameters,
            PopulateExistingInstance
        }
        /// <summary>
        /// should members with no rule or innerFaker set for them be filled by default random function corresponding to their type?
        /// </summary>
        public enum UnfilledMembers
        {
            LeaveBlank,
            DefaultRandomFunc
        }
    }

}
