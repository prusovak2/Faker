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
        public void RandomFloatTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                float rf = r.RandomFloat();
                Console.WriteLine(rf);
                Assert.IsTrue(float.IsNormal(rf));
            }
        }
    }
}
