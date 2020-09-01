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
    }
}
