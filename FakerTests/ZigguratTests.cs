using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System.IO;
using System.Globalization;

namespace FakerTests
{
    //TODO: print results to files
    [TestClass]
    public class NOASSERT_ProvideData
    {
        [TestMethod]
        public void NormalDistShiftedTest()
        {
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\normal_mean_42_stdev_13.csv", append: false);
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = NormalDistribution.Double(x, 42, 13);
                Console.WriteLine(d);
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
            }
            writer.Flush();
            writer.Dispose();
        }
        [TestMethod]
        public void NormalDistBasicTest()
        {
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\normal.csv", append: false);
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = NormalDistribution.Double(x);
                Console.WriteLine(d);
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
            }
            writer.Flush();
            writer.Dispose();
        }

        [TestMethod]
        public void ExponentialDistBasicTest()
        {
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\exponential.csv", append: false);
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = ExponentialDistribution.Double(x);
                Console.WriteLine(d);
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
            }
            writer.Flush();
            writer.Dispose();
        }

        [TestMethod]
        public void ExponentialDistributionShiftedTest()
        {
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\exponential5.csv", append: false);
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = ExponentialDistribution.Double(x,5);
                Console.WriteLine(d);
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
            }
            writer.Flush();
            writer.Dispose();
        }

        [TestMethod]
        public void ExponentialShiftedNotShifted()
        {
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\ExponentialShiftingNotShifted.csv", append: false);
            Xoshiro256starstar x = new();
            for (int i = 0; i< 2000; i++)
            {
                double d = ExponentialDistribution.Double(x,1);
                Console.WriteLine(d);
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
            }
            writer.Flush();
            writer.Dispose();
        }

        [TestMethod]
        public void ExponentialDistributionShifted2Test()
        {
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\exponential_-10.csv", append: false);
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = ExponentialDistribution.Double(x, -10);
                Console.WriteLine(d);
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
            }
            writer.Flush();
            writer.Dispose();
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

        [TestMethod]
        public void Binomial()
        {
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\Binomial.csv", append: false);
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                int x = rg.Distribution.Binomial(40, 0.5);
                Console.WriteLine(x);
                writer.WriteLine(x);
            }
            writer.Flush();
            writer.Dispose();
        }

        [TestMethod]
        public void BinomialNaive()
        {
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\BinomialNaive.csv", append: false);
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                int x = rg.Distribution.BinomialNaive(40, 0.5);
                Console.WriteLine(x);
                writer.WriteLine(x);
            }
            writer.Flush();
            writer.Dispose();
        }

        [TestMethod]
        public void Gamma()
        {
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\Gamma.csv", append: false);
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = rg.Distribution.Gamma(10);
                Console.WriteLine(d);
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
            }
            writer.Flush();
            writer.Dispose();
        }

        [TestMethod]
        public void Beta()
        {
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\Beta.csv", append: false);
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = rg.Distribution.Beta(10, 5);
                Console.WriteLine(d);
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
            }
            writer.Flush();
            writer.Dispose();
        }

        [TestMethod]
        public void ChiSquare1()
        {
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\ChiSquare1.csv", append: false);
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = rg.Distribution.ChiSquare(1);
                Console.WriteLine(d);
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
            }
            writer.Flush();
            writer.Dispose();
        }


        [TestMethod]
        public void ChiSquare2()
        {
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\ChiSquare2.csv", append: false);
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = rg.Distribution.ChiSquare(2);
                Console.WriteLine(d);
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
            }
            writer.Flush();
            writer.Dispose();
        }

        [TestMethod]
        public void ChiSquare3()
        {
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\ChiSquare3.csv", append: false);
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = rg.Distribution.ChiSquare(3);
                Console.WriteLine(d);
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
            }
            writer.Flush();
            writer.Dispose();
        }
    }
}

