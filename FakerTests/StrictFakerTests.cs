using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static FakerTests.TestUtils;

namespace FakerTests
{
    public class ValueClassFakerAllRules : StrictFaker<ValueClass>
    {
        public ValueClassFakerAllRules()
        {
            //RuleFor(x => x.Value, _ => 42);
            For(x => x.Value).SetRule(_ => 42);
            //RuleFor(x => x.AnotherVal,  r => r.Random.Int());
            For(x => x.AnotherVal).SetRule(r => r.Random.Int());
        }
    }

    public class StorageFakerAllRules : StrictFaker<Storage>
    {
        public StorageFakerAllRules()
        {
            //RuleFor(x => x.Text, _ => "ABRAKA");
            For(x => x.Text).SetRule(_ => "ABRAKA");
            //RuleFor(x => x.Test,_ => 42);
            For(x => x.Test).SetRule(_ => 42);
            //RuleFor(x => x.Field, rg => rg.Random.Byte());
            For(x => x.Field).SetRule(rg => rg.Random.Byte());
            //SetFaker(x => x.Value, new ValueClassFakerAllRules());
            SetFakerFor(x => x.Value).As(new ValueClassFakerAllRules());
        }
    }

    public class StorageFakerFlawedValueFaker : StrictFaker<Storage>
    {
        public StorageFakerFlawedValueFaker()
        {
            //RuleFor(x => x.Text, _ => "ABRAKA");
            For(x => x.Text).SetRule(_ => "ABRAKA");
            //RuleFor(x => x.Test, _ => 42);
            For(x => x.Test).SetRule(_ => 42);
            //RuleFor(x => x.Field, rg => rg.Random.Byte());
            For(x => x.Field).SetRule(rg => rg.Random.Byte());
            //SetFaker(x => x.Value, new ValueClassFakerMissingRule());
            SetFakerFor(x => x.Value).As(new ValueClassFakerMissingRule());
        }
    }

    public class ValueClassFakerMissingRule : StrictFaker<ValueClass>
    {
        public ValueClassFakerMissingRule()
        {
            //RuleFor(x => x.Value, _ => 42);
            For(x => x.Value).SetRule(_ => 42);
        }
    }

    public class StorageFakerMissingRule : StrictFaker<Storage>
    {
        public StorageFakerMissingRule()
        {
            //RuleFor(x => x.Text, _ => "ABRAKA");
            For(x => x.Text).SetRule(_ => "ABRAKA");
            //RuleFor(x => x.Test, _ => 42);
            For(x => x.Test).SetRule(_ => 42);
            //RuleFor(x => x.Field, rg => rg.Random.Byte());
            For(x => x.Field).SetRule(rg => rg.Random.Byte());
        }
    }
    public class ValueClassfakerAllRulesMissing : StrictFaker<ValueClass> { }

    public class StorageFakerMultipleMissingRules : StrictFaker<Storage>
    {
        public StorageFakerMultipleMissingRules()
        {
            SetFakerFor(x => x.Value).As(new ValueClassfakerAllRulesMissing());
            //SetFaker(x => x.Value, new ValueClassfakerAllRulesMissing());
        }
    }


    [TestClass]
    public class StrictFakerTests
    {
        [TestMethod]
        public void GetAllMembersRequiringRuleTest()
        {
            StorageFakerAllRules faker4 = new();
            var set1 = faker4.GetAllMembersRequiringRuleShallow();
            var set2 = faker4.GetAllMembersRequiringRuleDeep();
            Assert.IsTrue(set1.Count == 0);
            Assert.IsTrue(set2.Count == 0);

            ValueClassFakerMissingRule faker = new();
            set1 = faker.GetAllMembersRequiringRuleDeep();
            set2 = faker.GetAllMembersRequiringRuleShallow();
            foreach (var item in set1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            foreach (var item in set2)
            {
                Console.WriteLine(item);
            }
            Assert.IsTrue(set1.Count == 1);
            Assert.IsTrue(set2.Count == 1);
            var arr1 = set1.ToArray();
            Assert.AreEqual("AnotherVal", arr1[0].Name);
            var arr2 = set2.ToArray();
            Assert.AreEqual("AnotherVal", arr2[0].Name);
            set1.Clear();
            set2.Clear();

            Console.WriteLine();
            Console.WriteLine();
            StorageFakerFlawedValueFaker faker2 = new();
            set2 = faker2.GetAllMembersRequiringRuleShallow();
            set1 = faker2.GetAllMembersRequiringRuleDeep();
            
            foreach (var item in set1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            foreach (var item in set2)
            {
                Console.WriteLine(item);
            }
            Assert.IsTrue(set1.Count == 1);
            Assert.IsTrue(set2.Count == 0);
            arr1 = set1.ToArray();
            Assert.AreEqual("AnotherVal", arr1[0].Name);

            Console.WriteLine();
            Console.WriteLine();
            StorageFakerMultipleMissingRules faker3 = new();
            set2 = faker3.GetAllMembersRequiringRuleShallow();
            set1 = faker3.GetAllMembersRequiringRuleDeep();
            foreach (var item in set1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            foreach (var item in set2)
            {
                Console.WriteLine(item);
            }
            Assert.IsTrue(set1.Count == 5);
            Assert.IsTrue(set2.Count == 3);
        }

        [TestMethod]
        public void AllRulesSetTest()
        {
            StorageFakerAllRules faker4 = new();
            Assert.IsTrue(faker4.AllRulesSetShallow());
            Assert.IsTrue(faker4.AllRulesSetDeep());

            ValueClassFakerMissingRule faker = new();
            Assert.IsFalse(faker.AllRulesSetShallow());
            Assert.IsFalse(faker.AllRulesSetDeep());

            StorageFakerFlawedValueFaker faker2 = new();
            Assert.IsTrue(faker2.AllRulesSetShallow());
            Assert.IsFalse(faker2.AllRulesSetDeep());

            StorageFakerMissingRule faker3 = new();
            Assert.IsFalse(faker3.AllRulesSetShallow());
            Assert.IsFalse(faker3.AllRulesSetDeep());
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
