using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;

namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorNullableTests
    {

        [TestMethod]
        public void AlwaysNullNeverNull()
        {
            RandomGenerator r = new RandomGenerator();
            //always null
            for (int i = 0; i < 10000; i++)
            {
                int? ni = r.Random.NullableGeneric<int>(1f);
                Assert.IsNull(ni);
            }
            //never null
            for (int i = 0; i < 10000; i++)
            {
                int? ni = r.Random.NullableGeneric<int>(0f);
                Assert.IsNotNull(ni);
            }
        }

        [TestMethod]
        public void InvalidProbability()
        {
            RandomGenerator r = new RandomGenerator();
            Assert.ThrowsException<ArgumentException>(() => { bool? nb = r.Random.NullableGeneric<bool>(42); });
            Assert.ThrowsException<ArgumentException>(() => { bool? nb = r.Random.NullableGeneric<bool>(-73); });
        }

        [TestMethod]
        public void NullableByteTest()
        {
            RandomGenerator r = new RandomGenerator();
            byte? x;
            byte lower = 42;
            byte upper = 73;
            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableByte(5/10f, lower, upper);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= lower && x <= upper) || (x is null));
                if(x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Assert.IsFalse(nullCounter == 0);
            nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableByte(5 / 10f);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }


        
    }
}
