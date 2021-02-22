using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System.IO;

namespace FakerTests
{
    //TODO: print results to files
    [TestClass]
    public class NOASSERTZigguratTests
    {
        [TestMethod]
        public void NormalDistShiftedTest()
        {
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = NormalDistribution.Double(x, 42, 13);
                Console.WriteLine(d);
            }
        }
        [TestMethod]
        public void NormalDistBasicTest()
        {
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = NormalDistribution.Double(x);
                Console.WriteLine(d);
            }
        }

        [TestMethod]
        public void ExponentialDistBasicTest()
        {
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = ExponentialDistribution.Double(x);
                Console.WriteLine(d);
            }
        }

        [TestMethod]
        public void ExponentialDistributionShiftedTest()
        {
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = ExponentialDistribution.Double(x,5);
                Console.WriteLine(d);
            }
        }

        [TestMethod]
        public void ExponentialShiftedNotShofted()
        {
            Xoshiro256starstar x = new();
            for (int i = 0; i< 2000; i++)
            {
                double d = ExponentialDistribution.Double(x,1);
                Console.WriteLine(d);
            }
        }
        [TestMethod]
        public void ExponentialDistributionShifted2Test()
        {
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = ExponentialDistribution.Double(x, -10);
                Console.WriteLine(d);
            }
        }

        [TestMethod]
        public void GeometricBasic()
        {
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\Geometric0.5.csv", append: false);
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                int x = rg.Distribution.Geometric(0.2);
                Console.WriteLine(x);
                writer.WriteLine(x);
            }
            writer.Flush();
            writer.Dispose();
        }
    }
}
