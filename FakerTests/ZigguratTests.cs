//#define PRINT

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
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\normal_mean_42_stdev_13.csv", append: false);
#endif
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = NormalDistribution.Double(x, 42, 13);
                Console.WriteLine(d);
#if (PRINT)
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
#endif
            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif
        }
        [TestMethod]
        public void NormalDistBasicTest()
        {
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\normal.csv", append: false);
#endif
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = NormalDistribution.Double(x);
                Console.WriteLine(d);
#if (PRINT)
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
#endif
            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif
        }

        [TestMethod]
        public void ExponentialDistBasicTest()
        {
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\exponential.csv", append: false);
#endif           
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = ExponentialDistribution.Double(x);
                Console.WriteLine(d);
#if (PRINT)
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
#endif
            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif
        }

        [TestMethod]
        public void ExponentialDistributionShiftedTest()
        {
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\exponential5.csv", append: false);
#endif            
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = ExponentialDistribution.Double(x,5);
                Console.WriteLine(d);
#if (PRINT)
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
#endif
            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif
        }

        [TestMethod]
        public void ExponentialShiftedNotShifted()
        {
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\ExponentialShiftingNotShifted.csv", append: false);
#endif            
            Xoshiro256starstar x = new();
            for (int i = 0; i< 2000; i++)
            {
                double d = ExponentialDistribution.Double(x,1);
                Console.WriteLine(d);
#if (PRINT)
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
#endif
            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif
        }

        [TestMethod]
        public void ExponentialDistributionShifted2Test()
        {
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\exponential_-10.csv", append: false);
#endif            
            Xoshiro256starstar x = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = ExponentialDistribution.Double(x, -10);
                Console.WriteLine(d);
#if (PRINT)
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
#endif
            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif
        }

        [TestMethod]
        public void GeometricBasic()
        {
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\Geometric0.5.csv", append: false);
#endif            
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                int x = rg.Distribution.Geometric(0.2);
                Console.WriteLine(x);
#if (PRINT)
                writer.WriteLine(x);
#endif            

            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif            
        }

        [TestMethod]
        public void Binomial()
        {
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\Binomial.csv", append: false);
#endif             
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                int x = rg.Distribution.Binomial(40, 0.5);
                Console.WriteLine(x);
#if (PRINT)
                writer.WriteLine(x);
#endif 
            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif 
        }

        [TestMethod]
        public void BinomialNaive()
        {
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\BinomialNaive.csv", append: false);
#endif             
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                int x = rg.Distribution.BinomialNaive(40, 0.5);
                Console.WriteLine(x);
#if (PRINT)
                writer.WriteLine(x);
#endif 
            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif 
        }

        [TestMethod]
        public void BinomialClever()
        {
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\BinomialClever.csv", append: false);
#endif             
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                int x = rg.Distribution.BinomialClever(100, 0.5);
                Console.WriteLine(x);
#if (PRINT)
                writer.WriteLine(x);
#endif 
            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif 
        }

        [TestMethod]
        public void Gamma()
        {
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\Gamma.csv", append: false);
#endif             
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = rg.Distribution.Gamma(10);
                Console.WriteLine(d);
#if (PRINT)
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
#endif 
            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif 
        }

        [TestMethod]
        public void Beta()
        {
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\Beta.csv", append: false);
#endif             
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = rg.Distribution.Beta(10, 5);
                Console.WriteLine(d);
#if (PRINT)
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
#endif 
            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif 
        }

        [TestMethod]
        public void ChiSquare1()
        {
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\ChiSquare1.csv", append: false);
#endif             
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = rg.Distribution.ChiSquare(1);
                Console.WriteLine(d);
#if (PRINT)
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
#endif 
            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif 
        }


        [TestMethod]
        public void ChiSquare2()
        {
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\ChiSquare2.csv", append: false);
#endif             
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = rg.Distribution.ChiSquare(2);
                Console.WriteLine(d);
#if (PRINT)
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
#endif 
            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif 
        }

        [TestMethod]
        public void ChiSquare3()
        {
#if (PRINT)
            StreamWriter writer = new("D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\ChiSquare3.csv", append: false);
#endif             
            RandomGenerator rg = new();
            for (int i = 0; i < 2000; i++)
            {
                double d = rg.Distribution.ChiSquare(3);
                Console.WriteLine(d);
#if (PRINT)
                writer.WriteLine(d.ToString(CultureInfo.InvariantCulture.NumberFormat));
#endif 
            }
#if (PRINT)
            writer.Flush();
            writer.Dispose();
#endif 
        }
    }
}

