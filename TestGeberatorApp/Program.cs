using System;
using System.Linq.Expressions;

namespace TestApp
{

    public partial class MyPartialClass
    {
        public partial void Method();
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MyPartialClass p = new();
            NotFaker nf = new();
            nf.RuleFor(1, 2);
            Faker f = new Faker();
            f.RuleFor(x => x.value, rg => rg.Int());
            SecondFaker sf = new();
            sf.RuleFor(x => x.value, rg => rg.Int());
            sf.RuleFor(x => x.prop, rg => rg.Int());

            NotRuleFor<SomeClass> nrf = new();
            nrf.Rule(x => x.prop, rg => rg.Int());

            p.Method();
        }
    }

 

    public class RandomGenerator
    {
        public int Int()
        {
            return 42;
        }
    }

    public class BaseFaker<TClass>
    {
        

        public void RuleFor<TMember>(
            Expression<Func<TClass, TMember>> selector,
            Func<RandomGenerator, TMember> setter)
        { }
    }

    public class NotRuleFor<TClass>
    {
        public void Rule<TMember>(
            Expression<Func<TClass, TMember>> selector,
            Func<RandomGenerator, TMember> setter)
        { }
    }

    public class SomeClass
    {
        public int value;
        public int prop { get; set; }
    }

    public class SecondFaker : BaseFaker<SomeClass> { }

    public class Faker : BaseFaker<SomeClass>
    {
        public Faker()
        {

        }
    }

    public class NotFaker
    {
        public void RuleFor(int a, int b) { }

    }
}
