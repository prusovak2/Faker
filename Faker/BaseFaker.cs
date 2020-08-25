using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Faker
{
    public interface IFaker
    {
        public object Generate(object instance);
    }
    public class BaseFaker<TClass>: IFaker where TClass : class 
    {
        public RandomGenerator Random { get; }
        public CtorToUseWhenUsedAsInnerFaker CtorFlag { get; protected set; } = CtorToUseWhenUsedAsInnerFaker.Parameterless;
        public object[] CtorParametrs { get; set; } = new object[] { };

        IDictionary<MemberInfo, IFaker> InnerFakers = new Dictionary<MemberInfo, IFaker>();
        IDictionary<MemberInfo, Func<object>> Rules = new Dictionary<MemberInfo, Func<object>>();

        public BaseFaker()
        {
            this.Random = new RandomGenerator();
        }
        public BaseFaker(ulong seed)
        {
            this.Random = new RandomGenerator(seed);
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
                this.Rules.Add(memberInfo, () => setter(this.Random));
            }
            catch (ArgumentException)
            {
                throw new FakerException("You have stated multiple rules for the same member.");
            }
        }
        public void SetFaker<TInnerClass>(Expression<Func<TClass, TInnerClass>> selector, 
            BaseFaker<TInnerClass> faker) where TInnerClass : class
        {
            //TODO: FakerExceptions
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
            //TODO: ADJUST TO OTHER CONSTRUCTORS
            switch (this.CtorFlag)
            {
                case CtorToUseWhenUsedAsInnerFaker.Parameterless:
                    return this.Generate();
                case CtorToUseWhenUsedAsInnerFaker.GivenParameters:
                    return this.Generate(this.CtorParametrs);
                case CtorToUseWhenUsedAsInnerFaker.PopulateExistingInstance:
                    return this.Populate((TClass)instance);
                default:
                    throw new NotImplementedException();
            }
          
        }



        /// <summary>
        /// Creates new instance of TClass using parameterless constructor, generates a random content base on RulesFor stated for this Faker
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

            TClass instance = (TClass)ctor.Invoke(null);

            // Use rules
            instance = this.Populate(instance);
            return instance;
        }
        /// <summary>
        /// Creates new instance of TClass using constructor with corresponding param. types, generates a random content base on RulesFor stated for this Faker
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
            foreach (var innerFaker in this.InnerFakers)
            {
                this.UseInnerFaker(instance, innerFaker.Key, innerFaker.Value);
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
        internal void UseInnerFaker(TClass instance, MemberInfo memberInfo, IFaker faker)
        {
            if(memberInfo is PropertyInfo propertyInfo)
            {
                Type propertyType = propertyInfo.PropertyType;
                //TODO: other constructor type/only populate? flag?

                var instanceInProperty =  propertyInfo.GetValue(instance);
                var o = faker.Generate(instanceInProperty);
                
                var innerClass = Convert.ChangeType(o, propertyType);
                propertyInfo.SetValue(instance, innerClass);
            }
            if (memberInfo is FieldInfo fieldInfo)
            {
                Type propertyType = fieldInfo.FieldType;
                //TODO: other constructor type/only populate? flag?

                var instanceInField = fieldInfo.GetValue(instance);
                var o = faker.Generate(instanceInField);

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


        /*public void GetFrom<TProperty>(
             Expression<Func<TClass, TProperty>> selector,
             Func<IValueProvider<TProperty>, TProperty> setter)
         {
             //Store rule
         }*/

        /* private PropertyInfo GetPropertyFromExpression<TProperty>(Expression<Func<TClass, TProperty>> GetPropertyLambda)
         {
             MemberExpression expression;

             //this line is necessary, because sometimes the expression comes in as Convert(originalexpression)
             if (GetPropertyLambda.Body is UnaryExpression unaryExpression)
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
             else if (GetPropertyLambda.Body is MemberExpression)
             {
                 expression = (MemberExpression)GetPropertyLambda.Body;
             }
             else
             {
                 throw new ArgumentException();
             }

             return (PropertyInfo)expression.Member;
         }*/
        public enum CtorToUseWhenUsedAsInnerFaker
        {
            Parameterless,
            GivenParameters,
            PopulateExistingInstance,
        }
    }

}
