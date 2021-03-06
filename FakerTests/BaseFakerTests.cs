using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System.Reflection;

namespace FakerTests
{
    public class ValueClassFakerBase : BaseFaker<ValueClass>
    {
        public ValueClassFakerBase()
        {
            //RuleFor(x => x.Value, _ => 42);
            SetRuleFor(x => x.Value).Rule(_ => 42);
            //RuleFor(x => x.AnotherVal, r => r.Random.Int());
            SetRuleFor(x => x.AnotherVal).Rule(r => r.Random.Int());
        }
    }

    public class StorageFakerBase : StrictFaker<Storage>
    {
        public StorageFakerBase()
        {
            //RuleFor(x => x.Text, _ => "ABRAKA");
            SetRuleFor(x => x.Text).Rule(_ => "ABRAKA");
            //RuleFor(x => x.Test, _ => 42);
            SetRuleFor(x => x.Test).Rule(_ => 42);
            //RuleFor(x => x.Field, rg => rg.Random.Byte());
            SetRuleFor(x => x.Field).Rule(rg => rg.Random.Byte());
            //SetFaker(x => x.Value, new ValueClassFakerBase());
            SetFakerFor(x => x.Value).Faker(new ValueClassFakerBase());
        }
    }

    public class SimpleClass
    {
        [FakerIgnore]
        public string IgnoredString = "IGNORED";

        public int Int = 42;

        public sbyte Sbyte { get; set; } = 42;

        public long WithRuleFor = 42;

        public override string ToString()
        {
            return TestUtils.InstanceToString(this);
        }
    }

    public class SimpleClassBaseFaker : BaseFaker<SimpleClass>
    {
        public SimpleClassBaseFaker()
        {
            //RuleFor(x => x.WithRuleFor, _ => 73);
            SetRuleFor(x => x.WithRuleFor).Rule(_ => 73);
        }
    }

    public class SimpleClassAutoFaker : AutoFaker<SimpleClass>
    {
        public SimpleClassAutoFaker()
        {
            //RuleFor(x => x.WithRuleFor, _ => 73);
            SetRuleFor(x => x.WithRuleFor).Rule(_ => 73);
        }
    }

