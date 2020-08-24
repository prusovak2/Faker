using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;

namespace FakerTests
{
    public class Storage
    { 
        public ValueClass Value { get; set; }

        public int Test { get; set; }
        public override string ToString()
        {
            return $"Storage, Test = {this.Test}";
        }
    }

    public class ValueClassFaker : BaseFaker<ValueClass>
    {
        public ValueClassFaker()
        {
            RuleFor(e => e.Value, f => 10);
        }
    }
    public class StorageFaker : BaseFaker<Storage>
    {
        public StorageFaker()
        {
            //SetFaker(e => e.Value, new BaseFaker<ValueClass>());
            RuleFor(e => e.Test, f => f.RandomInt());

        }
    }
        public class ValueClass
    {
        public int Value { get; set; }
    }
    [TestClass]
    public class BaseFakerTests
    {
        [TestMethod]
        public void GenerateCreateInstanceTest()
        {
            StorageFaker s = new StorageFaker();
            Storage a = s.Generate();
            Console.WriteLine(a);
            Storage b = s.Generate(Storage())
        }
    }
}
