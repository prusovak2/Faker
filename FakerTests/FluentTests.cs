using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using static FakerTests.TestUtils;


namespace FakerTests
{
    [TestClass]
    public class FluentTests
    {
        public class InnerClass
        {
            public int InnerInt { get; set; }
            public override string ToString()
            {
                return InstanceToString(this);
            }
        }

        public class NestedClass
        {
            public InnerClass Inner { get; set; }
            public byte OuterByte;
            public override string ToString()
            {
                return InstanceToString(this);
            }
        }

        public class InnerClassFaker : BaseFaker<InnerClass>
        {
            public InnerClassFaker()
            {
                //RuleFor(x => x.InnerInt, rg => rg.Random.Int(upper: 42));
                SetRuleFor(x => x.InnerInt).Rule(rg => rg.Random.Int(upper: 42));
            }
            
        }
        public class NestedClassFaker : BaseFaker<NestedClass>
        {
            public NestedClassFaker()
            {
                SetFakerFor(x => x.Inner).Faker(new InnerClassFaker());
                SetRuleFor(x => x.OuterByte).Rule(rg => rg.Random.Byte());
            }
        }

        public class LotOfMembers
        {
            public int Int;
            public byte Byte;
            public long Long;
            public bool Bool { get; set; }
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
        public class LotOfMembersConditionalFaker : BaseFaker<LotOfMembers>
        {
            public LotOfMembersConditionalFaker()
            {
                For(x => x.Bool).SetRule(rg => rg.Random.Bool())
                    .When(c => c).For(x => x.IgnoredString).SetRule(_ => "FILLED")
                    .Otherwise().For(x => x.IgnoredString).SetRule(_ => "EMPTY");

                For(x => x.Byte).SetRule(rg => rg.Pick<Byte>(0, 1, 2))
                    .When(x => x == 0).For(x => x.Double).SetRule(x => x.Random.Int())
                    .When(x => x == 1).For(x => x.Double).SetRule(_ => 4.2)
                    .Otherwise().For(x => x.Double).SetRule(_ => 7.3);

                SetRuleFor(x => x.Short).Rule(rg => rg.Random.Short());

                For(x => x.Long).SetRule(rg => rg.Random.Long(0, 5))
                    .When(c => c == 1).For(x => x.IgnoredInt).SetRule(rg => rg.Random.Int(0, 10))
                    .When(c => c == 2).For(x => x.IgnoredInt).SetRule(rg => rg.Random.Int(11, 20));
            }
        }

        public class SimpleConditionClass
        {
            public int ConditonInt { get; set; }
            public long DependenLong;

            public override string ToString()
            {
                return InstanceToString(this);
            }
        }

        public class SimpleCondClassFaker : BaseFaker<SimpleConditionClass>
        {
            public SimpleCondClassFaker()
            {
                For(x => x.ConditonInt).SetRule(rg => rg.Pick(1,2)).
                    When(x => x == 1).
                    For(x => x.DependenLong).SetRule(_ => 42).
                    Otherwise().For(x => x.DependenLong).SetRule(_ => 73);
            }
        }

        //TODO: far more tests
        [TestMethod]
        public void FluentSetFakerBasicTest()
        {
            NestedClassFaker faker = new();
            NestedClass n = faker.Generate();
            Console.WriteLine(n);
        }
        [TestMethod]
        public void ChainedRulesBasicTest()
        {
            SimpleCondClassFaker faker = new();
            for (int i = 0; i < 15; i++)
            {
                SimpleConditionClass scc = faker.Generate();
                Console.WriteLine(scc);
                Console.WriteLine();
                if (scc.ConditonInt == 1)
                {
                    Assert.AreEqual(42L, scc.DependenLong);
                }
                else if(scc.ConditonInt == 2)
                {
                    Assert.AreEqual(73L, scc.DependenLong);
                }
                else
                {
                    Assert.Fail();   //.Pick should only pick from {1,2} set 
                }
            }
        }
        [TestMethod]
        public void ChainedRuleComplexTest()
        {
           
            LotOfMembersConditionalFaker faker = new();
            int numIterations = 20;
            Dictionary<double, int> doubleCounts = new();
            Dictionary<short, int> shortCount = new();
            Dictionary<int, int> ignoredIntCount = new();
            for (int i = 0; i < numIterations; i++)
            {
                LotOfMembers lom = faker.Generate();
                Console.WriteLine(lom);
                Console.WriteLine();
                /*
                For(x => x.Bool).SetRule(rg => rg.Random.Bool())
                   .When(c => c).For(x => x.IgnoredString).SetRule(_ => "FILLED")
                   .Otherwise().For(x => x.IgnoredString).SetRule(_ => "EMPTY");
                */
                if (lom.Bool)
                {
                    Assert.AreEqual("FILLED", lom.IgnoredString);
                }
                else
                {
                    Assert.AreEqual("EMPTY", lom.IgnoredString);
                }
                /*
                 For(x => x.Byte).SetRule(rg => rg.Pick<Byte>(0, 1, 2))
                    .When(x => x == 0).For(x => x.Double).SetRule(x => x.Random.Int())
                    .When(x => x == 1).For(x => x.Double).SetRule(_ => 4.2)
                    .Otherwise().For(x => x.Double).SetRule(_ => 7.3);
                */
                if(lom.Byte == 0)
                {
                    IncInDic(doubleCounts, lom.Double);
                }
                else if(lom.Byte == 1)
                {
                    Assert.IsTrue(lom.Double.EpsilonEquals(4.2));    
                }
                else
                {
                    Assert.IsTrue(lom.Double.EpsilonEquals(7.3));
                }
                //SetRuleFor(x => x.Short).Rule(rg => rg.Random.Short());
                IncInDic(shortCount, lom.Short);

                /*
                For(x => x.Long).SetRule(rg => rg.Random.Long(0, 5))
                    .When(c => c == 1).For(x => x.IgnoredInt).SetRule(rg => rg.Random.Int(0, 10))
                    .When(c => c == 2).For(x => x.IgnoredInt).SetRule(rg => rg.Random.Int(11, 20));
                */
                if(lom.Long == 1)
                {
                    Assert.IsTrue(lom.IgnoredInt >= 0 && lom.IgnoredInt <= 10);
                    IncInDic(ignoredIntCount, lom.IgnoredInt);
                }
                else if(lom.Long == 2)
                {
                    Assert.IsTrue(lom.IgnoredInt >= 11 && lom.IgnoredInt <= 20);
                    IncInDic(ignoredIntCount, lom.IgnoredInt);
                }
                else
                {
                    Assert.AreEqual(42, lom.IgnoredInt);
                }
            }
            CheckDic(doubleCounts, numIterations);
            CheckDic(shortCount, numIterations);
            CheckDic(ignoredIntCount, numIterations);
        }
    }
}
