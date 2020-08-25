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
        public byte Field;
        public string Text { get; set; }
        public override string ToString()
        {
            return $"Storage, Test = {this.Test}, Field = {this.Field}, Text = {this.Text}";
        }
        public Storage(int num, string text, bool abraka) 
        {
            this.Text = text;
        }
        public Storage() { }
        
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
            //RuleFor(e => e.Field, f => f.RandomByte()); THROWS EXCEPTION!

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
            Storage a = s.Generate(new object[] { 42, "text", true });
            Console.WriteLine(a);
            Assert.AreEqual("text", a.Text);
            Assert.ThrowsException<ArgumentException>(() => { Storage b = s.Generate(new object[] { "dabra"}); });
            Storage b = s.Generate();
            Console.WriteLine(b);

        }
    }
}
