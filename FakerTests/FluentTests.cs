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
                //RuleFor(x => x.OuterByte, rg => rg.Random.Byte());
                SetRuleFor(x => x.OuterByte).Rule(rg => rg.Random.Byte());
            }
        }

        public class LotOfMembers
        {
            public int Int;
            public byte Byte;
            public long Long;
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

        public class LotOfMembersCoditionalFaker : BaseFaker<LotOfMembers>
        {
            public LotOfMembersCoditionalFaker()
            {
                For(x => x.Int).SetRule(rg => rg.Random.Int()).
                    When(x => x == 42).
                    For(x => x.Long).SetRule(rg => rg.Random.Long()).
                    Otherwise().For(x => x.Long).SetRule(_ => 73);
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
    }
}
