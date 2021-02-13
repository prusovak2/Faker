using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

/*
    Faker
    Generator of pseudo random contend of C# objects
    Katerina Prusova, sophomore MFF UK
    summer term 2019/2020
    Jazyk C# a platforma .NET (NPRG035), Pokročilé programování pro .NET I (NPRG038)
*/

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
        /// (and Generate method is called automatically) <br/>
        /// default is InnerFakerConstructorUsage.Parameterless
        /// </summary>
        public InnerFakerConstructorUsage CtorUsageFlag { get; protected set; } = InnerFakerConstructorUsage.Parameterless;
        /// <summary>
        /// Parameters for TClass constructor used when CtorUsageFlag = InnerFakerConstructorUsage.GivenParameters 
        /// </summary>
        public object[] CtorParameters { get; set; } = new object[] { };
        /// <summary>
        /// How should the members with no RuleFor or InnerFaker set for them be treated <br/>
        /// default is UnfilledMembers.LeaveBlank
        /// </summary>
        //public UnfilledMembers FillEmptyMembers { get; protected set; } = UnfilledMembers.LeaveBlank;
        /// <summary>
        /// Generate call on a faker calls Generate on all its innerFakers 
        /// </summary>
        internal IDictionary<MemberInfo, IFaker> InnerFakers { get; } = new Dictionary<MemberInfo, IFaker>();
        /// <summary>
        /// rules used to generate pseudo-random content
        /// </summary>
        internal IDictionary<MemberInfo, Func<object>> Rules { get; } = new Dictionary<MemberInfo, Func<object>>();
        /// <summary>
        /// set of members whose content should not be filled by a default random function when UnfilledMember.DefaultRandomFunc is set <br/>
        /// members get inserted here by calling Ignore(member) in Fakers ctor <br/>
        /// once member is IgnoredStrictly, it cannot have a RuleFor or InnnerFaker set for it in the same instance of the Faker
        /// </summary>
        internal HashSet<MemberInfo> IgnoredStrictly { get; } = new HashSet<MemberInfo>();

        internal HashSet<MemberInfo> MembersWithoutFakerIgnoreAttribute { get; set; } = null;

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
        /// <typeparam name="TMember">Type of member to be filled in </typeparam>
        /// <param name="selector">lambda returning member to be filled </param>
        /// <param name="setter">random function to fill in the member </param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to set a RuleFor a member that already has a Rule or InnerFaker set or is Ignored by Ignore method</exception>
        public void RuleFor<TMember>(
            Expression<Func<TClass, TMember>> selector,
            Func<RandomGenerator, TMember> setter)
        {
            MemberInfo memberInfo = this.GetMemberFromExpression(selector);
            _internalRuleFor(memberInfo, setter);
        }


        internal protected virtual void _internalRuleFor<TMember>(MemberInfo memberInfo, Func<RandomGenerator, TMember> setter)
        {
            if (this.InnerFakers.ContainsKey(memberInfo))
            {
                throw new FakerException("You cannot state a RuleFor a member that already has a InnerFaker set for it.");
            }
            if (this.IgnoredStrictly.Contains(memberInfo))
            {
                throw new FakerException("You cannot state a RuleFor a member that was already marked as strictly Ignored by calling Ignore method.");
            }
            try
            {
                //magical line
                this.Rules.Add(memberInfo, () => setter(this.Random));
            }
            catch (ArgumentException)
            {
                throw new FakerException("You cannot state multiple rules for the same member.");
            }
        }
        /// <summary>
        /// Sets member as strictly Ignored - this member won't be filled by default random function when <br/>
        /// FillEmptyMembers is set to UnfilledMembers.DefaultRandomFunc.
        /// </summary>
        /// <typeparam name="TMember">Type of member to be ignored</typeparam>
        /// <param name="selector">lambda returning member to be ignored</param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to Ignore a member that already has a Rule or InnerFaker set for it</exception>
        internal protected virtual void _internalIgnore<TMember>(MemberInfo memberInfo)
        {            
            if (this.Rules.ContainsKey(memberInfo))
            {
                throw new FakerException("You cannot mark a member as strictly Ignored by Ignore method when it already has an RuleFor set for it");
            }
            if (this.InnerFakers.ContainsKey(memberInfo))
            {
                throw new FakerException("You cannot mark a member as strictly Ignored by Ignore method when it already has an InnerFaker set for it");
            }
            //does not throw exception when member info is already present in the HashSet, returns false
            //Ignored method called multiple times with the same member as the parameter won't change the semantics of the program
            //no need to throw the FakerException here
            this.IgnoredStrictly.Add(memberInfo);
        }
        /// <summary>
        /// sets InnerFaker for a member of TInnerClass type
        /// </summary>
        /// <typeparam name="TInnerClass"> type of member to has a faker set for it</typeparam>
        /// <param name="selector"> lambda returning the member </param>
        /// <param name="faker"> Faker to be used to generate contend of the member </param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to SetFaker for a member that already has a Rule or InnerFaker set or is Ignored by Ignore method</exception>
        public void SetFaker<TInnerClass>(Expression<Func<TClass, TInnerClass>> selector,
            BaseFaker<TInnerClass> faker) where TInnerClass : class
        {
            MemberInfo memberInfo = this.GetMemberFromExpression(selector);
            _internalSetFaker(memberInfo, faker);
        }

        /// <summary>
        /// sets InnerFaker for a member of TInnerClass type
        /// </summary>
        /// <typeparam name="TInnerClass"> type of member to has a faker set for it</typeparam>
        /// <param name="selector"> lambda returning the member </param>
        /// <param name="faker"> Faker to be used to generate contend of the member </param>
        /// <exception cref="FakerException">Throws FakerException, when you are trying to SetFaker for a member that already has a Rule or InnerFaker set or is Ignored by Ignore method</exception>
        internal protected virtual void _internalSetFaker<TInnerClass>(MemberInfo memberInfo, 
            BaseFaker<TInnerClass> faker) where TInnerClass : class
        {
            if (this.Rules.ContainsKey(memberInfo))
            {
                throw new FakerException("You cannot set an InnerFaker for a member that already has a Rule for it.");
            }
            if (this.IgnoredStrictly.Contains(memberInfo))
            {
                throw new FakerException("You cannot set an InnerFaker for a member that was already marked as strictly Ignored by calling Ignore method.");
            }
            try
            {
                // Store to list of known fakers
                InnerFakers.Add(memberInfo, faker);
            }
            catch (ArgumentException)
            {
                throw new FakerException("You cannot set multiple InnerFakers for the same member.");
            }
        }
        /// <summary>
        /// Called when faker is used as an innerFaker
        /// </summary>
        /// <returns> generated instance </returns>
        object IFaker.Generate(object instance)
        {
            switch (this.CtorUsageFlag)
            {
                case InnerFakerConstructorUsage.Parameterless:
                    return this.Generate();
                case InnerFakerConstructorUsage.GivenParameters:
                    return this.Generate(this.CtorParameters);
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
            return this._internal_populate(instance);
        }
        internal protected virtual TClass _internal_populate(TClass instance)
        {
            if(instance is null)
            {
                throw new FakerException($"Argument of Populate must be existing instance of {typeof(TClass)} type");
            }
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
            /*if (this.FillEmptyMembers == UnfilledMembers.DefaultRandomFunc)
            {
                this.RandomlyFillRemainingMembers(instance);
            }*/
            return instance;
        }
        /// <summary>
        /// Fill one member with a random contend
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
                // whether instanceInProperty is not null is checked in IFaker.Generate, when necessary
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
        /// returns HashSet of all fields and properties of TClass that does not have RuleFor or InnerFaker set in this Faker
        /// </summary>
        /// <returns></returns>
        /*internal HashSet<MemberInfo> GetSetOfMembersToBeFilledByDefaultRandFunc()
        {
            if(MembersWithoutFakerIgnoreAttribute is null)
            {
                Type type = typeof(TClass);
                MembersWithoutFakerIgnoreAttribute = type.GetMembers().Where(memberInfo => ((memberInfo is PropertyInfo || memberInfo is FieldInfo) && memberInfo.GetCustomAttributes<FakerIgnoreAttribute>().Count() == 0)).ToHashSet();
            }
            HashSet<MemberInfo> memberInfos = MembersWithoutFakerIgnoreAttribute;
            HashSet<MemberInfo> HasRulefor = this.Rules.Keys.ToHashSet();
            HashSet<MemberInfo> HasSetFaker = this.InnerFakers.Keys.ToHashSet();
            memberInfos.ExceptWith(HasRulefor);
            memberInfos.ExceptWith(HasSetFaker);
            memberInfos.ExceptWith(this.IgnoredStrictly);
            
            return memberInfos;
        }*/

        internal void InitializeListOfRandomlyFilledMembers()
        {
            if(this.MembersWithoutFakerIgnoreAttribute is null)
            {
                Type type = typeof(TClass);
                MembersWithoutFakerIgnoreAttribute = type.GetMembers().Where(memberInfo => ((memberInfo is PropertyInfo || memberInfo is FieldInfo) && !memberInfo.GetCustomAttributes<FakerIgnoreAttribute>().Any())).ToHashSet();
            }
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

    /// <summary>
    /// Simple Faker that RESPECTS FAKER IGNORE ATTRIBUTES assigned to the members of a TClass instance <br/>
    /// and all members of TClass of basic types with no RuleFor set for them fills by calling a default random function 
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    public class AutoFaker<TClass> : BaseFaker<TClass>, IFaker where TClass : class
    {
        /// <summary>
        /// new instance of AutoFaker that creates a new instance of the RandomGenerator and produces its seed automatically <br/>
        /// RESPECTS FAKER IGNORE ATTRIBUTES <br/>
        /// fills all members of TClass of basic types with no RuleFor set for them by default random function for particular type
        /// </summary>
        public AutoFaker() : base()
        {
            InitializeListOfRandomlyFilledMembers();
        }

        /// <summary>
        /// new instance of AutoFaker that creates a new instance of RandomGenerator with a given seed <br/>
        /// RESPECTS FAKER IGNORE ATTRIBUTES
        /// fills all members of TClass of basic types with no RuleFor set for them by default random function for particular type
        /// </summary>
        /// <param name="seed"></param>
        public AutoFaker(ulong seed) : base(seed)
        {
            InitializeListOfRandomlyFilledMembers();
        }

        /// <summary>
        /// new instance of AutoFaker that uses existing instance of RandomGenerator <br/>
        /// one instance of random generator can be shared by multiple fakers to save memory <br/>
        /// recommended for innerFakers <br/>
        /// RESPECTS FAKER IGNORE ATTRIBUTES <br/>
        /// this  faker fills all members of TClass of basic types with no RuleFor set for them by default random function for particular type
        /// </summary>
        /// <param name="randomGenerator"></param>
        public AutoFaker(RandomGenerator randomGenerator) : base(randomGenerator)
        {
            InitializeListOfRandomlyFilledMembers();
        }

        /// <summary>
        /// creates AutoFaker for given type that uses default random functions to fill members of basic types <br/>
        /// and recursively creates and set similar AutoFakers for members of user defined class types
        /// created Faker respects FakerIgnore attributes
        /// All user defined types appearing as member in hierarchy of TClass must have public parameterless ctor 
        /// </summary>
        /// <returns> a newly created AutoFaker </returns>
        public static AutoFaker<TClass> CreateAutoFaker()
        {
            return AutoFakerCreator.CreateAutoFaker<TClass>();
        }

        protected internal override TClass _internal_populate(TClass instance)
        {
            TClass PopulatedInstance = base._internal_populate(instance);
            RandomlyFillRemainingMembers(PopulatedInstance);
            return PopulatedInstance;
        }

        public new TClass Populate(TClass instance)
        {
            return this._internal_populate(instance);
        }

        protected internal override void _internalRuleFor<TMember>(MemberInfo memberInfo, Func<RandomGenerator, TMember> setter)
        {
            base._internalRuleFor(memberInfo, setter);
            MembersWithoutFakerIgnoreAttribute.Remove(memberInfo);
        }

        public new void RuleFor<TMember>(
            Expression<Func<TClass, TMember>> selector,
            Func<RandomGenerator, TMember> setter)
        {
            MemberInfo memberInfo = this.GetMemberFromExpression(selector);
            this._internalRuleFor(memberInfo, setter);
        }

        protected internal override void _internalSetFaker<TInnerClass>(MemberInfo memberInfo, BaseFaker<TInnerClass> faker)
        {
            base._internalSetFaker(memberInfo, faker);
            MembersWithoutFakerIgnoreAttribute.Remove(memberInfo);
        }

        public new void SetFaker<TInnerClass>(Expression<Func<TClass, TInnerClass>> selector,
            BaseFaker<TInnerClass> faker) where TInnerClass : class
        {
            MemberInfo memberInfo = this.GetMemberFromExpression(selector);
            this._internalSetFaker(memberInfo, faker);
        }

        public void Ignore<TMember>(Expression<Func<TClass, TMember>> selector)
        {
            MemberInfo memberInfo = this.GetMemberFromExpression(selector);
            MembersWithoutFakerIgnoreAttribute.Remove(memberInfo);
            base._internalIgnore<TMember>(memberInfo);
        }

        /// <summary>
        /// Fills members with no RuleFor or InnerFaker set for them with random values provided by default random functions corresponding to member types
        /// </summary>
        /// <param name="instance"></param>
        internal void RandomlyFillRemainingMembers(TClass instance)
        {
            HashSet<MemberInfo> membersToFill = this.MembersWithoutFakerIgnoreAttribute;
            foreach (var member in membersToFill)
            {
                if (member is PropertyInfo propertyInfo)
                {
                    Type propertyType = propertyInfo.PropertyType;
                    var sampleInstance = propertyType.GetSampleInstance(); // get sample instance of type to be used in GetDefaultRandomFuncForType()
                    var fillingFunc = this.Random.GetDefaultRandomFuncForType(sampleInstance);
                    if (fillingFunc is null)
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
    }

    internal class AutoFakerCreator
    {
        /// <summary>
        /// creates AutoFaker for given type that uses default random functions to fill members of basic types <br/>
        /// and recursively creates and set similar AutoFakers for members of user defined class types
        /// </summary>
        /// <returns> a newly created AutoFaker </returns>
        public static AutoFaker<TMember> CreateAutoFaker<TMember>() where TMember : class
        {
            AutoFaker<TMember> faker = new AutoFaker<TMember>();
            Type type = typeof(TMember);
            List<MemberInfo> memberInfos = type.GetMembers().Where(memberInfo => ((memberInfo is PropertyInfo || memberInfo is FieldInfo) && (IsUserDefinedClassType(memberInfo)) && (memberInfo.GetCustomAttributes<FakerIgnoreAttribute>().Count() == 0))).ToList();
            foreach (var memberInfo in memberInfos)
            {
                if(faker.IgnoredStrictly.Contains(memberInfo))
                {
                    continue;
                }
                Type memberType = GetTypeFromMemberInfo(memberInfo);
                var memberAutoFaker = typeof(AutoFakerCreator).GetMethod("CreateAutoFaker").MakeGenericMethod(memberType).Invoke(null, null);
                faker.InnerFakers.Add(memberInfo, (IFaker)memberAutoFaker);
            }
            return faker;
        }
        /// <summary>
        /// if member is property or field, returns a type of corresponding property or field
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        internal static Type GetTypeFromMemberInfo(MemberInfo memberInfo)
        {
            if (memberInfo is PropertyInfo propertyInfo)
            {
                return propertyInfo.PropertyType;
            }
            else if (memberInfo is FieldInfo fieldInfo)
            {
                return fieldInfo.FieldType;
            }
            else
            {
                throw new NotImplementedException("This method is expected to be used only for properties and fields");
            }
        }
        /// <summary>
        /// determines whether the member has type that for which an inner AutoFaker should be generated while creating AutoFaker via CreateAutoFaker method 
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        internal static bool IsUserDefinedClassType(MemberInfo memberInfo)
        {
            Type memberType = GetTypeFromMemberInfo(memberInfo);
            if (typeof(Delegate).IsAssignableFrom(memberType) || typeof(IEnumerable).IsAssignableFrom(memberType))
            {
                return false;
            }
            if(memberType == typeof(string))
            {
                return false;
            }
            
            return memberType.IsClass;
        }
    }
}
