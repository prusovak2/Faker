using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using static FakerTests.TestUtils;

namespace FakerTests
{
    public class ValueClassFakerAllRules : StrictFaker<ValueClass>
    {
        public ValueClassFakerAllRules()
        {
            RuleFor(x => x.Value, _ => 42);
            RuleFor(x => x.AnotherVal,  r => r.Random.Int());
        }
    }

    public class StorageFakerAllRules : StrictFaker<Storage>
    {
        public StorageFakerAllRules()
        {
            RuleFor(x => x.Text, _ => "ABRAKA");
            RuleFor(x => x.Test,_ => 42);
            RuleFor(x => x.Field, rg => rg.Random.Byte());
            SetFaker(x => x.Value, new ValueClassFakerAllRules());
        }
    }

    public class StorageFakerFlawedValueFaker : StrictFaker<Storage>
    {
        public StorageFakerFlawedValueFaker()
        {
            RuleFor(x => x.Text, _ => "ABRAKA");
            RuleFor(x => x.Test, _ => 42);
            RuleFor(x => x.Field, rg => rg.Random.Byte());
            SetFaker(x => x.Value, new ValueClassFakerMissingRule());
        }
    }

    public class ValueClassFakerMissingRule : StrictFaker<ValueClass>
    {
        public ValueClassFakerMissingRule()
        {
            RuleFor(x => x.Value, _ => 42);
        }
    }

    public class StorageFakerMissingRule : StrictFaker<Storage>
    {
        public StorageFakerMissingRule()
        {
            RuleFor(x => x.Text, _ => "ABRAKA");
            RuleFor(x => x.Test, _ => 42);
            RuleFor(x => x.Field, rg => rg.Random.Byte());
        }
    }


    [TestClass]
    public class StrictFakerTests
    {
        [TestMethod]
        public void AllRulesSetTest()
        {
            StorageFakerAllRules faker4 = new();
            Assert.IsTrue(faker4.AllRulesSet());
            Assert.IsTrue(faker4.AllRulesSetRecursively());

            ValueClassFakerMissingRule faker = new();
            Assert.IsFalse(faker.AllRulesSet());
            Assert.IsFalse(faker.AllRulesSetRecursively());

            StorageFakerFlawedValueFaker faker2 = new();
            Assert.IsTrue(faker2.AllRulesSet());
            Assert.IsFalse(faker2.AllRulesSetRecursively());

            StorageFakerMissingRule faker3 = new();
            Assert.IsFalse(faker3.AllRulesSet());
            Assert.IsFalse(faker3.AllRulesSetRecursively());
        }

        [TestMethod]
        public void MissingRuleTest()
        {
            ValueClassFakerMissingRule faker = new();
            ValueClass v;
            Assert.ThrowsException<FakerException>(() => { v = faker.Generate(); });
            Assert.ThrowsException<FakerException>(() => { v = new(); faker.Populate(v); });
            Assert.ThrowsException<FakerException>(() => { v = faker.Generate(42); });

            //inner Faker for value is missing a Rule
            StorageFakerFlawedValueFaker faker2 = new();
            Storage s;
            Assert.ThrowsException<FakerException>(() => { s = faker2.Generate(); });
            Assert.ThrowsException<FakerException>(() => { s = new(); faker2.Populate(s); });
            Assert.ThrowsException<FakerException>(() => { s = faker2.Generate(42, "abraka", false); });

            //storage faker missing inner faker
            StorageFakerMissingRule faker3 = new();
            Assert.ThrowsException<FakerException>(() => { s = faker3.Generate(); });
            Assert.ThrowsException<FakerException>(() => { s = new(); faker3.Populate(s); });
            Assert.ThrowsException<FakerException>(() => { s = faker3.Generate(42, "abraka", false); });
        }


        [TestMethod]
        public void StrictFakerBasicTest()
        {
            Dictionary<int, int> valueCounts = new();
            Dictionary<byte, int> fieldCounts = new();
            int numIterations = 20;

            for (int i = 0; i < numIterations; i++)
            {
                StorageFakerAllRules faker = new();
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

        [TestMethod]
        public void StrictFakerBasicPopulateTess()
        {
            Dictionary<int, int> valueCounts = new();
            Dictionary<byte, int> fieldCounts = new();
            int numIterations = 20;

            for (int i = 0; i < numIterations; i++)
            {
                StorageFakerAllRules faker = new();
                Storage s = new();
                faker.Populate(s);

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
