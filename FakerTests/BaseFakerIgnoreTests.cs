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
            return $"IgnoredPerson:\n \tName: {Name}\n\t FakeThisByte: {FakeThisByte}\n\t PropThatShouldNotBeFaked: {ProprertyThatShouldNotBeFaked}\n\t FieldThatShouldNotBeFaked: {FieldThatShouldNotBeFaked}\n";
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
                if(person.FakeThisByte == 73)
                {
                    counter73++;
                }
                Assert.AreEqual(42, person.ProprertyThatShouldNotBeFaked);
                Assert.AreEqual("NO FAKE", person.FieldThatShouldNotBeFaked);
            }
            Assert.AreNotEqual(numIterations, counter73);
            
        }
    }
}
