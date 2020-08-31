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
                float rf2 = r2.Float();
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
                float rf = r.Float(lower, upper);
                Assert.IsTrue(rf >= lower && rf < upper);
                Console.WriteLine(rf);
            }

            Console.WriteLine();
            lower = 1;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                float rf = r.Float(lower, upper);
                Assert.IsTrue(rf >= lower && rf < upper);
                Console.WriteLine(rf);
            }
            Console.WriteLine();
            lower = -100;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                float rf = r.Float(lower, upper);
                Assert.IsTrue(rf >= lower && rf < upper);
                Console.WriteLine(rf);
            }
            Console.WriteLine();
            lower = -10000;
            upper = -10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                float rf = r.Float(lower, upper);
                Assert.IsTrue(rf >= lower && rf < upper);
                Console.WriteLine(rf);
            }
            Console.WriteLine();
            lower = -10;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                float rf = r.Float(lower, upper);
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
                float rf = r.Float(lower, upper);
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
                float rf = r.Float(lower, upper);
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
                float rf = r.Float(lower, upper);
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
                float rf = r.Float();
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
                float rf = r.Float(0, 1);
                float rf2 = r2.Float(0, 1);
                Console.WriteLine("r:   {0}", rf);
                Console.WriteLine("r2: {0}", rf2);
                Assert.IsTrue(rf < 1 && rf >= 0);
                Assert.IsTrue(rf2 < 1 && rf2 >= 0);
            }
        }
        [TestMethod]
        public void RandomDecimalTest()
        {
            RandomGenerator r = new RandomGenerator();
            decimal rd;
            for (int i = 0; i < 100000; i++)
            {
                rd = r.RandomZeroToOneDecimal();
                Console.WriteLine(rd);
                Assert.IsTrue(rd >= 0 && rd < 1);
            }
        }
        [TestMethod]
        public void DecimalEpsilonEqualTest()
        {
            // Initialize two doubles with apparently identical values
            decimal decimal1 = 0.3333333m;
            decimal decimal2 = 1m / 3m;
            bool b = decimal1.EpsilonEquals(decimal2);
            Assert.IsTrue(b);

            decimal1 = .33333m;
            decimal2 = 1m / 4m;
            b = decimal1.EpsilonEquals(decimal2);
            Assert.IsFalse(b);

            decimal1 = .33333m;
            decimal2 = 99999999999999990000000.1m;
            b = decimal1.EpsilonEquals(decimal2);
            Assert.IsFalse(b);

            decimal1 = 0;
            decimal2 = -0;
            b = decimal1.EpsilonEquals(decimal2);
            Assert.IsTrue(b);
        }
        [TestMethod]
        public void RandomDecimalRangetest()
        {
            RandomGenerator r = new RandomGenerator();
            decimal ri;

            decimal lower = 1;
            decimal upper = 100000;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Decimal(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Decimal(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -100;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Decimal(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10000;
            upper = -10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Decimal(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Decimal(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void RandomDecimalRangeCornerCases()
        {
            RandomGenerator r = new RandomGenerator();
            decimal ri;

            decimal lower = decimal.MinValue;
            decimal upper = decimal.MaxValue;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Decimal(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = -42;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Decimal(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -3905107602332200;
            upper = 210496433832442233;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Decimal(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void ZeroOneDecimalTest()
        {
            RandomGenerator r = new RandomGenerator();
            RandomGenerator r2 = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                decimal rd = r.Decimal(0, 1);
                decimal rd2 = r2.Decimal();
                Console.WriteLine("r:   {0}", rd);
                Console.WriteLine("r2: {0}", rd2);
                Assert.IsTrue(rd <= 1 && rd >= 0);
                Assert.IsTrue(rd2 <= 1 && rd2 >= 0);
            }
        }
        [TestMethod]
        public void RandomIntTest()
        {
            //NO ASSERT!!!
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                //decimal ri = r.RandomDecimal();
                //Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void Test()
        {
            RandomGenerator r = new RandomGenerator();
            decimal rd = 0.9999999999999999999999999999m;
            var bits = decimal.GetBits(rd);
            foreach (var item in bits)
            {
                Console.WriteLine(item);
            }
            
        }
    }
}
