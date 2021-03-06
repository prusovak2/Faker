using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System;
using System.Threading;

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
                System.Console.WriteLine("{0}", Convert.ToString((long)r, 2));
                double d = r / (double)9007199254740992;
                Console.WriteLine(r);
                Console.WriteLine(d);
            }
        }
        [TestMethod]
        public void SeedTest()
        {
            RandomGenerator r = new RandomGenerator(42);
            Assert.AreEqual(42ul, r.Seed);
        }
        [TestMethod]
        public void WeysCounterThreadTest()
        {
            Job j1 = new Job(1);
            Job j2 = new Job(2);
            Thread t1 = new Thread(j1.run);
            Thread t2 = new Thread(j2.run);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            for (int i = 0; i < 30; i++)
            {
                Assert.AreNotEqual(j1.nums[i], j2.nums[i]);
            }
        }

        [TestMethod]
        public void ZeroOneDoubleTest()
        {
            Xoshiro256starstar x = new();
            Xoshiro256starstar x2 = new();
            for (int i = 0; i < 30; i++)
            {
                double rd = x.Next01Double();
                double rd2 = x.Next01Double();
                Console.WriteLine("r:   {0}", rd);
                Console.WriteLine("r2: {0}", rd2);
                Assert.IsTrue(rd < 1 && rd >= 0);
                Assert.IsTrue(rd2 < 1 && rd2 >= 0);
            }
        }

        [TestMethod]
        public void ZeroOneDoubleNonZeroTest()
        {
            Xoshiro256starstar x = new();
            Xoshiro256starstar x2 = new();
            for (int i = 0; i < 30; i++)
            {
                double rd = x.Next01NonZeroDouble();
                double rd2 = x.Next01NonZeroDouble();
                Console.WriteLine("r:   {0}", rd);
                Console.WriteLine("r2: {0}", rd2);
                Assert.IsTrue(rd < 1 && rd > 0);
                Assert.IsTrue(rd2 < 1 && rd2 > 0);
            }
        }
    }
    class Job
    {
        internal int id;
        public double[] nums = new double[30];
        public Job(int i)
        {
            id = i;
        }
        public void run()
        {
            Console.WriteLine("thread {0} creates random", this.id);
            RandomGenerator r = new RandomGenerator();
            Console.WriteLine("thread {0} counter value {1}",this.id, Splitmix64.WeylSequenceSeedCounter);
            for (int i = 0; i < 30; i++)
            {
                double random = r.Random.Double();
                this.nums[i] = random;
                Console.WriteLine("{0} thread {1} generates random number {2}",i, this.id, random);
            }

        }
    }
}

