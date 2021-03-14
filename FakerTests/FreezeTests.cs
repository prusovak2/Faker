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
    public class FreezeTests
    {
        public class LotOfMembersFakerAlmostEmpty : BaseFaker<LotOfMembers>
        {
             public LotOfMembersFakerAlmostEmpty()
            {
                For(x => x.Int).SetRule(rg => rg.Random.Int());
                For(x => x.Byte).SetRule(rg => rg.Random.Byte());
                CtorUsageFlag = InnerFakerConstructorUsage.GivenParameters;
            }
        }

        public class TwoMembers
        {

            public int Int;
            public double Double { get; set; }
            public override string ToString()
            {
                return InstanceToString(this);
            }
        }

        public class NestedClassFakerEmpty : BaseFaker<NestedClass> { }

        public class LotOfMembersAutoFakerEmpty : AutoFaker<LotOfMembers> { }

        public class TwoMembersStrictFaker : StrictFaker<TwoMembers> { }

        [TestMethod]
        public void FreezeTestGenerateBasic()
        {
            LotOfMembers lom;
            LotOfMembers lom2;
            NestedClass nc;

            LotOfMembersFakerAlmostEmpty faker = new();
            LotOfMembersAutoFakerEmpty fakerAutoEmpty = new();
            NestedClassFakerEmpty fakerNested = new();

            faker.For(x => x.Double).SetRule(rg => rg.Random.Double());
            fakerAutoEmpty.For(x => x.Double).SetRule(rg => rg.Random.Double());
            fakerNested.For(x => x.num).SetRule(rg => rg.Random.Int());

            lom = faker.Generate();
            lom2 = fakerAutoEmpty.Generate();
            nc = fakerNested.Generate();

            Assert.ThrowsException<InvalidOperationException>(() => { faker.For(x => x.Guid).SetRule(rg => rg.Random.Guid()); });
            Assert.ThrowsException<InvalidOperationException>(() => { fakerAutoEmpty.For(x => x.Guid).SetRule(rg => rg.Random.Guid()); });
            Assert.ThrowsException<InvalidOperationException>(() => { fakerNested.SetFakerFor(x => x.value).As(new BaseFaker<ValueClass>()); });
        }

        [TestMethod]
        public void FreezeTestBasic()
        {
            LotOfMembersFakerAlmostEmpty faker = new();
            LotOfMembersAutoFakerEmpty fakerAutoEmpty = new();
            TwoMembersStrictFaker fakerStrictEmpty = new();
            NestedClassFakerEmpty fakerNested = new();

            faker.For(x => x.Double).SetRule(rg => rg.Random.Double());
            fakerAutoEmpty.For(x => x.Double).SetRule(rg => rg.Random.Double());
            fakerStrictEmpty.For(x => x.Double).SetRule(rg => rg.Random.Double());
            fakerNested.For(x => x.num).SetRule(rg => rg.Random.Int());

            faker.Freeze();
            fakerAutoEmpty.Freeze();
            fakerStrictEmpty.Freeze();
            fakerNested.Freeze();

            Assert.ThrowsException<InvalidOperationException>(() => { faker.For(x => x.Guid).SetRule(rg => rg.Random.Guid()); });
            Assert.ThrowsException<InvalidOperationException>(() => { fakerStrictEmpty.For(x => x.Int).SetRule(rg => rg.Random.Int()); });
            Assert.ThrowsException<InvalidOperationException>(() => { fakerAutoEmpty.For(x => x.Guid).SetRule(rg => rg.Random.Guid()); });
            Assert.ThrowsException<InvalidOperationException>(() => { fakerNested.SetFakerFor(x => x.value).As(new BaseFaker<ValueClass>()); });
        }

        [TestMethod]
        public void FreezeTestPopulateBasic()
        {
            LotOfMembers lom = new();
            LotOfMembers lom2 = new();
            NestedClass nc = new();

            LotOfMembersFakerAlmostEmpty faker = new();
            LotOfMembersAutoFakerEmpty fakerAutoEmpty = new();
            NestedClassFakerEmpty fakerNested = new();

            faker.For(x => x.Double).SetRule(rg => rg.Random.Double());
            fakerAutoEmpty.For(x => x.Double).SetRule(rg => rg.Random.Double());
            fakerNested.For(x => x.num).SetRule(rg => rg.Random.Int());

            lom = faker.Populate(lom);
            lom2 = fakerAutoEmpty.Populate(lom2);
            nc = fakerNested.Populate(nc);

            Assert.ThrowsException<InvalidOperationException>(() => { faker.For(x => x.Guid).SetRule(rg => rg.Random.Guid()); });
            Assert.ThrowsException<InvalidOperationException>(() => { fakerAutoEmpty.For(x => x.Guid).SetRule(rg => rg.Random.Guid()); });
            Assert.ThrowsException<InvalidOperationException>(() => { fakerNested.SetFakerFor(x => x.value).As(new BaseFaker<ValueClass>()); });
        }
    }
}
