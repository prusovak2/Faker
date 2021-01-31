using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;

namespace FakerTests
{ 
    public class IgnoredPerson
    {
        public string Name;

        public byte FakeThisByte { get; set; } = 73;
        public int ProprertyThatShouldNotBeFaked { get; set; } = 42;

        public string FieldThatShouldNotBeFaked = "NO FAKE";

        public override string ToString()
        {
            //return $"IgnoredPerson:\n \tName: {Name}\n\t FakeThisByte: {FakeThisByte}\n\t PropThatShouldNotBeFaked: {ProprertyThatShouldNotBeFaked}\n\t FieldThatShouldNotBeFaked: {FieldThatShouldNotBeFaked}\n";
            return TestUtils.InstanceToString(this);
        }
    }
    public class IgnoredPersonFaker : BaseFaker<IgnoredPerson>
    {
        public IgnoredPersonFaker()
        {
            this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
            RuleFor(p => p.Name, _ => "ABRAKA_FAKE");
            Ignore(p => p.ProprertyThatShouldNotBeFaked);
            Ignore(p => p.FieldThatShouldNotBeFaked);
        }
    }

    public class LotOfMembers
    {
        public int Int;
        public byte Byte;
        public short Short { get; set; }
        public DateTime DateTime { get; set; }
        public double Double;
        public Guid Guid;
        public string IgnoredString { get; set; } = "IGNORED";
        public int IgnoredInt = 42;

        public override string ToString()
        {
            return TestUtils.InstanceToString(this);
        }
    }

    public class LotOfMembersFaker : BaseFaker<LotOfMembers>
    {
        public LotOfMembersFaker()
        {
            FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
            Ignore(l => l.IgnoredString);
            Ignore(l => l.IgnoredInt);
        }
    }

    public class Value
    {
        public short value;
    }

    public class ContainsValue
    {
        public int Int;
        public Value Val { get; set; }
    }

    public class ValueFaker : BaseFaker<Value> { }

    public class FlawedFakerIgnoreRuleFor : BaseFaker<ContainsValue>
    {
        public FlawedFakerIgnoreRuleFor()
        {
            Ignore(l => l.Int);
            RuleFor(l => l.Int, _ => 73);
        }
    }
    public class FlawedFakerRuleForIgnore : BaseFaker<ContainsValue>
    {
        public FlawedFakerRuleForIgnore()
        {
            RuleFor(l => l.Int, _ => 73);
            Ignore(l => l.Int);
        }
    }
    public class FlawedFakerIgnoreSetFaker : BaseFaker<ContainsValue>
    {
        public FlawedFakerIgnoreSetFaker()
        {
            Ignore(cv => cv.Val);
            SetFaker(cv => cv.Val, new ValueFaker());
        }
    }
    public class FlawedFakerSetFakerIgnore : BaseFaker<ContainsValue>
    {
        public FlawedFakerSetFakerIgnore()
        {
            SetFaker(cv => cv.Val, new ValueFaker());
            Ignore(cv => cv.Val);
        }
    }
    public class FlawedFakerSetFakerRuleFor: BaseFaker<ContainsValue>
    {
        public FlawedFakerSetFakerRuleFor()
        {
            SetFaker(cv => cv.Val, new ValueFaker());
            RuleFor(cv => cv.Val, _ => new Value());
        }
    }
    public class FlawedFakerRuleForSetFaker : BaseFaker<ContainsValue>
    {
        public FlawedFakerRuleForSetFaker()
        {
            RuleFor(cv => cv.Val, _ => new Value());
            SetFaker(cv => cv.Val, new ValueFaker());
        }
    }
    public class IgnoreIgnoreFaker : BaseFaker<ContainsValue>
    {
        public IgnoreIgnoreFaker()
        {
            Ignore(cv => cv.Val);
            Ignore(cv => cv.Val);
        }
    }

