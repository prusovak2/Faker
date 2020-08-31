using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System.Reflection;

namespace FakerTests
{
    public class NestedClass
    {
        public int num;
        public ValueClass value { get; set; } 
        public NestedClass() { }
        public override string ToString()
        {
            return $"Nested:num:{this.num},{this.value},";
        }
    }

    public class AwesomeClass
    {
        public int Number;
        public byte SmallerNumber { get; set; }
        public ValueClass Value { get; set; }
        private sbyte Private { get; set; }
        public bool IsAwesome;
        public string SomeString { get; set; }
        public NestedClass nested { get; set; }
        public AwesomeClass() { }
        public AwesomeClass(string s, int a)
        {
            this.Number = a;
            this.SomeString = s; 
        }
        public override string ToString()
        {
            return $"Number={this.Number},SmallerNumber={this.SmallerNumber},{this.Value},Private={this.Private},\nIsAwesome={this.IsAwesome},SomeString={this.SomeString} nested:{this.nested}";
        }
    }
    public class Storage
    { 
        public ValueClass Value { get; set; }

        public int Test { get; set; }
        public byte Field;
        public string Text { get; set; }
        public override string ToString()
        {
            return $"Storage, Test = {this.Test}, Field = {this.Field}, Text = {this.Text}, Value ={this.Value}";
        }
        public Storage(int num, string text, bool abraka) 
        {
            this.Text = text;
        }
        public Storage() { }
        
    }
    public class ValueClass
    {
        public int Value { get; set; }
        public int AnotherVal;
        public ValueClass() { }
        public ValueClass(int a)
        {
            this.AnotherVal = a;
        }
        public override string ToString()
        {
            return $"Value={this.Value}, AnotherVal={this.AnotherVal}";
        }
    }
    public class ValueClassFakerParams : BaseFaker<ValueClass>
    {
        public ValueClassFakerParams()
        {
            this.CtorUsageFlag = InnerFakerConstructorUsage.GivenParameters;
            this.CtorParametrs = new object[] { 73 };
            RuleFor(e => e.Value, f => 10);
        }
    }
    public class ValueClassFakerFlawedParams : BaseFaker<ValueClass>
    {
        public ValueClassFakerFlawedParams()
        {
            this.CtorUsageFlag = InnerFakerConstructorUsage.GivenParameters;
            this.CtorParametrs = new object[] { "flawed param" };
            RuleFor(e => e.Value, f => 10);
        }
    }

    public class StorageFakerFlawedParams : BaseFaker<Storage>
    {
        public StorageFakerFlawedParams()
        {

            SetFaker(e => e.Value, new ValueClassFakerFlawedParams());
            RuleFor(e => e.Test, f => f.Random.Int());
            RuleFor(e => e.Field, f => f.Random.Byte());

        }
    }

    public class ValueClassFakerParameterless : BaseFaker<ValueClass>
    {
        public ValueClassFakerParameterless()
        {
            this.CtorUsageFlag = InnerFakerConstructorUsage.Parameterless;
            RuleFor(e => e.Value, f => 10);
        }
    }
    public class ValueClassFakerPopulate : BaseFaker<ValueClass>
    {
        public ValueClassFakerPopulate()
        {
            this.CtorUsageFlag = InnerFakerConstructorUsage.PopulateExistingInstance;
            RuleFor(e => e.Value, f => 10);
        }
    }


