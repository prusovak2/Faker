using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Faker
{
    public class BaseFaker<TClass> where TClass : class
    {
        public RandomGenerator Random { get; }

        IDictionary<PropertyInfo, object> InnerFakers;
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
            // Use rules
            //TODO:GETCONSTRUCTORS????
            Type type = typeof(TClass);
            ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
            TClass instance = (TClass)ctor.Invoke(null);
            
            foreach (var item in this.Rules)
            {
                this.GenerateProperty(instance, item.Key, item.Value);
            }
            return instance;
        }
        public TClass Generate(Func<TClass> ctor)
        {
            TClass instance = (TClass)ctor();

            foreach (var item in this.Rules)
            {
                this.GenerateProperty(instance, item.Key, item.Value);
            }
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
            this.Rules.Add(propertyInfo, () => setter(this.Random));
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
                //var UnExp = (UnaryExpression)GetPropertyLambda.Body;
                if (unaryExpression.Operand is MemberExpression)
                {
                    expression = (MemberExpression)unaryExpression.Operand;
                }
                else
                    throw new ArgumentException();
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