    [TestClass]
    public class BaseFakerIgnoreTests
    {
        static void IncInDic<T>(Dictionary<T, int> dic, T member)
        {
            if (dic.ContainsKey(member))
            {
                dic[member]++;
            }
            else
            {
                dic.Add(member, 1);
            }
        }

        static void CheckDic<T>(Dictionary<T, int> dic, int unwantedValue)
        {
            foreach (var item in dic)
            {
                if (dic[item.Key] == unwantedValue)
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void IgnoredBasicTest()
        {
            int counter73 = 0;
            int numIterations = 20;
            for (int i = 0; i < numIterations; i++)
            {
                IgnoredPerson person = new IgnoredPerson();
                IgnoredPersonFaker faker = new IgnoredPersonFaker();
                faker.Populate(person);
                Console.WriteLine(person);
                Assert.AreEqual("ABRAKA_FAKE", person.Name);
                if (person.FakeThisByte == 73)
                {
                    counter73++;
                }
                Assert.AreEqual(42, person.ProprertyThatShouldNotBeFaked);
                Assert.AreEqual("NO FAKE", person.FieldThatShouldNotBeFaked);
            }
            Assert.AreNotEqual(numIterations, counter73);
        }

        [TestMethod]
        public void IgnoreLotOfMembersTest()
        {
            int numIterations = 20;
            Dictionary<int, int> intCounts = new Dictionary<int, int>();
            Dictionary<byte, int> byteCounts = new Dictionary<byte, int>();
            Dictionary<short, int> shortCounts = new Dictionary<short, int>();
            Dictionary<DateTime, int> dateTimeCounts = new Dictionary<DateTime, int>();
            Dictionary<double, int> doubleCounts = new Dictionary<double, int>();
            Dictionary<Guid, int> guidCounts = new Dictionary<Guid, int>();

            for (int i = 0; i < numIterations; i++)
            {
                LotOfMembers lom;
                LotOfMembersFaker faker = new LotOfMembersFaker();
                lom = faker.Generate();

                IncInDic(intCounts, lom.Int);
                IncInDic(byteCounts, lom.Byte);
                IncInDic(shortCounts, lom.Short);
                IncInDic(dateTimeCounts, lom.DateTime);
                IncInDic(doubleCounts, lom.Double);
                IncInDic(guidCounts, lom.Guid);

                Console.WriteLine(lom);

                Assert.AreEqual("IGNORED", lom.IgnoredString);
                Assert.AreEqual(42, lom.IgnoredInt);
            }
            CheckDic(intCounts, numIterations);
            CheckDic(byteCounts, numIterations);
            CheckDic(shortCounts, numIterations);
            CheckDic(dateTimeCounts, numIterations);
            CheckDic(doubleCounts, numIterations);
            CheckDic(guidCounts, numIterations);
        }

        [TestMethod]
        public void IgnoreExceptionsTest()
        {
            Assert.ThrowsException<FakerException>(() => { FlawedFakerRuleForIgnore f = new FlawedFakerRuleForIgnore(); });
            Assert.ThrowsException<FakerException>(() => { FlawedFakerIgnoreRuleFor f = new FlawedFakerIgnoreRuleFor(); });

            Assert.ThrowsException<FakerException>(() => { FlawedFakerSetFakerIgnore f = new FlawedFakerSetFakerIgnore(); });
            Assert.ThrowsException<FakerException>(() => { FlawedFakerIgnoreSetFaker f = new FlawedFakerIgnoreSetFaker(); });

            Assert.ThrowsException<FakerException>(() => { FlawedFakerRuleForSetFaker f = new FlawedFakerRuleForSetFaker(); });
            Assert.ThrowsException<FakerException>(() => { FlawedFakerSetFakerRuleFor f = new FlawedFakerSetFakerRuleFor(); });

            //should not throw the exception
            IgnoreIgnoreFaker i = new IgnoreIgnoreFaker();
        }
    }
}


