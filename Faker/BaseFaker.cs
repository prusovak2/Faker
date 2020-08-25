using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Faker
{
    public class BaseFaker<TClass> where TClass : class
    {
        public RandomGenerator Random { get; }

        IDictionary<PropertyInfo, object> InnerFakers = new Dictionary<PropertyInfo, object>();
        IDictionary<PropertyInfo, Func<object>> Rules = new Dictionary<PropertyInfo, Func<object>>();

        public BaseFaker()
        {
            this.Random = new RandomGenerator();
        }
        public BaseFaker(ulong seed)
        {
            this.Random = new RandomGenerator(seed);
        }

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
        public TClass Populate(TClass instance)
        {
            // Use rules
            foreach (var item in this.Rules)
            {
                this.GenerateProperty(instance, item.Key, item.Value);
            }
            return instance;
        }
        public TClass Generate(object[] CtorParams)
        {
            Type[] paramTypes = new Type[CtorParams.Length];
            for (int i = 0; i < CtorParams.Length; i++)
            {
                paramTypes[i] = CtorParams[i].GetType();
            }
            Type type = typeof(TClass);
            ConstructorInfo ctor = type.GetConstructor(paramTypes);
            if (ctor is null)
            {
                throw new ArgumentException("Your class does not have a constructor with corresponding parameters");
            }
            TClass instance = (TClass)ctor.Invoke(CtorParams);

            instance = Populate(instance);
            return instance;
        }

        /// <summary>
        /// Add rule of generation
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="selector"></param>
        /// <param name="setter"></param>
        public void RuleFor<TProperty>(
            Expression<Func<TClass, TProperty>> selector,
            Func<RandomGenerator, TProperty> setter)
        {
            PropertyInfo propertyInfo = this.GetPropertyFromExpression(selector);
            if (this.InnerFakers.ContainsKey(propertyInfo))
            {
                throw new FakerException("You stated a RuleFor a property that already has a InnerFaker set for it.");
            }
            try
            {
                this.Rules.Add(propertyInfo, () => setter(this.Random));
            }
            catch (ArgumentException)
            {
                throw new FakerException("You have stated multiple rules for the same property.");
            }
        }

        private void GenerateProperty(TClass instance, PropertyInfo propertyInfo, Func<object> setter)
        {
            Type propertyType = propertyInfo.PropertyType;
            var o = setter();
            var value = Convert.ChangeType(o, propertyType);
            propertyInfo.SetValue(instance, value);
        }

        private PropertyInfo GetPropertyFromExpression<TProperty>(Expression<Func<TClass, TProperty>> GetPropertyLambda)
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
        }



        public void SetFaker<TInnerClass>(Expression<Func<TClass, TInnerClass>> selector, BaseFaker<TInnerClass> faker) where TInnerClass : class
        {
            // TInnerClass is stored in PropertyInfo
            InnerFakers.Add(/* selector to PropertyInfo */ null, faker);

            // Store to list of known fakers
        }

        /*public void GetFrom<TProperty>(
             Expression<Func<TClass, TProperty>> selector,
             Func<IValueProvider<TProperty>, TProperty> setter)
         {
             //Store rule
         }*/
    }

}
