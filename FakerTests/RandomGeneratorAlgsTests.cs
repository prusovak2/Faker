using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System;

namespace FakerTests
{
    
    [TestClass]
    public class RandomGeneratorAlgTests
    {
        [TestMethod]
        public void SplitMixTest()
        {
            Splitmix64 s = new Splitmix64();
            System.Console.WriteLine(s.Seed);
            System.Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                System.Console.WriteLine(s.Next());
            }
        }
        [TestMethod]
        public void Xoshiro256starstarTest()
        {
            Xoshiro256starstar s = new Xoshiro256starstar(1);
            System.Console.WriteLine(s.Seed);
            System.Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                ulong r = s.Next();
                System.Console.WriteLine(Convert.ToString((long)r, 2));
                //ulong mask = 0b_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1111_1000_0000_0000;
                //r &= mask;
                r >>= 11;
                System.Console.WriteLine("{0}", Convert.ToString((long)r,2));
                double d = r / (double) 9007199254740992;
                Console.WriteLine(r);
                Console.WriteLine(d);
            }
        }
        [TestMethod]
        public void ZeroOneDoubleTest()
        {
            RandomGenerator r = new RandomGenerator();
            RandomGenerator r2 = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("r:   {0}", r.ZeroToOneDouble());
                Console.WriteLine("r2: {0}", r2.ZeroToOneDouble());
            }
        }
        [TestMethod]
        public void DoubleRangeIdeaTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine(r.DoubleRangePossitiveForATimeBeing(10,1100));
            }
        }
    }
}

