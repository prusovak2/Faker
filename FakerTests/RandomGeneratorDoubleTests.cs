using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;


namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorDoubleTests
    {
        [TestMethod]
        public void DoubleEpsilonEqualTest()
        {
            // Initialize two doubles with apparently identical values
            double double1 = 0.3333333;
            double double2 = 1d / 3d;
            bool b = double1.EpsilonEquals(double2);
            Assert.IsTrue(b);

            double1 = .33333;
            double2 = 1d / 4d;
            b = double1.EpsilonEquals(double2);
            Assert.IsFalse(b);

            double1 = .33333;
            double2 = 99999999999999990000000.1;
            b = double1.EpsilonEquals(double2);
            Assert.IsFalse(b);

            double1 = 0;
            double2 = -0;
            b = double1.EpsilonEquals(double2);
            Assert.IsTrue(b);
        }
        [TestMethod]
        public void DoubleRangeTest()
        {
            RandomGenerator r = new RandomGenerator();

            double lower = 1;
            double upper = 100000;
            Console.WriteLine("{0} - {1}", lower,upper);
            for (int i = 0; i < 50; i++)
            {
                double rd = r.Random.Double(lower, upper);
                Assert.IsTrue(rd >= lower && rd < upper);
                Console.WriteLine(rd);
            }

            Console.WriteLine();
            lower = 1;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                double rd = r.Random.Double(lower, upper);
                Assert.IsTrue(rd >= lower && rd < upper);
                Console.WriteLine(rd);
            }
            Console.WriteLine();
            lower = -100;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                double rd = r.Random.Double(lower, upper);
                Assert.IsTrue(rd >= lower && rd < upper);
                Console.WriteLine(rd);
            }
            Console.WriteLine();
            lower = -10000;
            upper = -10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                double rd = r.Random.Double(lower, upper);
                Assert.IsTrue(rd >= lower && rd < upper);
                Console.WriteLine(rd);
            }
            Console.WriteLine();
            lower = -10;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                double rd = r.Random.Double(lower, upper);
                Assert.IsTrue(rd >= lower && rd < upper);
                Console.WriteLine(rd);
            }
        }
        [TestMethod]
        public void DoubleRangeCornerCaseTest()
        {
            //min to max
            RandomGenerator r = new RandomGenerator();
            Console.WriteLine();
            double lower = Double.MinValue;
            double upper = Double.MaxValue;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                double rd = r.Random.Double(lower, upper);
                Console.WriteLine(rd);
                Assert.IsTrue(rd >= lower && rd < upper);
            }
            //non symetrical ends of large interval
            Console.WriteLine();
            lower = -1.7976931348623157E+308;
            upper = 3.9702658657533015E+306;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                double rd = r.Random.Double(lower, upper);
                Console.WriteLine(rd);
                Assert.IsTrue(rd >= lower && rd < upper);
            }

            //swapped lower and upper
            Console.WriteLine();
            lower = 42;
            upper = -42;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                double rd = r.Random.Double(lower, upper);
                Console.WriteLine(rd);
                Assert.IsTrue(rd >= upper && rd < lower);                
            }
        }
        [TestMethod]
        public void ZeroOneDoubleTest()
        {
            RandomGenerator r = new RandomGenerator();
            RandomGenerator r2 = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                double rd = r.Random.RandomZeroToOneDouble();
                double rd2 = r2.Random.Double();
                Console.WriteLine("r:   {0}",rd );
                Console.WriteLine("r2: {0}", rd2);
                Assert.IsTrue(rd < 1 && rd >= 0);
                Assert.IsTrue(rd2 < 1 && rd2 >= 0);
            }
        }
        [TestMethod]
        public void RandomDoubleTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                double rd = r.Random.Double();
                Console.WriteLine(rd);
                Assert.IsTrue(double.IsNormal(rd));
            }
        }
        [TestMethod]
        public void RandomDouble01Test()
        {
            RandomGenerator r = new RandomGenerator();
            RandomGenerator r2 = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                double rd = r.Random.Double(0,1);
                double rd2 = r2.Random.Double(0,1);
                Console.WriteLine("r:   {0}", rd);
                Console.WriteLine("r2: {0}", rd2);
                Assert.IsTrue(rd < 1 && rd >= 0);
                Assert.IsTrue(rd2 < 1 && rd2 >= 0);
            }
        }


        [TestMethod]
        public void DoubleNullableSwapTest()
        {
            RandomGenerator rg = new RandomGenerator();
            double low = 2;
            double up = -1;

            for (int i = 0; i < 20; i++)
            {
                double rd1 = rg.Random.Double(upper: up);
                Console.WriteLine($"{up}>{rd1}");
                Assert.IsTrue(rd1 < up);

                double rd2 = rg.Random.Double(lower: low);
                Console.WriteLine($"{low}<{rd2}");
                Assert.IsTrue(rd2>low);

                //boarder swap
                double rd3 = rg.Random.Double(up, low);
                Console.WriteLine($"{up}<{rd3}<{low}");
                Assert.IsTrue((up < rd3) && (rd3 < low));
            }
        }


        [TestMethod]
        public void RandomBoolTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                bool b = r.Random.Bool();
                Console.WriteLine(b);
            }
        }
    }

}

