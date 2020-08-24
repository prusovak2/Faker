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
        IDictionary<PropertyInfo, PropertyStorage> Rules;

        public BaseFaker()
        {
            this.Random = new RandomGenerator();
        }

        public TClass Generate()
        {
            // Use rules
            return default;
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
            PropertyStorage<TProperty> storage = new PropertyStorage<TProperty>(setter);
            //Func<RandomGenerator, object> a = (Func<RandomGenerator, object>)setter;
            this.Rules.Add(propertyInfo, storage);
        }
        private void GenerateProperty(TClass instance, PropertyInfo propertyInfo, PropertyStorage storage)
        {
            Type propertyType = propertyInfo.PropertyType;
            //var v = Convert.ChangeType(storage,Pro)
            //var s = setter();
        }
        private class PropertyStorage { }
        private class PropertyStorage<TProperty>: PropertyStorage
        {
            Func<RandomGenerator, TProperty> setterFunc;
            public PropertyStorage(Func<RandomGenerator,TProperty> setter)
            {
                this.setterFunc = setter;
            }
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
    //public delegate void Setter<TProperty>(Func<RandomGenerator, TProperty> setter);
}
