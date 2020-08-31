using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;

namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorIntegerTests
    {
        [TestMethod]
        public void RandomIntRangetest()
        {
            RandomGenerator r = new RandomGenerator();
            int ri;

            int lower = 1;
            int upper = 100000;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Int(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Int(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -100;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Int(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10000;
            upper = -10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Int(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Int(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void RandomIntRangeCornerCases()
        {
            RandomGenerator r = new RandomGenerator();
            int ri;

            int lower = int.MinValue;
            int upper = int.MaxValue;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Int(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = -42;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Int(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }
           
            Console.WriteLine();
            lower = -390510760;
            upper = 2104964338;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Int(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void ZeroOneIntTest()
        {
            RandomGenerator r = new RandomGenerator();
            RandomGenerator r2 = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                int rd = r.Random.Int(0,1);
                int rd2 = r2.Random.Int(0,1);
                Console.WriteLine("r:   {0}", rd);
                Console.WriteLine("r2: {0}", rd2);
                Assert.IsTrue(rd <= 1 && rd >= 0);
                Assert.IsTrue(rd2 <= 1 && rd2 >= 0);
            }
        }
        [TestMethod]
        public void RandomIntTest()
        {
            //NO ASSERT!!!
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                int ri = r.Random.Int();
                Console.WriteLine(ri);
            }
        }

        [TestMethod]
        public void RandomUintRangetest()
        {
            RandomGenerator r = new RandomGenerator();
            uint ri;

            uint lower = 1;
            uint upper = 100000;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Uint(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Uint(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void RandomUintRangeCornerCases()
        {
            RandomGenerator r = new RandomGenerator();
            uint ri;

            uint lower = uint.MinValue;
            uint upper = uint.MaxValue;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Uint(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = 13;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Uint(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 390510;
            upper = 2104964338;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Uint(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void ZeroOneUintTest()
        {
            RandomGenerator r = new RandomGenerator();
            RandomGenerator r2 = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                uint rd = r.Random.Uint(0, 1);
                uint rd2 = r2.Random.Uint(0, 1);
                Console.WriteLine("r:   {0}", rd);
                Console.WriteLine("r2: {0}", rd2);
                Assert.IsTrue(rd <= 1 && rd >= 0);
                Assert.IsTrue(rd2 <= 1 && rd2 >= 0);
            }
        }
        [TestMethod]
        public void RandomUintTest()
        {
            //NO ASSERT!!!
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                uint ri = r.Random.Uint();
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void RandomShortRangetest()
        {
            RandomGenerator r = new RandomGenerator();
            short ri;

            short lower = 1;
            short upper = 10000;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Short(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Short(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -100;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Short(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10000;
            upper = -10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Short(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Short(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void RandomShortRangeCornerCases()
        {
            RandomGenerator r = new RandomGenerator();
            short ri;

            short lower = short.MinValue;
            short upper = short.MaxValue;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Short(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = -42;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Short(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -3905;
            upper = 21049;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Short(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void ZeroOneShortTest()
        {
            RandomGenerator r = new RandomGenerator();
            RandomGenerator r2 = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                short rd = r.Random.Short(0, 1);
                short rd2 = r2.Random.Short(0, 1);
                Console.WriteLine("r:   {0}", rd);
                Console.WriteLine("r2: {0}", rd2);
                Assert.IsTrue(rd <= 1 && rd >= 0);
                Assert.IsTrue(rd2 <= 1 && rd2 >= 0);
            }
        }
        [TestMethod]
        public void RandomShortTest()
        {
            //NO ASSERT!!!
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                int ri = r.Random.Short();
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void RandomUshortRangetest()
        {
            RandomGenerator r = new RandomGenerator();
            ushort ri;

            ushort lower = 1;
            ushort upper = 10000;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Ushort(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Ushort(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void RandomUshortRangeCornerCases()
        {
            RandomGenerator r = new RandomGenerator();
            ushort ri;

            ushort lower = ushort.MinValue;
            ushort upper = ushort.MaxValue;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Ushort(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = 13;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Ushort(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 390;
            upper = 21049;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Ushort(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void ZeroOneUshortTest()
        {
            RandomGenerator r = new RandomGenerator();
            RandomGenerator r2 = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                ushort rd = r.Random.Ushort(0, 1);
                ushort rd2 = r2.Random.Ushort(0, 1);
                Console.WriteLine("r:   {0}", rd);
                Console.WriteLine("r2: {0}", rd2);
                Assert.IsTrue(rd <= 1 && rd >= 0);
                Assert.IsTrue(rd2 <= 1 && rd2 >= 0);
            }
        }
        [TestMethod]
        public void RandomUshortTest()
        {
            //NO ASSERT!!!
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                uint ri = r.Random.Uint();
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void RandomSbyteRangetest()
        {
            RandomGenerator r = new RandomGenerator();
            sbyte ri;

            sbyte lower = 1;
            sbyte upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Sbyte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Sbyte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -100;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Sbyte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -100;
            upper = -10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Sbyte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Sbyte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void RandomSbyteRangeCornerCases()
        {
            RandomGenerator r = new RandomGenerator();
            sbyte ri;

            sbyte lower = sbyte.MinValue;
            sbyte upper = sbyte.MaxValue;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Sbyte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = -42;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Sbyte(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -78;
            upper = 113;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Sbyte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void ZeroOneSbyteTest()
        {
            RandomGenerator r = new RandomGenerator();
            RandomGenerator r2 = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                short rd = r.Random.Sbyte(0, 1);
                short rd2 = r2.Random.Sbyte(0, 1);
                Console.WriteLine("r:   {0}", rd);
                Console.WriteLine("r2: {0}", rd2);
                Assert.IsTrue(rd <= 1 && rd >= 0);
                Assert.IsTrue(rd2 <= 1 && rd2 >= 0);
            }
        }
        [TestMethod]
        public void RandomSbyteTest()
        {
            //NO ASSERT!!!
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                int ri = r.Random.Sbyte();
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void RandomByteRangetest()
        {
            RandomGenerator r = new RandomGenerator();
            byte ri;

            byte lower = 1;
            byte upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Byte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Byte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void RandomByteRangeCornerCases()
        {
            RandomGenerator r = new RandomGenerator();
            byte ri;

            byte lower = byte.MinValue;
            byte upper = byte.MaxValue;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Byte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = 13;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Byte(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 33;
            upper = 210;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Byte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void ZeroOneByteTest()
        {
            RandomGenerator r = new RandomGenerator();
            RandomGenerator r2 = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                ushort rd = r.Random.Byte(0, 1);
                ushort rd2 = r2.Random.Byte(0, 1);
                Console.WriteLine("r:   {0}", rd);
                Console.WriteLine("r2: {0}", rd2);
                Assert.IsTrue(rd <= 1 && rd >= 0);
                Assert.IsTrue(rd2 <= 1 && rd2 >= 0);
            }
        }
        [TestMethod]
        public void RandomBytetTest()
        {
            //NO ASSERT!!!
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                uint ri = r.Random.Byte();
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void RandomLongRangetest()
        {
            RandomGenerator r = new RandomGenerator();
            long ri;

            long lower = 1;
            long upper = 1000000;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Long(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Long(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -100;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Long(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10000;
            upper = -10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Long(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Long(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void RandomLongRangeCornerCases()
        {
            RandomGenerator r = new RandomGenerator();
            long ri;

            long lower = long.MinValue;
            long upper = long.MaxValue;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Long(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = -42;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Long(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -3905107632108282;
            upper = 210496433832342233;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Long(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void ZeroOneLongTest()
        {
            RandomGenerator r = new RandomGenerator();
            RandomGenerator r2 = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                long rd = r.Random.Long(0, 1);
                long rd2 = r2.Random.Long(0, 1);
                Console.WriteLine("r:   {0}", rd);
                Console.WriteLine("r2: {0}", rd2);
                Assert.IsTrue(rd <= 1 && rd >= 0);
                Assert.IsTrue(rd2 <= 1 && rd2 >= 0);
            }
        }
        [TestMethod]
        public void RandomLongTest()
        {
            //NO ASSERT!!!
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                long ri = r.Random.Long();
                Console.WriteLine(ri);
            }
        }

        [TestMethod]
        public void RandomUlongRangetest()
        {
            RandomGenerator r = new RandomGenerator();
            ulong ri;

            ulong lower = 1;
            ulong upper = 100000;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Ulong(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Ulong(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void RandomUlongRangeCornerCases()
        {
            RandomGenerator r = new RandomGenerator();
            ulong ri;

            ulong lower = ulong.MinValue;
            ulong upper = ulong.MaxValue;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Ulong(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = 13;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Ulong(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 39051022;
            upper = 210496433832342327;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.Random.Ulong(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void ZeroOneUlongTest()
        {
            RandomGenerator r = new RandomGenerator();
            RandomGenerator r2 = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                ulong rd = r.Random.Ulong(0, 1);
                ulong rd2 = r2.Random.Ulong(0, 1);
                Console.WriteLine("r:   {0}", rd);
                Console.WriteLine("r2: {0}", rd2);
                Assert.IsTrue(rd <= 1 && rd >= 0);
                Assert.IsTrue(rd2 <= 1 && rd2 >= 0);
            }
        }
        [TestMethod]
        public void RandomUlongest()
        {
            //NO ASSERT!!!
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                ulong ri = r.Random.Ulong();
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void OddTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 50; i++)
            {
                int odd = r.Random.IntOdd();
                Console.WriteLine(odd);
                Assert.IsTrue((odd % 2) != 0);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int odd = r.Random.IntOdd(21, 30);
                Console.WriteLine(odd);
                Assert.IsTrue((odd % 2) != 0);
                Assert.IsTrue(odd >= 21 && odd <= 30);
            }
            Console.WriteLine();
            for (int i = 0; i < 1000; i++)
            {
                int odd = r.Random.IntOdd(-101, -21);
                Console.WriteLine(odd);
                Assert.IsTrue((odd % 2) != 0);
                Assert.IsTrue(odd >= -101 && odd <= 21);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int odd = r.Random.IntOdd(-500, 333);
                Console.WriteLine(odd);
                Assert.IsTrue((odd % 2) != 0);
                Assert.IsTrue(odd >= -500 && odd <= 333);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int odd = r.Random.IntOdd(-11, 10);
                Console.WriteLine(odd);
                Assert.IsTrue((odd % 2) != 0);
                Assert.IsTrue(odd >= -11 && odd <= 10);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int odd = r.Random.IntOdd(20, 100);
                Console.WriteLine(odd);
                Assert.IsTrue((odd % 2) != 0);
                Assert.IsTrue(odd >= 20 && odd <= 100);
            }
            Console.WriteLine();
            for (int i = 0; i < 1000; i++)
            {
                int odd = r.Random.IntOdd(-21, -100);
                Console.WriteLine(odd);
                Assert.IsTrue((odd % 2) != 0);
                Assert.IsTrue(odd >= -100 && odd <= -21);
            }
        }
        [TestMethod]
        public void EvenTest()
        {
            //Console.WriteLine(-21/2);
            //Console.WriteLine();
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 50; i++)
            {
                int even = r.Random.IntEven();
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int even = r.Random.IntEven(-500, 333);
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
                Assert.IsTrue(even >= -500 && even <= 333);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int even = r.Random.IntEven(-11, 10);
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
                Assert.IsTrue(even >= -11 && even <= 10);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int even = r.Random.IntEven(20, 100);
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
                Assert.IsTrue(even >= 20 && even <= 100);
            }
            Console.WriteLine();
            for (int i = 0; i < 1000; i++)
            {
                int even = r.Random.IntEven(-21, -100);
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
                Assert.IsTrue(even >=-100 && even <= -21);
            }
            Console.WriteLine();
            for (int i = 0; i < 1000; i++)
            {
                int even = r.Random.IntEven(-21, -101);
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
                Assert.IsTrue(even >= -101 && even <= -21);
            }
            for (int i = 0; i < 50; i++)
            {
                int even = r.Random.IntEven(21, 101);
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
                Assert.IsTrue(even >= 21 && even <= 101);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int even = r.Random.IntEven(-20, -100);
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
                Assert.IsTrue(even >= -100 && even <= -20);
            }
            for (int i = 0; i < 50; i++)
            {
                int even = r.Random.IntEven(20, 101);
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
                Assert.IsTrue(even >= 20 && even <= 101);
            }
            for (int i = 0; i < 50; i++)
            {
                int even = r.Random.IntEven(21, 100);
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
                Assert.IsTrue(even >= 21 && even <= 100);
            }
        }
    }
}
