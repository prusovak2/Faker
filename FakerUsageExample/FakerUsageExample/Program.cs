using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Faker;

namespace FakerUsageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Attempt(col.blue, col.green, col.red);
            Examples.BasicPersonFakerExample();
            Examples.RandomGeneratorBasicExample();
            Examples.SimplePersonFakerExample();
            Examples.StorageFakerExample();
            Examples.IntOverloadsExample();
            Examples.RandomEnumerableExample();
            Examples.StringExample();
           
        }

        static void Attempt(params col[] p)
        {
            foreach (var item in p)
            {
                Console.WriteLine(item);
            }
        }
    }
    public class Person
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public bool IsAwesome { get; set; }
        public override string ToString()
        {
            return $"Person: Id:{this.Id}, Age:{this.Age}, Name:{this.Name}, IsAwesome:{this.IsAwesome}";
        }
    }
    public class BasicPersonFaker: BaseFaker<Person>
    {
        public BasicPersonFaker()
        {
            RuleFor(person => person.Id, rg => rg.Random.Guid());
            RuleFor(p => p.Age, rg => rg.Random.Int(0, 100));
            RuleFor(p => p.Name, rg => rg.String.Letters(30));
            RuleFor(p => p.IsAwesome, rg => rg.Random.Bool());
        }
    }
    public class Storage
    {
        public ValueClass Value { get; set; }
        public int Property { get; set; }
        public double Field;
        public override string ToString()
        {
            return $"Storage: {this.Value}, Property:{this.Property}, Field:{this.Field}";
        }
    }

    public class ValueClass
    {
        public int Number { get; set; }
        public byte SmallerNumber { get; set; }
        public override string ToString()
        {
            return $"Value: Number:{this.Number}, SmallerNumber:{this.SmallerNumber}";
        }
    }
    public class ValueClassFaker: BaseFaker<ValueClass>
    {
        public ValueClassFaker()
        {
            RuleFor(val => val.Number, rg => rg.Random.IntEven());
            RuleFor(val => val.SmallerNumber, rg => rg.Random.Byte());
        }
    }
    public class StorageFaker: BaseFaker<Storage>
    {
        public StorageFaker()
        {
            SetFaker(s => s.Value, new ValueClassFaker());
            RuleFor(s => s.Property, rg => rg.Random.Int(upper:42));
            RuleFor(s => s.Field, rg => rg.Random.Double());
        }
    }
   /* public class FlawedStorageFaker: BaseFaker<Storage>
    {
        public FlawedStorageFaker()
        {
            //won't compile
            //trying to fill the int property by a string value
            RuleFor(s => s.Property, rg => rg.String.String());
            //trying to set faker specialized on another type than is the type of the property
            SetFaker(s => s.Value, new BasicPersonFaker());
        }
    }*/
   public class SimplePersonFaker : BaseFaker<Person>
    {
        public SimplePersonFaker()
        {
            this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
        }
    }
    public class ValueClassFakerShared : BaseFaker<ValueClass>
    {
        public ValueClassFakerShared(RandomGenerator random):base(random)
        {
            RuleFor(val => val.Number, rg => rg.Random.IntEven());
            RuleFor(val => val.SmallerNumber, rg => rg.Random.Byte());
        }
    }
    public class StorageFakerShared : BaseFaker<Storage>
    {
        public StorageFakerShared()
        {
            SetFaker(s => s.Value, new ValueClassFakerShared(this.Random));
            RuleFor(s => s.Property, rg => rg.Random.Int(upper: 42));
            RuleFor(s => s.Field, rg => rg.Random.Double());
        }
    }
    enum col
    {
        red,
        blue,
        green
    }
    class ProviderTest : IValueProvider
    {
        int Provide()
        {
            return 42;
        }
    }
    public class ProvideFaker : BaseFaker<ValueClass>
    {
        public ProvideFaker()
        {
            //ProvideVal(val => val.Number, p => p.Provide());
            //RuleFor(val => val.Number, _ => Animation.statickapolozka)
        }
    }
}
