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
                RuleFor(x => x.InnerInt, rg => rg.Random.Int(upper: 42));
            }
            
        }
        public class NestedClassFaker : BaseFaker<NestedClass>
        {
            public NestedClassFaker()
            {
                SetFakerFor(x => x.Inner).Faker(new InnerClassFaker());
                RuleFor(x => x.OuterByte, rg => rg.Random.Byte());
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