    public class StorageFakerParams : BaseFaker<Storage>
    {
        public StorageFakerParams()
        {

            SetFaker(e => e.Value, new ValueClassFakerParams());
            RuleFor(e => e.Test, f => f.Random.Int());
            RuleFor(e => e.Field, f => f.Random.Byte()); 

        }
    }
    public class StorageFakerPopulate : BaseFaker<Storage>
    {
        public StorageFakerPopulate()
        {

            SetFaker(e => e.Value, new ValueClassFakerPopulate());
            RuleFor(e => e.Test, f => f.Random.Int());
            RuleFor(e => e.Field, f => f.Random.Byte());

        }
    }
    public class StorageFakerParamless : BaseFaker<Storage>
    {
        public StorageFakerParamless()
        {

            SetFaker(e => e.Value, new ValueClassFakerParameterless());
            RuleFor(e => e.Test, f => f.Random.Int());
            RuleFor(e => e.Field, f => f.Random.Byte());

        }
    }
    public class StorageFakerMultipleRuleFor :BaseFaker<Storage>
    {
        public StorageFakerMultipleRuleFor()
        {
            RuleFor(e => e.Test, f => f.Random.Int());
            RuleFor(e => e.Field, f => f.Random.Byte());
            RuleFor(e => e.Test, f => f.Random.Int());
        }
    }
    public class StorageFakerMultipleSetFaker : BaseFaker<Storage>
    {
        public StorageFakerMultipleSetFaker()
        {
            SetFaker(e => e.Value, new ValueClassFakerParameterless());
            RuleFor(e => e.Test, f => f.Random.Int());
            RuleFor(e => e.Field, f => f.Random.Byte());
            SetFaker(e => e.Value, new ValueClassFakerPopulate()) ;
        }
    }
    public class StorageFakerRuleForAndSetFaker : BaseFaker<Storage>
    {
        public StorageFakerRuleForAndSetFaker()
        {
            SetFaker(e => e.Value, new ValueClassFakerParameterless());
            RuleFor(e => e.Test, f => f.Random.Int());
            RuleFor(e => e.Value, f => new ValueClass());
        }
    }
    public class StorageFakerFillDefault : BaseFaker<Storage>
    {
        public StorageFakerFillDefault()
        {
            RuleFor(e => e.Text, f => "ABRAKA DABRA");
            this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
        }
    }
    public class AwesomeFakerNoRules : BaseFaker<AwesomeClass>
    {
        public AwesomeFakerNoRules(UnfilledMembers unfilled, InnerFakerConstructorUsage ctorFlag, object[] parameters)
        {
            RuleFor(e => e.SomeString, f => "ABRAKA");
            this.FillEmptyMembers = unfilled;
            this.CtorUsageFlag = ctorFlag;
            this.CtorParametrs = parameters;
        }
    }
    public class AwesomeFaker : BaseFaker<AwesomeClass>
    {
        public AwesomeFaker()
        {
            RuleFor(e => e.SomeString, f => "ABRAKA");
            RuleFor(e => e.SmallerNumber, g => g.Random.Byte());
            RuleFor(e => e.Number, h => h.Random.Int());
            RuleFor(e => e.IsAwesome, i => i.Random.Bool());
            this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
            this.SetFaker(e => e.Value, new ValueClassFakerParameterless());
        }
    }
    public class NestedFaker : BaseFaker<NestedClass>
    {
        public NestedFaker()
        {
            RuleFor(e => e.num, f => 42);
            SetFaker(e => e.value, new ValueClassFakerParameterless());
        }
    }
    public class AwesomeFakerNested :BaseFaker<AwesomeClass>
    {
        public AwesomeFakerNested()
        {
            RuleFor(e => e.SomeString, f => "ABRAKA");
            this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
            SetFaker(e => e.nested, new NestedFaker());
        }
    }



