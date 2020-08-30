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
                ri = r.RandomInt(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomInt(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -100;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomInt(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10000;
            upper = -10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomInt(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomInt(lower, upper);
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
                ri = r.RandomInt(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = -42;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomInt(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }
           
            Console.WriteLine();
            lower = -390510760;
            upper = 2104964338;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomInt(lower, upper);
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
                int rd = r.RandomInt(0,1);
                int rd2 = r2.RandomInt(0,1);
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
                int ri = r.RandomInt();
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
                ri = r.RandomUint(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomUint(lower, upper);
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
                ri = r.RandomUint(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = 13;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomUint(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 390510;
            upper = 2104964338;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomUint(lower, upper);
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
                uint rd = r.RandomUint(0, 1);
                uint rd2 = r2.RandomUint(0, 1);
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
                uint ri = r.RandomUint();
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
                ri = r.RandomShort(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomShort(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -100;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomShort(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10000;
            upper = -10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomShort(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomShort(lower, upper);
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
                ri = r.RandomShort(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = -42;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomShort(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -3905;
            upper = 21049;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomShort(lower, upper);
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
                short rd = r.RandomShort(0, 1);
                short rd2 = r2.RandomShort(0, 1);
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
                int ri = r.RandomShort();
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
                ri = r.RandomUshort(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomUshort(lower, upper);
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
                ri = r.RandomUshort(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = 13;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomUshort(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 390;
            upper = 21049;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomUshort(lower, upper);
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
                ushort rd = r.RandomUshort(0, 1);
                ushort rd2 = r2.RandomUshort(0, 1);
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
                uint ri = r.RandomUint();
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
                ri = r.RandomSbyte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomSbyte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -100;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomSbyte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -100;
            upper = -10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomSbyte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomSbyte(lower, upper);
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
                ri = r.RandomSbyte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = -42;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomSbyte(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -78;
            upper = 113;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomSbyte(lower, upper);
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
                short rd = r.RandomSbyte(0, 1);
                short rd2 = r2.RandomSbyte(0, 1);
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
                int ri = r.RandomSbyte();
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
                ri = r.RandomByte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomByte(lower, upper);
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
                ri = r.RandomByte(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = 13;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomByte(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 33;
            upper = 210;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomByte(lower, upper);
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
                ushort rd = r.RandomByte(0, 1);
                ushort rd2 = r2.RandomByte(0, 1);
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
                uint ri = r.RandomByte();
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
                ri = r.RandomLong(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomLong(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -100;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomLong(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10000;
            upper = -10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomLong(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -10;
            upper = 10;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomLong(lower, upper);
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
                ri = r.RandomLong(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = -42;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomLong(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = -3905107632108282;
            upper = 210496433832342233;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomLong(lower, upper);
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
                long rd = r.RandomLong(0, 1);
                long rd2 = r2.RandomLong(0, 1);
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
                long ri = r.RandomLong();
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
                ri = r.RandomUlong(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 1;
            upper = 100;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomUlong(lower, upper);
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
                ri = r.RandomUlong(lower, upper);
                Assert.IsTrue(ri >= lower && ri <= upper);
                Console.WriteLine(ri);
            }
            Console.WriteLine();
            lower = 42;
            upper = 13;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomUlong(lower, upper);
                Assert.IsTrue(ri >= upper && ri <= lower);
                Console.WriteLine(ri);
            }

            Console.WriteLine();
            lower = 39051022;
            upper = 210496433832342327;
            Console.WriteLine("{0} - {1}", lower, upper);
            for (int i = 0; i < 50; i++)
            {
                ri = r.RandomUlong(lower, upper);
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
                ulong rd = r.RandomUlong(0, 1);
                ulong rd2 = r2.RandomUlong(0, 1);
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
                ulong ri = r.RandomUlong();
                Console.WriteLine(ri);
            }
        }
        [TestMethod]
        public void OddTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 50; i++)
            {
                int odd = r.RandomOddInt();
                Console.WriteLine(odd);
                Assert.IsTrue((odd % 2) != 0);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int odd = r.RandomOddInt(-500, 333);
                Console.WriteLine(odd);
                Assert.IsTrue((odd % 2) != 0);
                Assert.IsTrue(odd >= -500 && odd <= 333);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int odd = r.RandomOddInt(-11, 10);
                Console.WriteLine(odd);
                Assert.IsTrue((odd % 2) != 0);
                Assert.IsTrue(odd >= -11 && odd <= 10);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int odd = r.RandomOddInt(20, 100);
                Console.WriteLine(odd);
                Assert.IsTrue((odd % 2) != 0);
                Assert.IsTrue(odd >= 20 && odd <= 100);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int odd = r.RandomOddInt(-20, -100);
                Console.WriteLine(odd);
                Assert.IsTrue((odd % 2) != 0);
                Assert.IsTrue(odd >= -100 && odd <= -20);
            }
        }
        [TestMethod]
        public void EvenTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 50; i++)
            {
                int even = r.RandomEvenInt();
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int even = r.RandomEvenInt(-500, 333);
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
                Assert.IsTrue(even >= -500 && even <= 333);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int even = r.RandomEvenInt(-11, 10);
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
                Assert.IsTrue(even >= -11 && even <= 10);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int even = r.RandomEvenInt(20, 100);
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
                Assert.IsTrue(even >= 20 && even <= 100);
            }
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                int even = r.RandomEvenInt(-20, -100);
                Console.WriteLine(even);
                Assert.IsTrue((even % 2) == 0);
                Assert.IsTrue(even >=-100 && even <= -20);
            }
        }
    }
}
