using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;

namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorListTests
    {
        [TestMethod]
        public void ListByteTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;

            //range
            counter = 0;
            byte lower = 0;
            byte upper = 200;
            IList<byte> l = r.List.Byte(count, lower, upper);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(byte));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range
            counter = 0;
            l = r.List.Byte(count);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(byte));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.Byte(count, precise: false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(byte));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }

        [TestMethod]
        public void ListSbyteTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;

            //range
            counter = 0;
            sbyte lower = -100;
            sbyte upper = 100;
            IList<sbyte> l = r.List.Sbyte(count, lower, upper);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(sbyte));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range
            counter = 0;
            l = r.List.Sbyte(count);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(sbyte));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.Sbyte(count, precise: false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(sbyte));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }

        [TestMethod]
        public void ListShortTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;

            //range
            counter = 0;
            short lower = -1000;
            short upper = 1000;
            IList<short> l = r.List.Short(count, lower, upper);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(short));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range
            counter = 0;
            l = r.List.Short(count);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(short));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.Short(count, precise: false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(short));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void ListUshortTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;

            //range
            counter = 0;
            ushort lower = 42;
            ushort upper = 1000;
            IList<ushort> l = r.List.Ushort(count, lower, upper);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ushort));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range
            counter = 0;
            l = r.List.Ushort(count);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ushort));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.Ushort(count, precise: false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ushort));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }

        [TestMethod]
        public void ListIntTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;

            //range
            counter = 0;
            int lower = -4200;
            int upper = 7300;
            IList<int> l = r.List.Int(count, lower, upper);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(int));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range
            counter = 0;
            l = r.List.Int(count);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(int));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.Int(count, precise: false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(int));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }

        [TestMethod]
        public void ListUintTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;

            //range
            counter = 0;
            uint lower = 42;
            uint upper = 73000;
            IList<uint> l = r.List.Uint(count, lower, upper);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(uint));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range
            counter = 0;
            l = r.List.Uint(count);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(uint));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.Uint(count, precise: false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(uint));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }

        [TestMethod]
        public void ListUlongTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;

            //range
            counter = 0;
            ulong lower = 42;
            ulong upper = 73000;
            IList<ulong> l = r.List.Ulong(count, lower, upper);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ulong));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range
            counter = 0;
            l = r.List.Ulong(count);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ulong));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.Ulong(count, precise: false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ulong));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }

        [TestMethod]
        public void ListLongTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;

            //range
            counter = 0;
            long lower = -4200;
            long upper = 73000;
            IList<long> l = r.List.Long(count, lower, upper);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(long));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range
            counter = 0;
            l = r.List.Long(count);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(long));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.Long(count, precise: false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(long));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }

        [TestMethod]
        public void ListFloatTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;

            //range
            counter = 0;
            float lower = -4200.765f;
            float upper = 73000.888f;
            IList<float> l = r.List.Float(count, lower, upper);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(float));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range
            counter = 0;
            l = r.List.Float(count);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(float));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.Float(count, precise: false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(float));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }

        [TestMethod]
        public void ListDoubleTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;

            //range
            counter = 0;
            double lower = -4200.666d;
            double upper = 73000.7676d;
            IList<double> l = r.List.Double(count, lower, upper);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(double));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range
            counter = 0;
            l = r.List.Double(count);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(double));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.Double(count, precise: false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(double));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }

        [TestMethod]
        public void ListDecimalTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;

            //range
            counter = 0;
            decimal lower = -4200.666m;
            decimal upper = 73000.7676m;
            IList<decimal> l = r.List.Decimal(count, lower, upper);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(decimal));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range
            counter = 0;
            l = r.List.Decimal(count);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(decimal));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.Decimal(count, precise: false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(decimal));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void ListCharTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;

            //range
            counter = 0;
            char lower = 'A';
            char upper = 'Z';
            IList<char> l = r.List.Char(count, lower, upper);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(char));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range
            counter = 0;
            l = r.List.Char(count);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(char));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.Char(count, precise: false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(char));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void ListBoolTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            IList<bool> l = r.List.Bool(count);
            //no range
            counter = 0;
           
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(bool));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.Bool(count, false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(bool));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void ListDateTimeTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;

            //range
            counter = 0;
            DateTime lower = new DateTime(2000, 1, 1);
            DateTime upper = new DateTime(6000, 1, 1);
            IList<DateTime> l = r.List.DateTime(count, lower, upper);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range
            counter = 0;
            l = r.List.DateTime(count);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.DateTime(count, false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void ListGuidTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;

            //no range
            counter = 0;
            IList<Guid> l = r.List.Guid(count);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(Guid));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //no range, not precise
            counter = 0;
            l = r.List.Guid(count, false);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(Guid));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }

        [TestMethod]
        public void ListStringTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            IList<string> l;
            //range, inner precise 20
            l = r.List.String(count, true, 20);
            foreach (var item in l)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(string));
                Assert.AreEqual(20, item.Length);
                counter++;
            }
            Assert.AreEqual(count, counter);
            //Console.WriteLine();
        }
        [TestMethod]
        public void ListStringTest2()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            IList<string> l;
            //finite not precise
            l = r.List.String(count, false);
            foreach (var item in l)
            {
                //Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(string));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
    }
}
