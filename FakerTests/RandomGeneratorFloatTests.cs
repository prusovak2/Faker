using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;


namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorFloatTests
    {
        [TestMethod]
        public void FloatEpsilonEqualTest()
        {
            // Initialize two floats with apparently identical values
            float float1 = 0.3333333f;
            float float2 = 1f / 3f;
            bool b = float1.EpsilonEquals(float2);
            Assert.IsTrue(b);

            float1 = .33333f;
            float2 = 1f / 4f;
            b = float1.EpsilonEquals(float2);
            Assert.IsFalse(b);

            float1 = .33333f;
            float2 = 99999999999999990000000.1f;
            b = float1.EpsilonEquals(float2);
            Assert.IsFalse(b);

            float1 = 0;
            float2 = -0;
            b = float1.EpsilonEquals(float2);
            Assert.IsTrue(b);
        }
        [TestMethod]
        public void ZeroOneFloatTest()
        {
            RandomGenerator r = new RandomGenerator();
            RandomGenerator r2 = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                float rf = r.RandomZeroToOneFloat();
                float rf2 = r2.RandomZeroToOneFloat();
                Console.WriteLine("r:   {0}", rf);
                Console.WriteLine("r2: {0}", rf2);
                Assert.IsTrue(rf < 1 && rf >= 0);
                Assert.IsTrue(rf2 < 1 && rf2 >= 0);
            }
        }
        [TestMethod]
        public void RandomFloatRangeTest()
        {
            RandomGenerator r = new RandomGenerator();

            float lower = 1;
            float upper = 100000;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                float rf = r.RandomFloat(lower, upper);
                Assert.IsTrue(rf >= lower && rf < upper);
                Console.WriteLine(rf);
            }

            Console.WriteLine();
            lower = 1;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                float rf = r.RandomFloat(lower, upper);
                Assert.IsTrue(rf >= lower && rf < upper);
                Console.WriteLine(rf);
            }
            Console.WriteLine();
            lower = -100;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                float rf = r.RandomFloat(lower, upper);
                Assert.IsTrue(rf >= lower && rf < upper);
                Console.WriteLine(rf);
            }
            Console.WriteLine();
            lower = -10000;
            upper = -10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                float rf = r.RandomFloat(lower, upper);
                Assert.IsTrue(rf >= lower && rf < upper);
                Console.WriteLine(rf);
            }
            Console.WriteLine();
            lower = -10;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                float rf = r.RandomFloat(lower, upper);
                Assert.IsTrue(rf >= lower && rf < upper);
                Console.WriteLine(rf);
            }
        }
        [TestMethod]
        public void FloatRangeCornerCaseTest()
        {
            //min to max
            RandomGenerator r = new RandomGenerator();
            Console.WriteLine();
            float lower = float.MinValue;
            float upper = float.MaxValue;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                float rf = r.RandomFloat(lower, upper);
                Console.WriteLine(rf);
                Assert.IsTrue(rf >= lower && rf < upper);
            }
            //non symetrical ends of large interval
            Console.WriteLine();
            lower = -3.4028235E+38f;
            upper = 1.2448247E+38f;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                float rf = r.RandomFloat(lower, upper);
                Console.WriteLine(rf);
                Assert.IsTrue(rf >= lower && rf < upper);
            }

            //swapped lower and upper
            Console.WriteLine();
            lower = 42;
            upper = -42;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                float rf = r.RandomFloat(lower, upper);
                Console.WriteLine(rf);
                Assert.IsTrue(rf >= upper && rf < lower);
            }
        }
        [TestMethod]
        public void RandomFloatTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                float rf = r.RandomFloat();
                Console.WriteLine(rf);
                Assert.IsFalse(float.IsInfinity(rf));
                Assert.IsFalse(float.IsNaN(rf));
            }
        }
        [TestMethod]
        public void RandomFloat01Test()
        {
            RandomGenerator r = new RandomGenerator();
            RandomGenerator r2 = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                float rf = r.RandomFloat(0, 1);
                float rf2 = r2.RandomFloat(0, 1);
                Console.WriteLine("r:   {0}", rf);
                Console.WriteLine("r2: {0}", rf2);
                Assert.IsTrue(rf < 1 && rf >= 0);
                Assert.IsTrue(rf2 < 1 && rf2 >= 0);
            }
        }
    }
}