    public class NestedClass
    {
        public int num;
        public ValueClass value { get; set; } 
        public NestedClass() { }
        public override string ToString()
        {
            return TestUtils.InstanceToString(this);
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
            //return $"Number={this.Number},SmallerNumber={this.SmallerNumber},{this.Value},Private={this.Private},\nIsAwesome={this.IsAwesome},SomeString={this.SomeString} nested:{this.nested}";
            return TestUtils.InstanceToString(this);
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
            //return $"Storage, Test = {this.Test}, Field = {this.Field}, Text = {this.Text}, Value ={this.Value}";
            return TestUtils.InstanceToString(this);
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
    public class Base { }
    public class Derived : Base { }

    public class ConvertClass
    {
        public int Int { get; set; }
        public byte Byte;
        public long Long { get; set; }
        public Base Base { get; set; }
        public Derived Derived { get; set; }
        public override string ToString()
        {
            return TestUtils.InstanceToString(this);
        }
    }
    public class DerivedFaker : BaseFaker<Derived> { }
    public class BaseClassFaker :BaseFaker<Base> { }

    public class ConvertClassFaker : BaseFaker<ConvertClass>
    {
        public ConvertClassFaker()
        {
            //RuleFor(x => x.Int, rg => rg.Random.Double());
            SetRuleFor(x => x.Int).Rule(rg => rg.Random.Int());
            //RuleFor(x => x.Long, rg => rg.Random.Int());
            SetRuleFor(x => x.Long).Rule(rg => rg.Random.Byte());
            //RuleFor(x=> x.Int, rg => rg.Random.Byte());
            SetRuleFor(x => x.Byte).Rule(rg => rg.Random.Byte());
            //RuleFor(x => x.Base, _ => d);
            //SetFakerFor(x => x.Base).Faker(new DerivedFaker());
        }
    }

    public class ValueClassFakerParams : BaseFaker<ValueClass>
    {
        public ValueClassFakerParams()
        {
            this.CtorUsageFlag = InnerFakerConstructorUsage.GivenParameters;
            this.CtorParameters = new object[] { 73 };
            //RuleFor(e => e.Value, f => 10);
            SetRuleFor(e => e.Value).Rule(f => 10);
        }
    }
    public class ValueClassFakerFlawedParams : BaseFaker<ValueClass>
    {
        public ValueClassFakerFlawedParams()
        {
            this.CtorUsageFlag = InnerFakerConstructorUsage.GivenParameters;
            this.CtorParameters = new object[] { "flawed param" };
            //RuleFor(e => e.Value, f => 10);
            SetRuleFor(e => e.Value).Rule(f => 10);
        }
    }

    public class StorageFakerFlawedParams : BaseFaker<Storage>
    {
        public StorageFakerFlawedParams()
        {

            //SetFaker(e => e.Value, new ValueClassFakerFlawedParams());
            SetFakerFor(e => e.Value).Faker(new ValueClassFakerFlawedParams());
            //RuleFor(e => e.Test, f => f.Random.Int());
            SetRuleFor(e => e.Test).Rule(f => f.Random.Int());
            //RuleFor(e => e.Field, f => f.Random.Byte());
            SetRuleFor(e => e.Field).Rule(f => f.Random.Byte());
        }
    }

    public class ValueClassFakerParameterless : BaseFaker<ValueClass>
    {
        public ValueClassFakerParameterless()
        {
            this.CtorUsageFlag = InnerFakerConstructorUsage.Parameterless;
            //RuleFor(e => e.Value, f => 10);
            SetRuleFor(e => e.Value).Rule(f => 10);
        }
    }
    public class ValueClassFakerPopulate : BaseFaker<ValueClass>
    {
        public ValueClassFakerPopulate()
        {
            this.CtorUsageFlag = InnerFakerConstructorUsage.PopulateExistingInstance;
            //RuleFor(e => e.Value, f => 10);
            SetRuleFor(e => e.Value).Rule(f => 10);
        }
    }


    public class StorageFakerParams : BaseFaker<Storage>
    {
        public StorageFakerParams()
        {

            //SetFaker(e => e.Value, new ValueClassFakerParams());
            SetFakerFor(e => e.Value).Faker(new ValueClassFakerParams());
            //RuleFor(e => e.Test, f => f.Random.Int());
            SetRuleFor(e => e.Test).Rule(f => f.Random.Int());
            //RuleFor(e => e.Field, f => f.Random.Byte()); 
            SetRuleFor(e => e.Field).Rule(f => f.Random.Byte());

        }
    }
    public class StorageFakerPopulate : BaseFaker<Storage>
    {
        public StorageFakerPopulate()
        {

            //SetFaker(e => e.Value, new ValueClassFakerPopulate());
            SetFakerFor(e => e.Value).Faker(new ValueClassFakerPopulate());
            //RuleFor(e => e.Test, f => f.Random.Int());
            SetRuleFor(e => e.Test).Rule(f => f.Random.Int());
            //RuleFor(e => e.Field, f => f.Random.Byte());
            SetRuleFor(e => e.Field).Rule(f => f.Random.Byte());

        }
    }
    public class StorageFakerParamless : BaseFaker<Storage>
    {
        public StorageFakerParamless()
        {

            // SetFaker(e => e.Value, new ValueClassFakerParameterless());
            SetFakerFor(e => e.Value).Faker(new ValueClassFakerParameterless());
            //RuleFor(e => e.Test, f => f.Random.Int());
            SetRuleFor(e => e.Test).Rule(f => f.Random.Int());
            //RuleFor(e => e.Field, f => f.Random.Byte());
            SetRuleFor(e => e.Field).Rule(f => f.Random.Byte());
        }
    }
    public class StorageFakerParamlessAuto : AutoFaker<Storage>
    {
        public StorageFakerParamlessAuto()
        {

            //SetFaker(e => e.Value, new ValueClassFakerParameterless());
            SetFakerFor(e => e.Value).Faker(new ValueClassFakerParameterless());
            //RuleFor(e => e.Test, f => f.Random.Int());
            SetRuleFor(e => e.Test).Rule(f => f.Random.Int());
            //RuleFor(e => e.Field, f => f.Random.Byte());
            SetRuleFor(e => e.Field).Rule(f => f.Random.Byte());
        }
    }

    public class StorageFakerMultipleRuleFor :BaseFaker<Storage>
    {
        public StorageFakerMultipleRuleFor()
        {
            //RuleFor(e => e.Test, f => f.Random.Int());
            SetRuleFor(e => e.Test).Rule(f => f.Random.Int());
            //RuleFor(e => e.Field, f => f.Random.Byte());
            SetRuleFor(e => e.Field).Rule(f => f.Random.Byte());
            //RuleFor(e => e.Test, f => f.Random.Int());
            SetRuleFor(e => e.Test).Rule(f => f.Random.Int());
        }
    }
    public class StorageFakerMultipleSetFaker : BaseFaker<Storage>
    {
        public StorageFakerMultipleSetFaker()
        {
            //SetFaker(e => e.Value, new ValueClassFakerParameterless());
            SetFakerFor(e => e.Value).Faker(new ValueClassFakerParameterless());
            //RuleFor(e => e.Test, f => f.Random.Int());
            SetRuleFor(e => e.Test).Rule(f => f.Random.Int());
            //RuleFor(e => e.Field, f => f.Random.Byte());
            SetRuleFor(e => e.Field).Rule(f => f.Random.Byte());
            //SetFaker(e => e.Value, new ValueClassFakerPopulate()) ;
            SetFakerFor(e => e.Value).Faker(new ValueClassFakerParameterless());
        }
    }
    public class StorageFakerRuleForAndSetFaker : BaseFaker<Storage>
    {
        public StorageFakerRuleForAndSetFaker()
        {
            //SetFaker(e => e.Value, new ValueClassFakerParameterless());
            SetFakerFor(e => e.Value).Faker(new ValueClassFakerParameterless());
            //RuleFor(e => e.Test, f => f.Random.Int());
            SetRuleFor(e => e.Test).Rule(f => f.Random.Int());
            //RuleFor(e => e.Value, f => new ValueClass());
            SetRuleFor(e => e.Value).Rule(_ => new ValueClass());
        }
    }
    public class ValueFakerAuto : AutoFaker<ValueClass> { }

    public class StorageFakerFillDefault : AutoFaker<Storage>
    {
        public StorageFakerFillDefault()
        {
            //RuleFor(e => e.Text, f => "ABRAKA DABRA");
            SetRuleFor(e => e.Text).Rule(_ => "ABRAKA DABRA");
            //SetFaker(e => e.Value, new ValueFakerAuto());
            SetFakerFor(e => e.Value).Faker(new ValueFakerAuto());
            //this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
        }
    }

    public class NestedClassFakerAuto : AutoFaker<NestedClass>
    {
        public NestedClassFakerAuto()
        {
            //SetFaker(e => e.value, new ValueFakerAuto());
            SetFakerFor(e => e.value).Faker(new ValueFakerAuto());
        }
    }

    public class AwesomeFakerNoRules : AutoFaker<AwesomeClass>
    {
        public AwesomeFakerNoRules(InnerFakerConstructorUsage ctorFlag, object[] parameters)
        {
            //RuleFor(e => e.SomeString, f => "ABRAKA");
            SetRuleFor(e => e.SomeString).Rule(f => "ABRAKA");
            //SetFaker(e => e.Value, new ValueFakerAuto());
            SetFakerFor(e => e.Value).Faker(new ValueFakerAuto());
            //SetFaker(e => e.nested, new NestedClassFakerAuto());
            SetFakerFor(e => e.nested).Faker(new NestedClassFakerAuto());
            //this.FillEmptyMembers = unfilled;
            this.CtorUsageFlag = ctorFlag;
            this.CtorParameters = parameters;
        }
    }
    public class AwesomeFaker : AutoFaker<AwesomeClass>
    {
        public AwesomeFaker()
        {
            //RuleFor(e => e.SomeString, f => "ABRAKA");
            SetRuleFor(e => e.SomeString).Rule(f => "ABRAKA");
            //RuleFor(e => e.SmallerNumber, g => g.Random.Byte());
            SetRuleFor(e => e.SmallerNumber).Rule(g => g.Random.Byte());
            //RuleFor(e => e.Number, h => h.Random.Int());
            SetRuleFor(e => e.Number).Rule(g => g.Random.Int());
            //RuleFor(e => e.IsAwesome, i => i.Random.Bool());
            SetRuleFor(e => e.IsAwesome).Rule(g => g.Random.Bool());
            //this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
            //this.SetFaker(e => e.Value, new ValueClassFakerParameterless());
            SetFakerFor(e => e.Value).Faker(new ValueClassFakerParameterless());
        }
    }
    public class NestedFaker : BaseFaker<NestedClass>
    {
        public NestedFaker()
        {
            //RuleFor(e => e.num, f => 42);
            SetRuleFor(e => e.num).Rule(_ => 42);
            //SetFaker(e => e.value, new ValueClassFakerParameterless());
            SetFakerFor(e => e.value).Faker(new ValueClassFakerParameterless());
        }
    }
    public class AwesomeFakerNested :AutoFaker<AwesomeClass>
    {
        public AwesomeFakerNested()
        {
            //RuleFor(e => e.SomeString, f => "ABRAKA");
            SetRuleFor(e => e.SomeString).Rule(f => "ABRAKA");
            //this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
            //SetFaker(e => e.nested, new NestedFaker());
            SetFakerFor(e => e.nested).Faker(new NestedFaker());
        }
    }



    [TestClass]
    public class BaseFakerTests
    {
        [TestMethod]
        public void ConvertTest()
        {
            ConvertClassFaker faker = new();
            ConvertClass c = faker.Generate();
            Console.WriteLine(c);
        }

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
            Assert.ThrowsException<FakerException>(() => { Storage b = s.Generate(new object[] { "dabra" }); });
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
            Assert.ThrowsException<FakerException>(() => { Storage s = sf.Generate(); });

        }
        [TestMethod]
        public void GetSetOfMembersWithNoRuleOrFakerTest()
        {
            Type type = typeof(Storage);
            var members = type.GetMembers();
            foreach (var item in members)
            {
                if (item is PropertyInfo || item is FieldInfo)
                    Console.WriteLine(item.Name);
            }
            Console.WriteLine();
            StorageFakerParamlessAuto s = new StorageFakerParamlessAuto();
            HashSet<MemberInfo> memberInfos = s.RulelessMembersInstance;

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
            int numIterations = 20;

            Dictionary<int, int> testCounts = new();
            Dictionary<byte, int> fieldCount = new();
            Dictionary<int, int> valueCounts = new();
            Dictionary<int, int> anotherValCounts = new();

            for (int i = 0; i < numIterations; i++)
            {
                Storage s = f.Generate();
                Console.WriteLine(s);
                TestUtils.IncInDic(testCounts, s.Test);
                TestUtils.IncInDic(fieldCount, s.Field);
                TestUtils.IncInDic(valueCounts, s.Value.Value);
                TestUtils.IncInDic(anotherValCounts, s.Value.AnotherVal);

                Assert.AreEqual("ABRAKA DABRA", s.Text);
            }
            TestUtils.CheckDic(testCounts, numIterations);
            TestUtils.CheckDic(fieldCount, numIterations);
            TestUtils.CheckDic(valueCounts, numIterations);
            TestUtils.CheckDic(anotherValCounts, numIterations);

        }
        [TestMethod]
        public void AwesomeEmptyDefaultFillTest()
        {
            AwesomeFakerNoRules af = new AwesomeFakerNoRules(BaseFaker<AwesomeClass>.InnerFakerConstructorUsage.Parameterless, null);
            int numIterations = 20;

            Dictionary<int, int> numberCounts = new();
            Dictionary<byte, int> smallerNumberCounts = new();
            Dictionary<int, int> valueCounts = new();
            Dictionary<int, int> anotherValCounts = new();
            Dictionary<bool, int> isAwesomeCounts = new();
            Dictionary<int, int> numCounts = new();
            Dictionary<int, int> nestedValueCounts = new();
            Dictionary<int, int> nestedAnotherValCounts = new();

            for (int i = 0; i < numIterations; i++)
            {
                AwesomeClass s = af.Generate();
                Console.WriteLine(s);
                Assert.AreEqual("ABRAKA", s.SomeString);
                TestUtils.IncInDic(numberCounts, s.Number);
                TestUtils.IncInDic(smallerNumberCounts, s.SmallerNumber);
                TestUtils.IncInDic(valueCounts, s.Value.Value);
                TestUtils.IncInDic(anotherValCounts, s.Value.AnotherVal);
                TestUtils.IncInDic(isAwesomeCounts, s.IsAwesome);
                TestUtils.IncInDic(numCounts, s.nested.num);
                TestUtils.IncInDic(nestedValueCounts, s.nested.value.Value);
                TestUtils.IncInDic(nestedAnotherValCounts, s.nested.value.AnotherVal);
            }

            TestUtils.CheckDic(numberCounts, numIterations);
            TestUtils.CheckDic(smallerNumberCounts, numIterations);
            TestUtils.CheckDic(valueCounts, numIterations);
            TestUtils.CheckDic(anotherValCounts, numIterations);
            TestUtils.CheckDic(isAwesomeCounts, numIterations);
            TestUtils.CheckDic(numCounts, numIterations);
            TestUtils.CheckDic(nestedValueCounts, numIterations);
            TestUtils.CheckDic(nestedAnotherValCounts, numIterations);
        }

        [TestMethod]
        public void PolymophListTest()
        {
            List<BaseFaker<SimpleClass>> fakers = new();
            SimpleClassAutoFaker auto1 = new();
            SimpleClassAutoFaker auto2 = new();
            SimpleClassBaseFaker base1 = new();
            SimpleClassBaseFaker base2 = new();

            fakers.Add(auto1);
            fakers.Add(base1);
            fakers.Add(auto2);
            fakers.Add(base2);

            int numIterations = 10;

            for (int i = 0; i < 4; i++)
            {
                Dictionary<int, int> intCounts = new();
                Dictionary<sbyte, int> sbyteCounts = new();

                for (int j = 0; j < numIterations; j++)
                {
                    SimpleClass s = fakers[i].Generate();
                    Console.WriteLine(s);

                    Assert.AreEqual("IGNORED", s.IgnoredString);
                    Assert.AreEqual(73, s.WithRuleFor);
                    if (i % 2 == 1)
                    {
                        //baseFaker
                        Assert.AreEqual(42, s.Int);
                        Assert.AreEqual(42, s.Sbyte);
                    }
                    else
                    {
                        TestUtils.IncInDic(intCounts, s.Int);
                        TestUtils.IncInDic(sbyteCounts, s.Sbyte);
                    }
                }
                if (i % 2 == 0)
                {
                    TestUtils.CheckDic(intCounts, numIterations);
                    TestUtils.CheckDic(sbyteCounts, numIterations);
                }
            }
        }
        [TestMethod]
        public void BaseFakerBasicTest()
        {
            Dictionary<int, int> valueCounts = new();
            Dictionary<byte, int> fieldCounts = new();
            int numIterations = 20;

            for (int i = 0; i < numIterations; i++)
            {
                StorageFakerBase faker = new();
                Storage s = faker.Generate();

                Console.WriteLine(s);
                TestUtils.IncInDic(valueCounts, s.Value.AnotherVal);
                TestUtils.IncInDic(fieldCounts, s.Field);

                Assert.AreEqual("ABRAKA", s.Text);
                Assert.AreEqual(42, s.Test);
                Assert.AreEqual(42, s.Value.Value);
            }

            TestUtils.CheckDic(valueCounts, numIterations);
            TestUtils.CheckDic(fieldCounts, numIterations);
        }
    }
}