    [TestClass]
    public class BaseFakerTests
    {
        [TestMethod]
        public void NestedTest()
        {
            AwesomeFakerNested f = new AwesomeFakerNested();
            AwesomeClass a = f.Generate();
            Console.WriteLine(a);
            Assert.IsInstanceOfType(a.nested, typeof(NestedClass));
            Assert.IsInstanceOfType(a.nested.value, typeof(ValueClass));
            Assert.AreEqual(42, a.nested.num);
        }
        [TestMethod]
        public void SebastiansAwesomeTest()
        {
            AwesomeFaker awesomeFaker = new AwesomeFaker();
            AwesomeClass awesome = awesomeFaker.Generate();
            
            Console.WriteLine(awesome);
            Assert.IsTrue(awesome.Value is object);
            Assert.IsTrue(awesome.IsAwesome == true || awesome.IsAwesome == false);
            Assert.IsTrue(awesome.SomeString == "ABRAKA");
        }
        [TestMethod]
        public void GenerateCreateInstanceTest()
        {
            StorageFakerParamless s = new StorageFakerParamless();
            Storage a = s.Generate(new object[] { 42, "text", true });
            Console.WriteLine(a);
            Assert.AreEqual("text", a.Text);
            Assert.AreEqual(10, a.Value.Value);
            Assert.ThrowsException<ArgumentException>(() => { Storage b = s.Generate(new object[] { "dabra"}); });
            Storage b = s.Generate();
            Assert.AreEqual(10, b.Value.Value);
            Console.WriteLine(b);

        }
        [TestMethod]
        public void InnerFakerTest()
        {
            StorageFakerParamless s = new StorageFakerParamless();
            Storage a = s.Generate(new object[] { 42, "text", true });
            Console.WriteLine(a);
            Assert.AreEqual("text", a.Text);
            Assert.AreEqual(10, a.Value.Value);

            StorageFakerParams paramless = new StorageFakerParams();
            a = paramless.Generate(new object[] { 42, "text", true });
            Console.WriteLine(a);
            Assert.AreEqual("text", a.Text);
            Assert.AreEqual(10, a.Value.Value);
            Assert.AreEqual(73, a.Value.AnotherVal);

            StorageFakerPopulate populate = new StorageFakerPopulate();
            Storage stor = new Storage();
            ValueClass val = new ValueClass();
            stor.Value = val;
            a = populate.Populate(stor);
            Console.WriteLine(a);
            Assert.AreEqual(10, a.Value.Value);

            //try to populate storage that does not have ValueClass instantiated with inner faker that tries to populate existing instance of ValueClass
            Storage ss = new Storage();
            Assert.ThrowsException<FakerException>(() => { Storage aa = populate.Populate(ss); });
        }
        [TestMethod]
        public void BaseFakerExceptionsTest()
        {
            Assert.ThrowsException<FakerException>(() => { StorageFakerMultipleRuleFor s = new StorageFakerMultipleRuleFor(); });
            Assert.ThrowsException<FakerException>(() => { StorageFakerMultipleSetFaker s = new StorageFakerMultipleSetFaker(); });
            Assert.ThrowsException<FakerException>(() => { StorageFakerRuleForAndSetFaker s = new StorageFakerRuleForAndSetFaker(); });
            StorageFakerFlawedParams sf = new StorageFakerFlawedParams();
            Assert.ThrowsException<ArgumentException>(() => { Storage s = sf.Generate(); });

        }
        [TestMethod]
        public void GetSetOfMembersWithNoRuleOrFakerTest()
        {
            Type type = typeof(Storage);
            var members = type.GetMembers();
            foreach (var item in members)
            {
                if(item is PropertyInfo || item is FieldInfo)
                    Console.WriteLine(item.Name);
            }
            Console.WriteLine();
            StorageFakerParamless s = new StorageFakerParamless();
            HashSet<MemberInfo> memberInfos = s.GetSetOfMembersWithNoRuleOrFaker();

            foreach (var item in memberInfos)
            {
                Console.WriteLine(item.Name);
                Assert.AreEqual("Text", item.Name);
            }
            Assert.AreEqual(1, memberInfos.Count);
        }
        [TestMethod]
        public void DefaultFillTest()
        {
            StorageFakerFillDefault f = new StorageFakerFillDefault();
            Storage s = f.Generate();
            Console.WriteLine(s);
            //may fail when 0 is randomly generated
            Assert.AreNotEqual(0, s.Test);
            Assert.AreNotEqual(0, s.Field);
            Assert.AreEqual("ABRAKA DABRA", s.Text);
        }
        [TestMethod]
        public void AwesomeEmptyDefaultFillTest()
        {
            AwesomeFakerNoRules af = new AwesomeFakerNoRules(BaseFaker<AwesomeClass>.UnfilledMembers.DefaultRandomFunc, BaseFaker<AwesomeClass>.InnerFakerConstructorUsage.Parameterless, null);
            AwesomeClass a = af.Generate();
            Console.WriteLine(a);
            Assert.AreNotEqual(0, a.Number);
            Assert.AreEqual("ABRAKA", a.SomeString);
        }
    }
}
