﻿using System;
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
                For(x => x.InnerInt).SetRule(rg => rg.Random.Int(upper: 42));
            }
            
        }
        public class NestedClassFaker : BaseFaker<NestedClass>
        {
            public NestedClassFaker()
            {
                SetFakerFor(x => x.Inner).As(new InnerClassFaker());
                For(x => x.OuterByte).SetRule(rg => rg.Random.Byte());
            }
        }

        public class FewMembers
        {
            public sbyte Sbyte = 42;
            public short Short { get; set; } = 42;
            public int Int = 42;
            public long Long { get; set; } = 42;
            public bool Bool = true;

            public override string ToString()
            {
                return InstanceToString(this);
            }
        }
        public class FewMembersChainedIgnoreFaker : AutoFaker<FewMembers>
        {
            public FewMembersChainedIgnoreFaker()
            {
                For(x => x.Int).SetRule(_ => 73)
                    .When(x => x == 73).For(x => x.Long).Ignore();
            }
        }
        public class IgnoreVsIgnoredFaker : AutoFaker<FewMembers>
        {
            public IgnoreVsIgnoredFaker()
            {
                //won't compile
                //For(x => x.Int).Ignore();
            }
        }

        public class ChainedIgnoreFaker: AutoFaker<FewMembers>
        {
            public ChainedIgnoreFaker()
            {
                Ignore(x => x.Sbyte);
                For(x => x.Int).SetRule(_ => 73)
                    .When(x => x == 73).For(x => x.Long).Ignore()
                    .When(x => x == 42).For(x => x.Sbyte).Ignore();
                Ignore(x=>x.Long);
            }
        }

        public class InnerClassChainedIgnore : AutoFaker<NestedClass>
        {
            public InnerClassChainedIgnore()
            {
                For(x => x.OuterByte).SetRule(_ => 42)
                    .When(x => x == 73).For(x => x.Inner).Ignore();
            }
        }

        public class FewMembersChainedAutoFaker : BaseFaker<FewMembers>
        {
            public FewMembersChainedAutoFaker()
            {
                For(x => x.Sbyte).SetRule(rg => rg.Pick<sbyte>(0, 1, 2))
                    .When(c => c == 0).For(x => x.Short).SetRule(_ => 73)
                    .When(c => c == 1).For(x => x.Int).SetRule(_ => 73)
                    .When(c => c == 2).For(x => x.Long).SetRule(_ => 73);
            }
        }
        public class FewMembersChainedStrictFaker : StrictFaker<FewMembers>
        {
            public FewMembersChainedStrictFaker()
            {
                For(x => x.Sbyte).SetRule(rg => rg.Pick<sbyte>(0, 1, 2))
                    .When(c => c == 0).For(x => x.Short).SetRule(_ => 73)
                    .When(c => c == 1).For(x => x.Int).SetRule(_ => 73)
                    .When(c => c == 2).For(x => x.Long).SetRule(_ => 73);
                Ignore(x => x.Bool);
            }
        }

        public class ConditionalAndUncoditionalRulesFaker : BaseFaker<FewMembers>
        {
            public ConditionalAndUncoditionalRulesFaker()
            {
                For(x => x.Sbyte).SetRule(rg => rg.Pick<sbyte>(0, 1, 2))
                    .When(c => c == 0).For(x => x.Short).SetRule(_ => 73)
                    .When(c => c == 1).For(x => x.Int).SetRule(_ => 73)
                    .When(c => c == 2).For(x => x.Long).SetRule(_ => 73);
                For(x => x.Short).SetRule(_ => 1);
                For(x => x.Int).SetRule(_ => 1);
                For(x => x.Long).SetRule(_ => 1);
            }
        }

        public class SetFakerAndCondRule : BaseFaker<NestedClass>
        {
            public SetFakerAndCondRule()
            {
                SetFakerFor(x => x.Inner).As(new InnerClassFaker());
                For(x => x.OuterByte).SetRule(_ => 42)
                    .When(c => c == 42).For(x => x.Inner).SetRule(_ => new InnerClass());
            }
        }

        public class CondRuleAndSetFaker : BaseFaker<NestedClass>
        {
            public CondRuleAndSetFaker()
            {
                For(x => x.OuterByte).SetRule(_ => 42)
                    .When(c => c == 42).For(x => x.Inner).SetRule(_ => new InnerClass());
                SetFakerFor(x => x.Inner).As(new InnerClassFaker());
            }
        }

        public class IgnoreAndCondRule : AutoFaker<FewMembers>
        {
            public IgnoreAndCondRule()
            {
                For(x => x.Sbyte).SetRule(rg => rg.Pick<sbyte>(0, 1))
                    .When(c => c == 0).For(x => x.Short).SetRule(rg => rg.Random.Short());
                Ignore(x => x.Short);
            }
        }

        public class CondRuleIgnore : AutoFaker<FewMembers>
        {
            public CondRuleIgnore()
            {
                For(x => x.Sbyte).SetRule(rg => rg.Pick<sbyte>(0,1))
                    .When(c => c == 0).For(x => x.Short).SetRule(rg => rg.Random.Short());
                Ignore(x => x.Short);
            }
        }
        
        public class SetFakerAndFor : AutoFaker<NestedClass>
        {
            public SetFakerAndFor()
            {
                SetFakerFor(x => x.Inner).As(new InnerClassFaker());
                For(x => x.Inner).SetRule(_ => new InnerClass())
                   .When(c => true).For(x => x.OuterByte).SetRule(_ => 42);
            }
        }

        public class ForAndSetFaker : AutoFaker<NestedClass>
        {
            public ForAndSetFaker()
            {
                For(x => x.Inner).SetRule(_ => new InnerClass())
                   .When(c => true).For(x => x.OuterByte).SetRule(_ => 42);
                SetFakerFor(x => x.Inner).As(new InnerClassFaker());
            }
        }

        public class SetRuleForAndFor : AutoFaker<FewMembers>
        {
            public SetRuleForAndFor()
            {
                For(x => x.Sbyte).SetRule(rg => rg.Random.Sbyte());
                For(x => x.Sbyte).SetRule(_ => 42);

               
            }
        }

        public class ForAndSetRule : AutoFaker<FewMembers>
        {
            public ForAndSetRule()
            {
                For(x => x.Sbyte).SetRule(_ => 42)
                    .When(x => x == 42).For(x => x.Long).SetRule(_ => 42);
                For(x => x.Sbyte).SetRule(rg => rg.Random.Sbyte());
            }
        }

        public class IgnoreAndFor : AutoFaker<FewMembers>
        {
            public IgnoreAndFor()
            {
                Ignore(x => x.Sbyte);
                For(x => x.Sbyte).SetRule(_ => 42);
            }
        }

        public class ForAndIgnore : AutoFaker<FewMembers>
        {
            public ForAndIgnore()
            {
                For(x => x.Sbyte).SetRule(_ => 42)
                   .When(x => x == 42).For(x => x.Long).SetRule(_ => 42);
                Ignore(x => x.Sbyte);

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

                For(x => x.Short).SetRule(rg => rg.Random.Short());

                For(x => x.Long).SetRule(rg => rg.Random.Long(0, 5))
                    .When(c => c == 1).For(x => x.IgnoredInt).SetRule(rg => rg.Random.Int(0, 10))
                    .When(c => c == 2).For(x => x.IgnoredInt).SetRule(rg => rg.Random.Int(11, 20));

            }
        }

        public class LotOfMembersConditionalAutoFaker : AutoFaker<LotOfMembers>
        {
            public LotOfMembersConditionalAutoFaker()
            {
                For(x => x.Bool).SetRule(rg => rg.Random.Bool())
                   .When(c => c).For(x => x.IgnoredString).SetRule(_ => "FILLED")
                   .When(c =>!c).For(x => x.IgnoredString).SetRule(_ => "EMPTY");

                Ignore(x => x.IgnoredInt);
                Ignore(x => x.IgnoredString);

                For(x => x.Long).SetRule(rg => rg.Pick<long>(0, 1, 2))
                    .When(c => c == 0).For(x => x.Int).SetRule(_ => 0)
                    .When(c => c == 1).For(x => x.Int).SetRule(_ => 1)
                    .When(c => c == 2).For(x => x.Int).SetRule(_ => 2)
                    .Otherwise().For(x => x.Int).SetRule(_ => 73);
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

        public class AutoFakerAsInnerFaker : BaseFaker<NestedClass>
        {
            public AutoFakerAsInnerFaker()
            {
                SetFakerFor(x => x.Inner).As(new AutoFaker<InnerClass>());
            }
        }

        public class SimpleMultipleRulesForConditionAuto : AutoFaker<FewMembers>
        {
            public SimpleMultipleRulesForConditionAuto()
            {
                For(x => x.Bool).SetRule(rg => rg.Random.Bool())
                    .When(x => x)
                        .For(x => x.Int).SetRule(_ => 1)
                        .For(x => x.Long).SetRule(_ => 1)
                        .For(x => x.Sbyte).SetRule(_ => 1)
                    .Otherwise()
                        .For(x => x.Short).SetRule(_ => 73)
                        .For(x => x.Sbyte).SetRule(_ => 73);
            }
        }

        public class SimpleMultipleRulesForConditionBase : BaseFaker<FewMembers>
        {
            public SimpleMultipleRulesForConditionBase()
            {
                For(x => x.Bool).SetRule(rg => rg.Random.Bool())
                    .When(x => x)
                        .For(x => x.Int).SetRule(_ => 1)
                        .For(x => x.Long).SetRule(_ => 1)
                        .For(x => x.Sbyte).SetRule(_ => 1)
                    .Otherwise()
                        .For(x => x.Short).SetRule(_ => 73)
                        .For(x => x.Sbyte).SetRule(_ => 73);
            }
        }

        public class SimpleMultipleRulesForConditionStrict : StrictFaker<FewMembers>
        {
            public SimpleMultipleRulesForConditionStrict()
            {
                For(x => x.Bool).SetRule(rg => rg.Random.Bool())
                    .When(x => x)
                        .For(x => x.Int).SetRule(_ => 1)
                        .For(x => x.Long).SetRule(_ => 1)
                        .For(x => x.Sbyte).SetRule(_ => 1)
                    .Otherwise()
                        .For(x => x.Short).SetRule(_ => 73)
                        .For(x => x.Sbyte).SetRule(_ => 73);
            }
        }
        public class LotRulesPerConditionAuto : AutoFaker<LotOfMembers>
        {
            public LotRulesPerConditionAuto()
            {
                For(x => x.Bool).SetRule(_ => true)
                    .When(x => x)
                        .For(x => x.Byte).SetRule(_ => 42)
                        .For(x => x.DateTime).SetRule(rg => rg.Random.DateTime())
                        .For(x => x.Guid).SetRule(rg => rg.Random.Guid())
                        .For(x => x.IgnoredInt).Ignore()
                        .For(x => x.IgnoredString).Ignore()
                        .For(x => x.Long).SetRule(_ => 42);
            }
        }
        public class LotRulesPerConditionBase : BaseFaker<LotOfMembers>
        {
            public LotRulesPerConditionBase()
            {
                For(x => x.Bool).SetRule(_ => true)
                    .When(x => x)
                        .For(x => x.Byte).SetRule(_ => 42)
                        .For(x => x.DateTime).SetRule(rg => rg.Random.DateTime())
                        .For(x => x.Guid).SetRule(rg => rg.Random.Guid())
                        .For(x => x.IgnoredInt).SetRule(_ => 73)
                        .For(x => x.IgnoredString).SetRule(_=> "ABRAKA")
                        .For(x => x.Long).SetRule(_ => 42);
            }
        }

        [TestMethod]
        public void AutoLotRulePerCondition()
        {
            /*
             For(x => x.Bool).SetRule(_ => true)
                    .When(x => x)
                        .For(x => x.Byte).SetRule(_ => 42)
                        .For(x => x.DateTime).SetRule(rg => rg.Random.DateTime())
                        .For(x => x.Guid).SetRule(rg => rg.Random.Guid())
                        .For(x => x.IgnoredInt).Ignore()
                        .For(x => x.IgnoredString).Ignore()
                        .For(x => x.Long).SetRule(_ => 42);
            */
            LotRulesPerConditionAuto faker = new();
            int numIterations = 20;
            Dictionary<DateTime, int> dateTimeCounts = new();
            Dictionary<Guid, int> giudCounts = new();
            Dictionary<short, int> shortCounts = new();
            for (int i = 0; i < numIterations; i++)
            {
                LotOfMembers lom = faker.Generate();
                Console.WriteLine(lom);
                Console.WriteLine();

                Assert.AreEqual(42, lom.Byte);
                IncInDic(dateTimeCounts, lom.DateTime);
                IncInDic(giudCounts, lom.Guid);
                IncInDic(shortCounts, lom.Short);
                Assert.AreEqual(42, lom.IgnoredInt);
                Assert.AreEqual("IGNORED", lom.IgnoredString);
                Assert.AreEqual(42, lom.Long);
            }
            CheckDic(dateTimeCounts, numIterations);
            CheckDic(giudCounts, numIterations);
            CheckDic(shortCounts, numIterations);
        }

        [TestMethod]
        public void BaseLotRulePerCondition()
        {
            /*
              For(x => x.Bool).SetRule(_ => true)
                    .When(x => x)
                        .For(x => x.Byte).SetRule(_ => 42)
                        .For(x => x.DateTime).SetRule(rg => rg.Random.DateTime())
                        .For(x => x.Guid).SetRule(rg => rg.Random.Guid())
                        .For(x => x.IgnoredInt).SetRule(_ => 73)
                        .For(x => x.IgnoredString).SetRule(_=> "ABRAKA")
                        .For(x => x.Long).SetRule(_ => 42);
            */
            LotRulesPerConditionBase faker = new();
            int numIterations = 20;
            Dictionary<DateTime, int> dateTimeCounts = new();
            Dictionary<Guid, int> giudCounts = new();
            for (int i = 0; i < numIterations; i++)
            {
                LotOfMembers lom = faker.Generate();
                Console.WriteLine(lom);
                Console.WriteLine();

                Assert.AreEqual(42, lom.Byte);
                IncInDic(dateTimeCounts, lom.DateTime);
                IncInDic(giudCounts, lom.Guid);
                Assert.AreEqual(73, lom.IgnoredInt);
                Assert.AreEqual("ABRAKA", lom.IgnoredString);
                Assert.AreEqual(42, lom.Long);
            }
            CheckDic(dateTimeCounts, numIterations);
            CheckDic(giudCounts, numIterations);
        }


        [TestMethod]
        public void AutoMultipleRulesPerConditionBasicTest()
        {
            /*
             For(x => x.Bool).SetRule(rg => rg.Random.Bool())
                    .When(x => x)
                        .For(x => x.Int).SetRule(_ => 1)
                        .For(x => x.Long).SetRule(_ => 1)
                        .For(x => x.Sbyte).SetRule(_ => 1)
                    .Otherwise()
                        .For(x => x.Short).SetRule(_ => 73)
                        .For(x => x.Sbyte).SetRule(_ => 73);
            */
            SimpleMultipleRulesForConditionAuto faker = new();
            for (int i = 0; i < 20; i++)
            {
                FewMembers fm = faker.Generate();
                Console.WriteLine(fm);
                Console.WriteLine();
                if (fm.Bool)
                {
                    Assert.AreEqual(1, fm.Int);
                    Assert.AreEqual(1, fm.Long);
                    Assert.AreEqual(1, fm.Sbyte);
                }
                else
                {
                    Assert.AreEqual(73, fm.Short);
                    Assert.AreEqual(73, fm.Sbyte);
                }
            }
        }

        [TestMethod]
        public void BaseMultipleRulesPerConditionBasicTest()
        {
            /*
             For(x => x.Bool).SetRule(rg => rg.Random.Bool())
                    .When(x => x)
                        .For(x => x.Int).SetRule(_ => 1)
                        .For(x => x.Long).SetRule(_ => 1)
                        .For(x => x.Sbyte).SetRule(_ => 1)
                    .Otherwise()
                        .For(x => x.Short).SetRule(_ => 73)
                        .For(x => x.Sbyte).SetRule(_ => 73);
            */
            SimpleMultipleRulesForConditionBase faker = new();
            for (int i = 0; i < 20; i++)
            {
                FewMembers fm = faker.Generate();
                Console.WriteLine(fm);
                Console.WriteLine();
                if (fm.Bool)
                {
                    Assert.AreEqual(1, fm.Int);
                    Assert.AreEqual(1, fm.Long);
                    Assert.AreEqual(1, fm.Sbyte);
                }
                else
                {
                    Assert.AreEqual(73, fm.Short);
                    Assert.AreEqual(73, fm.Sbyte);
                }
            }
        }
        [TestMethod]
        public void StrictMultipleRulesPerConditionBasicTest()
        {
            /*
             For(x => x.Bool).SetRule(rg => rg.Random.Bool())
                    .When(x => x)
                        .For(x => x.Int).SetRule(_ => 1)
                        .For(x => x.Long).SetRule(_ => 1)
                        .For(x => x.Sbyte).SetRule(_ => 1)
                    .Otherwise()
                        .For(x => x.Short).SetRule(_ => 73)
                        .For(x => x.Sbyte).SetRule(_ => 73);
            */
            SimpleMultipleRulesForConditionStrict faker = new();
            for (int i = 0; i < 20; i++)
            {
                FewMembers fm = faker.Generate();
                Console.WriteLine(fm);
                Console.WriteLine();
                if (fm.Bool)
                {
                    Assert.AreEqual(1, fm.Int);
                    Assert.AreEqual(1, fm.Long);
                    Assert.AreEqual(1, fm.Sbyte);
                }
                else
                {
                    Assert.AreEqual(73, fm.Short);
                    Assert.AreEqual(73, fm.Sbyte);
                }
            }
        }


        [TestMethod]
        public void ChainedIgnoreTest()
        {
            /*
            Ignore(x => x.Long);
                For(x => x.Int).SetRule(_ => 73)
                    .When(x => x == 73).For(x => x.Long).Ignore()
                    .When(x => x == 42).For(x => x.Sbyte).Ignore();
            */
            ChainedIgnoreFaker faker = new();
            int numIterations = 20;
            Dictionary<bool, int> boolCounts = new();
            Dictionary<short, int> shortCounts = new();
            for (int i = 0; i < numIterations; i++)
            {
                FewMembers fm = faker.Generate();
                Console.WriteLine(fm);
                Console.WriteLine();
                Assert.AreEqual(73, fm.Int);
                Assert.AreEqual(42, fm.Long);
                Assert.AreEqual(42, fm.Sbyte);
                IncInDic(boolCounts, fm.Bool);
                IncInDic(shortCounts, fm.Short);
            }
            CheckDic(boolCounts, numIterations);
            CheckDic(shortCounts, numIterations);
        }



        [TestMethod]
        public void ChainedIgnoreBasicTest()
        {
            /*
            For(x => x.Int).SetRule(_ => 73)
                    .When(x => x == 73).For(x => x.Long).Ignore();
            */
            FewMembersChainedIgnoreFaker faker = new();
            int numIterations = 20;
            for (int i = 0; i < numIterations; i++)
            {
                FewMembers fm = faker.Generate();
                Console.WriteLine(fm);
                Console.WriteLine();
                Assert.AreEqual(73, fm.Int);
                Assert.AreEqual(42, fm.Long);
            }
        }

        [TestMethod]
        public void AutoFakerAsInnerFakerTest()
        {
            AutoFakerAsInnerFaker faker = new();
            int numIterations = 20;
            Dictionary<int, int> innerIntCount = new();
            for (int i = 0; i < numIterations; i++)
            {
                NestedClass n = faker.Generate();
                Console.WriteLine(n);
                Console.WriteLine();
                IncInDic(innerIntCount, n.Inner.InnerInt);
            }
            CheckDic(innerIntCount, numIterations);
        }

        [TestMethod]
        public void ChainedRulesExceptionsTest()
        {
            //should not throw the exception
            CondRuleAndSetFaker f1 = new();
            var i1 = f1.Generate();

            SetFakerAndCondRule f2 = new();
            var i2 = f2.Generate();

            FewMembersChainedStrictFaker f3 = new();
            var i3 = f3.Generate();
            Assert.IsTrue(f3.AllRulesSetDeep());
            Assert.IsTrue(f3.AllRulesSetShallow());

            InnerClassChainedIgnore f4 = new();
            NestedClass n = f4.Generate();
            Console.WriteLine(n);

            Assert.ThrowsException<FakerException>(() => { SetFakerAndFor f = new(); });
            Assert.ThrowsException<FakerException>(() => { ForAndSetFaker f = new(); });
            Assert.ThrowsException<FakerException>(() => { SetRuleForAndFor f = new(); });
            Assert.ThrowsException<FakerException>(() => { ForAndSetRule f = new(); });
            Assert.ThrowsException<FakerException>(() => { IgnoreAndFor f = new(); });
            Assert.ThrowsException<FakerException>(() => { ForAndIgnore f = new(); });
        }

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
        [TestMethod]
        public void ChainedRulesAutoFakerTest()
        {
            LotOfMembersConditionalAutoFaker faker = new();
            int numIterations = 20;
            Dictionary<double, int> doubleCounts = new();
            Dictionary<short, int> shortCounts = new();
            Dictionary<Guid, int> guidCounts = new();
            Dictionary<DateTime, int> datetimeCounts = new();
            for (int i = 0; i < numIterations; i++)
            {
                LotOfMembers lom = faker.Generate();
                Console.WriteLine(lom);
                Console.WriteLine();
                /*
                For(x => x.Bool).SetRule(rg => rg.Random.Bool())
                       .When(c => c).For(x => x.IgnoredString).SetRule(_ => "FILLED")
                       .When(c => !c).For(x => x.IgnoredString).SetRule(_ => "EMPTY");
                */
                if (lom.Bool)
                {
                    Assert.AreEqual("FILLED", lom.IgnoredString);
                }
                else
                {
                    Assert.AreEqual("EMPTY", lom.IgnoredString);
                }
                //Ignore(x => x.IgnoredInt);
                Assert.AreEqual(42, lom.IgnoredInt);
                /*
                 For(x => x.Long).SetRule(rg => rg.Pick<long>(0, 1, 2))
                    .When(c => c == 0).For(x => x.Int).SetRule(_ => 0)
                    .When(c => c == 1).For(x => x.Int).SetRule(_ => 1)
                    .When(c => c == 2).For(x => x.Int).SetRule(_ => 2)
                    .Otherwise().For(x => x.Int).SetRule(_ => 73);
                */
                if(lom.Long == 0)
                {
                    Assert.AreEqual(0, lom.Int);
                }
                else if(lom.Long == 1)
                {
                    Assert.AreEqual(1, lom.Int);
                }
                else if(lom.Long == 2)
                {
                    Assert.AreEqual(2, lom.Int);
                }
                else
                {
                    Assert.AreEqual(73, lom.Int);
                }
                IncInDic(doubleCounts, lom.Double);
                IncInDic(shortCounts, lom.Short);
                IncInDic(guidCounts, lom.Guid);
                IncInDic(datetimeCounts, lom.DateTime);
            }
            CheckDic(doubleCounts, numIterations);
            CheckDic(shortCounts, numIterations);
            CheckDic(guidCounts, numIterations);
            CheckDic(datetimeCounts, numIterations);
        }

        [TestMethod]
        public void ConditionAffectingMultipleMemebers()
        {
            FewMembersChainedAutoFaker faker = new();
            int numIterations = 20;
            /*
            For(x => x.Sbyte).SetRule(rg => rg.Pick<sbyte>(0, 1, 2))
                    .When(c => c == 0).For(x => x.Short).SetRule(_ => 73)
                    .When(c => c == 1).For(x => x.Int).SetRule(_ => 73)
                    .When(c => c == 2).For(x => x.Long).SetRule(_ => 73);
            */
            for (int i = 0; i < numIterations; i++)
            {
                FewMembers fm = faker.Generate();
                if(fm.Sbyte == 0)
                {
                    Assert.AreEqual(73, fm.Short);
                    Assert.AreEqual(42, fm.Int);
                    Assert.AreEqual(42, fm.Long);
                }
                else if (fm.Sbyte == 1)
                {
                    Assert.AreEqual(42, fm.Short);
                    Assert.AreEqual(73, fm.Int);
                    Assert.AreEqual(42, fm.Long);
                }
                else if (fm.Sbyte == 2)
                {
                    Assert.AreEqual(42, fm.Short);
                    Assert.AreEqual(42, fm.Int);
                    Assert.AreEqual(73, fm.Long);
                }
                else
                {
                    Assert.AreEqual(42, fm.Short);
                    Assert.AreEqual(42, fm.Int);
                    Assert.AreEqual(42, fm.Long);
                }
            }
        }
        [TestMethod]
        public void ConditionalAnduncoditionalRulesTest()
        {
            /*
            For(x => x.Sbyte).SetRule(rg => rg.Pick<sbyte>(0, 1, 2))
                    .When(c => c == 0).For(x => x.Short).SetRule(_ => 73)
                    .When(c => c == 1).For(x => x.Int).SetRule(_ => 73)
                    .When(c => c == 2).For(x => x.Long).SetRule(_ => 73);
            SetRuleFor(x => x.Short).Rule(_ => 1);
            SetRuleFor(x => x.Int).Rule(_ => 1);
            For(x => x.Long).SetRule(_ => 1);
            */
            int numIterations = 20;
            ConditionalAndUncoditionalRulesFaker faker = new();
            for (int i = 0; i < numIterations; i++)
            {
                FewMembers fm = faker.Generate();
                Console.WriteLine(fm);
                Console.WriteLine();

                Assert.AreEqual(1, fm.Short);
                Assert.AreEqual(1, fm.Int);
                Assert.AreEqual(1, fm.Long);
            }
        }
        [TestMethod]
        public void CondRuleAndIgnoreTest()
        {
            CondRuleIgnore faker1 = new();
            /*
            For(x => x.Sbyte).SetRule(rg => rg.Pick<sbyte>(0,1))
                    .When(c => c == 0).For(x => x.Short).SetRule(rg => rg.Random.Short());
                Ignore(x => x.Short);
            */
            IgnoreAndCondRule faker2 = new();
            /*
            For(x => x.Sbyte).SetRule(rg => rg.Pick<sbyte>(0, 1))
                    .When(c => c == 0).For(x => x.Short).SetRule(rg => rg.Random.Short());
                Ignore(x => x.Short);
            */
            int numIterations = 20;
            Dictionary<short, int> fm1Counts = new();
            Dictionary<short, int> fm2Counts = new();
            for (int i = 0; i < numIterations; i++)
            {
                FewMembers fm1 = faker1.Generate();
                Console.WriteLine(fm1);
                FewMembers fm2 = faker2.Generate();
                Console.WriteLine(fm2);
                Console.WriteLine();

                if(fm1.Sbyte == 1)
                {
                    Assert.AreEqual(42, fm1.Short);
                }
                else
                {
                    IncInDic(fm1Counts, fm1.Short);
                }
                if (fm2.Sbyte == 1)
                {
                    Assert.AreEqual(42, fm2.Short);
                }
                else
                {
                    IncInDic(fm2Counts, fm2.Short);
                }
            }
            CheckDic(fm1Counts, numIterations);
            CheckDic(fm2Counts, numIterations);
        }
    }
}
