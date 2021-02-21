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
    public class ZigguratTests
    {
        [TestMethod]
        public void NormalDistBasicTest()
        {
            NormalDistribution nd = new NormalDistribution(new Xoshiro256starstar());           
            for (int i = 0; i < 1000; i++)
            {
                double d = nd.Double(42, 13);
                Console.WriteLine(d);
            }
        }

        [TestMethod]
        public void ExponentialDistBasicTest()
        {
            ExponentialZiggurat nd = ExponentialZiggurat.Instance;
            IRandomGeneratorAlg alg = new Xoshiro256starstar();
            for (int i = 0; i < 1000; i++)
            {
                double d = nd.Generate(alg);
                Console.WriteLine(d);
            }
            Console.WriteLine("abraka");
        }
    }
}
