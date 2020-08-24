using System;

namespace Faker
{
    public class Class1
    {
 /* public class BaseFaker<TClass> where TClass : class
    {

       public RandomGenerator Random { get; set; }

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
           //Store rule
       }

       public void GetFrom<TProperty>(
           Expression<Func<TClass, TProperty>> selector,
           Func<IValueProvider<TProperty>, TProperty> setter)
       {
           //Store rule
       }

       public void SetFaker<TInnerClass>(Expression<Func<TClass, TInnerClass>> selector, BaseFaker<TInnerClass> faker) where TInnerClass : class
       {
           //Store to list of known fakers
       }
   }

   public interface IValueProvider<T>
   {
       T GetValue();

       ICollection<T> GetValues();
   }





   public class Storage
   {

       public ValueClass Value { get; set; }

       public int Test { get; set; }
   }

   public class ValueClass
   {
       public int Value { get; set; }
   }

   public class ValueClassFaker : BaseFaker<ValueClass>
   {
       public ValueClassFaker()
       {
           RuleFor(e => e.Value, f => 10);
       }
   }

        IDictionary<PropertyInfo, object> InnerFakers;

        public void SetFaker<TInnerClass>(Expression<Func<TClass, TInnerClass>> selector, BaseFaker<TInnerClass> faker) where TInnerClass : class
        {
            / TInnerClass is stored in PropertyInfo
            InnerFakers.Add(/selector to PropertyInfo/ null, faker);

            /Store to list of known fakers
        }


    public class StorageFaker : BaseFaker<Storage>
   {
       public StorageFaker()
       {
           SetFaker(e => e.Value, new BaseFaker<ValueClass>());
           RuleFor(e => e.Test, f => f.Int());
           //VS
           RuleFor(e => e.Value, _ => new BaseFaker<ValueClass>().Generate());
           RuleFor(e => e.Test, f => f.Int());


           //VS
           RuleFor(e => e.Value, _ => new ValueClass());
           SetFaker(e => e.Value, new BaseFaker<ValueClass>());
       }
   }*/
    }
}
