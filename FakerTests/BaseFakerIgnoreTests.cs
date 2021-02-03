using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using static FakerTests.TestUtils;

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
    public class IgnoredPersonFakerNoDefaultFill : BaseFaker<IgnoredPerson>
    {
        public IgnoredPersonFakerNoDefaultFill()
        {
            RuleFor(p => p.Name, _ => "ABRAKA_FAKE");
            Ignore(p => p.ProprertyThatShouldNotBeFaked);
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
        public short value = 42;

        public override string ToString()
        {
            return InstanceToString(this);
        }
    }

    public class ContainsValue
    {
        public int Int;
        public Value Val { get; set; }

        public override string ToString()
        {
            return InstanceToString(this);
        }
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
    public class FlawedFakerSetFakerRuleFor : BaseFaker<ContainsValue>
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

    /*############################### IGNORE ATTRIBUTE CLASSES ############################ */
    public class WithIgnoreAttr
    {
        [FakerIgnore]
        public int IgnoredIntField = 42;

        public byte ByteField;

        [FakerIgnore]
        public string IgnoredStringProp { get; set; } = "IGNORED";

        public ulong UlongProp { get; set; }

        public override string ToString()
        {
            return InstanceToString(this);
        }
    }

    public class WithIgnoreAttrFaker : IgnoreFaker<WithIgnoreAttr>
    {
        public WithIgnoreAttrFaker()
        {
            this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
        }
    }
    public class ContainsValueATTR
    {
        [FakerIgnore]
        public int IgnoredInt;
        [FakerIgnore]
        public Value IgnoredVal { get; set; }

        public override string ToString()
        {
            return InstanceToString(this);
        }
    }

    public class ValueFakerNonEmpty : IgnoreFaker<Value>
    {
        public ValueFakerNonEmpty()
        {
            RuleFor(x => x.value, _ => 73);
        }
    }

    public class RuleForSetFakerForIgnoreATTRfaker : IgnoreFaker<ContainsValueATTR>
    {
        public RuleForSetFakerForIgnoreATTRfaker()
        {
            FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
            RuleFor(x => x.IgnoredInt, _ => 73);
            SetFaker(x => x.IgnoredVal, new ValueFakerNonEmpty());
        }
    }

    public class LotOfMembersATTR
    {
        public int Int;
        public byte Byte;
        public short Short { get; set; }
        public DateTime DateTime { get; set; }
        public double Double;
        public Guid Guid;
        [FakerIgnore]
        public string IgnoredString { get; set; } = "IGNORED";
        [FakerIgnore]
        public int IgnoredInt = 42;

        public override string ToString()
        {
            return TestUtils.InstanceToString(this);
        }
    }

    public class LotOfMembersATTRFaker : IgnoreFaker<LotOfMembersATTR>
    {
        public LotOfMembersATTRFaker()
        {
            FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
        }
    }

    public class InnerWithATTR
    {
        [FakerIgnore]
        public int IgnoredInt { get; set; } = 42;
        [FakerIgnore]
        public short IgnoredShort = 42;

        public long Long = 0;

        public override string ToString()
        {
            return InstanceToString(this);
        }
    }

    public class InnerWithATTRIgnoreFaker : IgnoreFaker<InnerWithATTR>
    {
        public InnerWithATTRIgnoreFaker()
        {
            this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
        }
    }

    public class ContainsInnerATTR
    {
        public InnerWithATTR Inner { get; set; }
        public override string ToString()
        {
            return InstanceToString(this);
        }
    }

    public class ContainsInnerATTRIgnoreFaker : IgnoreFaker<ContainsInnerATTR>
    {
        public ContainsInnerATTRIgnoreFaker()
        {
            this.FillEmptyMembers = UnfilledMembers.DefaultRandomFunc;
            SetFaker(x => x.Inner, new InnerWithATTRIgnoreFaker());
        }
    }

    public class LotOfMembersAutoFaker : AutoFaker<LotOfMembers>
    {
        //SHOULD NOT COMPILE
        /*public LotOfMembersAutoFaker()
        {
            FillEmptyMembers = UnfilledMembers.LeaveBlank;
        }*/
    }

    public class UpperNested
    {
        public int IntField = 42;

        public ulong UlongProp { get; set; } = 42;

        public ContainsValue Inner { get; set; }

        public override string ToString()
        {
            return InstanceToString(this); 
        }
    }

    [TestClass]
    public class BaseFakerIgnoreTests
    {
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

        [TestMethod]
        public void NoDefaultFilIgnoreTest()
        {
            for (int i = 0; i < 20; i++)
            {
                IgnoredPerson person = new IgnoredPerson();
                IgnoredPersonFakerNoDefaultFill faker = new IgnoredPersonFakerNoDefaultFill();
                faker.Populate(person);
                Console.WriteLine(person);
                Assert.AreEqual("ABRAKA_FAKE", person.Name);
                Assert.AreEqual(73, person.FakeThisByte);
                Assert.AreEqual(42, person.ProprertyThatShouldNotBeFaked);
                Assert.AreEqual("NO FAKE", person.FieldThatShouldNotBeFaked);
            }
        }

        [TestMethod]
        public void IgnoreAttributeBasicTest()
        {
            int numIterations = 20;
            Dictionary<byte, int> byteCounts = new Dictionary<byte, int>();
            Dictionary<ulong, int> ulongCounts = new Dictionary<ulong, int>();

            for (int i = 0; i < numIterations; i++)
            {
                WithIgnoreAttr wia = new WithIgnoreAttr();
                WithIgnoreAttrFaker faker = new WithIgnoreAttrFaker();
                wia = faker.Generate();

                IncInDic(byteCounts, wia.ByteField);
                IncInDic(ulongCounts, wia.UlongProp);

                Console.WriteLine(wia);

                Assert.AreEqual("IGNORED", wia.IgnoredStringProp);
                Assert.AreEqual(42, wia.IgnoredIntField);
            }
            CheckDic(byteCounts, numIterations);
            CheckDic(ulongCounts, numIterations);
        }
        [TestMethod]
        public void RuleForSetFakerForIgnoreAttrTest()
        {
            int numIterations = 30;

            for (int i = 0; i < numIterations; i++)
            {
                ContainsValueATTR cva = new ContainsValueATTR();
                RuleForSetFakerForIgnoreATTRfaker faker = new RuleForSetFakerForIgnoreATTRfaker();
                cva = faker.Generate();

                Console.WriteLine(cva);

                Assert.AreEqual(73, cva.IgnoredInt);
                Assert.AreEqual(73, cva.IgnoredVal.value);
            }
        }
        [TestMethod]
        public void IgnoreLotOfMembersATTRTest()
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
                LotOfMembersATTR lom;
                LotOfMembersATTRFaker faker = new LotOfMembersATTRFaker();
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
        public void IgnoreFakerAsInnerFakerTest()
        {
            int numIterations = 20;
            Dictionary<long, int> LongCounts = new Dictionary<long, int>();
            for (int i = 0; i < numIterations; i++)
            {
                ContainsInnerATTR cia = new ContainsInnerATTR();
                ContainsInnerATTRIgnoreFaker faker = new ContainsInnerATTRIgnoreFaker();
                cia = faker.Generate();

                IncInDic(LongCounts, cia.Inner.Long);

                Console.WriteLine(cia);

                Assert.AreEqual(42, cia.Inner.IgnoredInt);
                Assert.AreEqual(42, cia.Inner.IgnoredShort);
            }
            CheckDic(LongCounts, numIterations);
        }

        [TestMethod]
        public void AutoFakerBasicTest()
        {
            LotOfMembers lom;
            LotOfMembersAutoFaker faker = new LotOfMembersAutoFaker();
            lom = faker.Generate();
            Console.WriteLine(lom);
            Console.WriteLine(faker.FillEmptyMembers);
            Assert.AreEqual(BaseFaker<LotOfMembers>.UnfilledMembers.DefaultRandomFunc, faker.FillEmptyMembers);
        }

        [TestMethod]
        public void AutoFakerLotOfMembersTest()
        {
            int numIterations = 20;
             Dictionary<int, int> intCounts = new Dictionary<int, int>();
             Dictionary<byte, int> byteCounts = new Dictionary<byte, int>();
             Dictionary<short, int> shortCounts = new Dictionary<short, int>();
             Dictionary<DateTime, int> dateTimeCounts = new Dictionary<DateTime, int>();
             Dictionary<double, int> doubleCounts = new Dictionary<double, int>();
             Dictionary<Guid, int> guidCounts = new Dictionary<Guid, int>();
             Dictionary<int, int> igIntCounts = new Dictionary<int, int>();
             Dictionary<string, int> igStringCounts = new Dictionary<string, int>();


             for (int i = 0; i < numIterations; i++)
             {
                 LotOfMembers lom;
                 LotOfMembersAutoFaker faker = new LotOfMembersAutoFaker();
                 lom = faker.Generate();

                 IncInDic(intCounts, lom.Int);
                 IncInDic(byteCounts, lom.Byte);
                 IncInDic(shortCounts, lom.Short);
                 IncInDic(dateTimeCounts, lom.DateTime);
                 IncInDic(doubleCounts, lom.Double);
                 IncInDic(guidCounts, lom.Guid);
                 IncInDic(igIntCounts, lom.IgnoredInt);
                 IncInDic(igStringCounts, lom.IgnoredString);

                 Console.WriteLine(lom);

             }
             CheckDic(intCounts, numIterations);
             CheckDic(byteCounts, numIterations);
             CheckDic(shortCounts, numIterations);
             CheckDic(dateTimeCounts, numIterations);
             CheckDic(doubleCounts, numIterations);
             CheckDic(guidCounts, numIterations);
             CheckDic(igIntCounts, numIterations);
             CheckDic(igStringCounts, numIterations);
        }

        [TestMethod]
        public void CreateAutoFakerBasicTest()
        {
            AutoFaker<ContainsValue> autoFaker = AutoFaker<ContainsValue>.CreateAutoFaker();
            ContainsValue cv = autoFaker.Generate();
            Console.WriteLine(cv);
        }
        [TestMethod]
        public void CreateAutoFakerNested()
        {
            AutoFaker<UpperNested> autoFaker = AutoFaker<UpperNested>.CreateAutoFaker();
            UpperNested un = autoFaker.Generate();
            Console.WriteLine(un);
        }
    }
}



