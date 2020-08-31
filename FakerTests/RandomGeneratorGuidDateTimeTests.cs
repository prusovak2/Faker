using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;

namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorGuidDateTimeTests
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
                Guid g = r.Random.Guid();
                Assert.IsInstanceOfType(g, typeof(Guid));
                areUnique.Add(g);
                Console.WriteLine(g);
            }
        }
        [TestMethod]
        public void RandomDateTimeTest()
        {
            RandomGenerator r = new RandomGenerator();
            HashSet<DateTime> areUnique = new HashSet<DateTime>();
            DateTime lower = new DateTime(2000,1,1);
            DateTime upper = new DateTime(6000,1,1);
            Console.WriteLine("lower-upper");
            for (int i = 0; i < 30; i++)
            {
                DateTime d = r.Random.DateTime(lower, upper);
                Console.WriteLine(d);
                Assert.IsTrue(d <= upper && d >= lower);
                areUnique.Add(d);
            }
            Console.WriteLine();
            areUnique.Clear();

            Console.WriteLine("lower-max");
            for (int i = 0; i < 30; i++)
            {
                DateTime d = r.Random.DateTime(lower, false);
                Console.WriteLine(d);
                Assert.IsTrue(d >= lower);
                areUnique.Add(d);
            }
            Console.WriteLine();
            areUnique.Clear();

            Console.WriteLine("lower-max");
            for (int i = 0; i < 30; i++)
            {
                DateTime d = r.Random.DateTime(upper, true);
                Console.WriteLine(d);
                Assert.IsTrue(d <= upper);
                areUnique.Add(d);
            }
            Console.WriteLine();
            areUnique.Clear();

            Console.WriteLine("min-max");
            for (int i = 0; i < 30; i++)
            {
                DateTime d = r.Random.DateTime();
                Console.WriteLine(d);
                Assert.IsTrue(d <= DateTime.MaxValue && d >= DateTime.MinValue);
                areUnique.Add(d);
            }
            Console.WriteLine();
            areUnique.Clear();

            for (int i = 0; i < 30; i++)
            {
                DateTime l = r.Random.DateTime(lower, true);
                DateTime h = r.Random.DateTime(lower, false);
                Console.WriteLine("{0}<={1}", l, h);
                Assert.IsTrue(l<=h);
                areUnique.Add(l);
                areUnique.Add(h);
            }
        }
       
    }
}
