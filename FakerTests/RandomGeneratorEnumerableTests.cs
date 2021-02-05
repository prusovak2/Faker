using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorEnumerableTests
    {
        [TestMethod]
        public void IntEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite, no range
            IEnumerable<int> e = r.Enumerable.Int();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(int));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, range
            counter = 0;
            int lower = -100;
            int upper = 100;
            e = r.Enumerable.Int(count,lower,upper);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(int));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.Int(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(int));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.Int(count, precise:false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(int));
                counter++;
            }
            Assert.IsTrue(count>= counter);
        }
        [TestMethod]
        public void UIntEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite, no range
            IEnumerable<uint> e = r.Enumerable.Uint();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(uint));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, range
            counter = 0;
            uint lower = 0;
            uint upper = 200;
            e = r.Enumerable.Uint(count, lower, upper);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(uint));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.Uint(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(uint));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.Uint(count, precise: false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(uint));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void ShortEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite, no range
            IEnumerable<short> e = r.Enumerable.Short();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(short));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, range
            counter = 0;
            short lower = -100;
            short upper = 100;
            e = r.Enumerable.Short(count, lower, upper);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(short));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.Short(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(short));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.Short(count, precise: false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(short));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void UshortEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite, no range
            IEnumerable<ushort> e = r.Enumerable.Ushort();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ushort));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, range
            counter = 0;
            ushort lower = 0;
            ushort upper = 200;
            e = r.Enumerable.Ushort(count, lower, upper);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ushort));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.Ushort(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ushort));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.Ushort(count, precise: false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ushort));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void SbyteEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite, no range
            IEnumerable<sbyte> e = r.Enumerable.Sbyte();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(sbyte));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, range
            counter = 0;
            sbyte lower = -100;
            sbyte upper = 100;
            e = r.Enumerable.Sbyte(count, lower, upper);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(sbyte));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.Sbyte(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(sbyte));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.Sbyte(count, precise: false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(sbyte));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void ByteEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite, no range
            IEnumerable<byte> e = r.Enumerable.Byte();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(byte));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, range
            counter = 0;
            byte lower = 0;
            byte upper = 200;
            e = r.Enumerable.Byte(count, lower, upper);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(byte));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.Byte(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(byte));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.Byte(count, precise: false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(byte));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void UlongEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite, no range
            IEnumerable<ulong> e = r.Enumerable.Ulong();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ulong));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, range
            counter = 0;
            ulong lower = 0;
            ulong upper = 200;
            e = r.Enumerable.Ulong(count, lower, upper);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ulong));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.Ulong(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ulong));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.Ulong(count, precise: false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ulong));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void LongEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite, no range
            IEnumerable<long> e = r.Enumerable.Long();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(long));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, range
            counter = 0;
            long lower = 0;
            long upper = 200;
            e = r.Enumerable.Long(count, lower, upper);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(long));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.Long(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(long));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.Long(count, precise: false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(long));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void DoubleEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite, no range
            IEnumerable<double> e = r.Enumerable.Double();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(double));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, range
            counter = 0;
            double lower = 0;
            double upper = 200;
            e = r.Enumerable.Double(count, lower, upper);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(double));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.Double(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(double));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.Double(count, precise: false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(double));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void FloatEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite, no range
            IEnumerable<float> e = r.Enumerable.Float();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(float));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, range
            counter = 0;
            float lower = 0;
            float upper = 200;
            e = r.Enumerable.Float(count, lower, upper);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(float));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.Float(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(float));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.Float(count, precise: false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(float));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void DecimalEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite, no range
            IEnumerable<decimal> e = r.Enumerable.Decimal();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(decimal));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, range
            counter = 0;
            decimal lower = -100;
            decimal upper = 200;
            e = r.Enumerable.Decimal(count, lower, upper);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(decimal));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.Decimal(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(decimal));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.Decimal(count, precise: false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(decimal));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void DateTimeEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite, no range
            IEnumerable<DateTime> e = r.Enumerable.DateTime();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, range
            counter = 0;
            DateTime lower = new DateTime(2000, 1, 1);
            DateTime upper = new DateTime(6000, 1, 1);
            e = r.Enumerable.DateTime(count, lower, upper);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.DateTime(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.DateTime(count, precise:false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void GuidEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite
            IEnumerable<Guid> e = r.Enumerable.Guid();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(Guid));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.Guid(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(Guid));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.Guid(count, false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(Guid));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void BoolEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite
            IEnumerable<bool> e = r.Enumerable.Bool();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(bool));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.Bool(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(bool));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.Bool(count, false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(bool));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void CharEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite, no range
            IEnumerable<char> e = r.Enumerable.Char();
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(char));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

            //finite, range
            counter = 0;
            char lower = 'A';
            char upper = 'Z';
            e = r.Enumerable.Char(count, lower, upper);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(char));
                Assert.IsTrue(item >= lower && item <= upper);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range
            counter = 0;
            e = r.Enumerable.Char(count);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(char));
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite, no range, not precise
            counter = 0;
            e = r.Enumerable.Char(count, precise: false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(char));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
        [TestMethod]
        public void StringEnumerableTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            //infinite, inner precise 255
            IEnumerable<string> e = r.Enumerable.String();
            foreach (var item in e)
            {
                //Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(string));
                Assert.AreEqual(255, item.Length);
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Console.WriteLine();

           
/*
            //finite, inner not precise
            counter = 0;
            e = r.Enumerable.String(count, true, 30, false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(string));
                Assert.IsTrue(item.Length <= 30);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();

            //finite not precise
            counter = 0;
            e = r.Enumerable.String(count, false);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(string));
                counter++;
            }
            Assert.IsTrue(count >= counter);*/
        }
        [TestMethod]
        public void StringEnumerableTest2()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            IEnumerable<string> e;
            //finite, range inner precise 20
            e = r.Enumerable.String(count, true, 20);
            foreach (var item in e)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(string));
                Assert.AreEqual(20, item.Length);
                counter++;
            }
            Assert.AreEqual(count, counter);
            Console.WriteLine();
        }
        [TestMethod]
        public void StringEnumerableTest3()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            int counter = 0;
            IEnumerable<string> e;
            //finite not precise
            counter = 0;
            e = r.Enumerable.String(count, false);
            foreach (var item in e)
            {
                //Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(string));
                counter++;
            }
            Assert.IsTrue(count >= counter);
        }
    }

}

