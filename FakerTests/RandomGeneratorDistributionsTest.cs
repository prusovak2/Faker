using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;

namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorDistributionsTest
    {
        [TestMethod]
        public void NormalTest()
        {
            RandomGenerator rg = new();
            for (int i = 0; i < 100; i++)
            {
                double d = rg.Distribution.Normal();
                Console.WriteLine(d);
            }
        }

        [TestMethod]
        public void BernoulliTest()
        {
            RandomGenerator rg = new();

            int numIterations = 1000;
            int trueCounter = 0;
            int falseCounter = 0;
            for (int i = 0; i < numIterations; i++)
            {
                bool b = rg.Distribution.Bernoulli(0.5);
                if (b)
                {
                    trueCounter++;
                }
                else
                {
                    falseCounter++;
                }
            }

            // may potentially fail even for working Bernoulli distribution
            Assert.IsTrue(Math.Abs(trueCounter - falseCounter) < (numIterations / 10));
            Console.WriteLine($"True: {trueCounter}");
            Console.WriteLine($"False: {falseCounter}");

            for (int i = 0; i < 20; i++)
            {
                bool b = rg.Distribution.Bernoulli(1);
                Console.WriteLine(b);
                Assert.IsTrue(b);
            }

            for (int i = 0; i < 20; i++)
            {
                bool b = rg.Distribution.Bernoulli(0);
                Assert.IsFalse(b);
            }
        }

        [TestMethod]
        public void BernoulliIntTest()
        {
            RandomGenerator rg = new();

            int numIterations = 1000;
            int counter = 0;
            for (int i = 0; i < numIterations; i++)
            {
                int x = rg.Distribution.BernoulliInt(0.5);
                counter += x;
            }

            // may potentially fail even for working Bernoulli distribution
            Console.WriteLine($"trueCount: {counter}");
            Assert.IsTrue(Math.Abs(counter - numIterations/2) < (numIterations / 10));

            for (int i = 0; i < 20; i++)
            {
                int x = rg.Distribution.BernoulliInt(1);
                Console.WriteLine(i);
                Assert.IsTrue(x==1);
            }

            for (int i = 0; i < 20; i++)
            {
                int x = rg.Distribution.BernoulliInt(0);
                Assert.IsTrue(x==0);
            }
        }
    }
}
