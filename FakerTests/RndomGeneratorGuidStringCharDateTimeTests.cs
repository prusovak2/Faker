using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;

namespace FakerTests
{
    [TestClass]
    public class RndomGeneratorGuidStringCharDateTimeTests
    {
        [TestMethod]
        public void RandomGuidTest()
        {
            /* for big indian test
            upperBytes = new byte[8] { 0, 0, 0, 1, 2, 3, 4, 5 };
            middleBytes = new byte[8] { 0, 0, 0, 6, 7, 8, 9, 10 };
            lowerBytes = new byte[8] { 0, 0, 11, 12, 13, 14, 15, 16 };*/
            HashSet<Guid> areUnique = new HashSet<Guid>();
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 100; i++)
            {
                Guid g = r.RandomGuid();
                Assert.IsInstanceOfType(g, typeof(Guid));
                areUnique.Add(g);
                Console.WriteLine(g);
            }
        }
    }
}
