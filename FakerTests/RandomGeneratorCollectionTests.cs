using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System.Reflection;
using System.Linq;

namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorCollectionTests
    {
        [TestMethod]
        public void RandomCollectionParams()
        {
            RandomGenerator r = new RandomGenerator();
            ICollection<byte> c = r.GenericCollection(r.Random.Byte, 30,(byte)5, byte.MaxValue);
            Console.WriteLine(c.Count);
            foreach (var item in c)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(byte));
                Assert.IsTrue(item >= 5 && item <= byte.MaxValue);
            }
            // Nullable params - Func<TMember?, TMember?, TMember> overload
            double? d = 5d;
            ICollection<double> c2 = r.GenericCollection<double>(r.Random.Double, 30, d, Double.MaxValue);
            Console.WriteLine(c.Count);
            foreach (var item in c2)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(double));
                Assert.IsTrue(item >= 5 && item < double.MaxValue);
            }
        }

        [TestMethod]
        public void RandomCollectionDefaultFuncTest()
        {
            RandomGenerator r = new RandomGenerator();
            ICollection<ushort> c = r.GenericCollection<ushort>(30);
            Console.WriteLine("ushort");
            foreach (var item in c)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(ushort));
            }
            ICollection<DateTime> c2 = r.GenericCollection<DateTime>(30);
            Console.WriteLine("DateTime");
            foreach (var item in c2)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
            }

            //no default random function
            Assert.ThrowsException<FakerException>(() => { ICollection<ValueClass> c3 = r.GenericCollection<ValueClass>(30); });

            ICollection<Char> c3 = r.GenericCollection<Char>(30);
            Console.WriteLine("char");
            foreach (var item in c3)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(char));
            }
        }
        [TestMethod]
        public void RandomListDefaultFuncTest()
        {
            RandomGenerator r = new RandomGenerator();
            IList<ushort> c = r.GenericList<ushort>(30);
            Console.WriteLine("ushort");
            foreach (var item in c)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(ushort));
            }
            IList<DateTime> c2 = r.GenericList<DateTime>(30);
            Console.WriteLine("DateTime");
            foreach (var item in c2)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
            }

            Assert.ThrowsException<FakerException>(() => { IList<ValueClass> c3 = r.GenericList<ValueClass>(30); });

            IList<Char> c3 = r.GenericList<Char>(30);
            Console.WriteLine("char");
            foreach (var item in c3)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(char));
            }
        }
        [TestMethod]
        public void RandomCollectionParamlessTest()
        {
            RandomGenerator r = new RandomGenerator();
            ICollection<char> c = r.GenericCollection(r.Char.LowerCaseLetter, 30);
            Console.WriteLine(c.Count);
            foreach (var item in c)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(Char));
                Assert.IsTrue(char.IsLower(item));
            }
            ICollection<DateTime> c2 = r.GenericCollection<DateTime>(r.Random.DateTime, 30);
            Console.WriteLine(c.Count);
            foreach (var item in c2)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
            }
        }
        [TestMethod]
        public void RandomCollectionNotPreciseTest()
        {
            RandomGenerator r = new RandomGenerator();
            Console.WriteLine("default func");
            ICollection<float> c= r.GenericCollection<float>(100, false);
            Console.WriteLine(c.Count);
            Assert.IsTrue(c.Count <= 100);
            foreach (var item in c)
            {
                Assert.IsInstanceOfType(item, typeof(float));
                Console.WriteLine(item);
            }

            Console.WriteLine("paramless");
            ICollection<char> c2 = r.GenericCollection(r.Char.AlphaNumeric, 100, false);
            Console.WriteLine(c2.Count);
            Assert.IsTrue(c2.Count <= 100);
            foreach (var item in c2)
            {
                Assert.IsInstanceOfType(item, typeof(char));
                Assert.IsTrue(char.IsLetterOrDigit(item));
                Console.WriteLine(item);
            }

            Console.WriteLine("with params");
            Decimal? m = 200m; 
            ICollection<decimal> c3 = r.GenericCollection(r.Random.Decimal, 100, m, 100000m, false);
            Console.WriteLine(c3.Count);
            Assert.IsTrue(c3.Count <= 100);
            foreach (var item in c3)
            {
                Assert.IsInstanceOfType(item, typeof(decimal));
                Assert.IsTrue(item >= 200m && item <= 100000m);
                Console.WriteLine(item);
            }

        }
        [TestMethod]
        public void RandomListNotPreciseTest()
        {
            RandomGenerator r = new RandomGenerator();
            Console.WriteLine("default func");
            IList<float> c = r.GenericList<float>(100, false);
            Console.WriteLine(c.Count);
            Assert.IsTrue(c.Count <= 100);
            foreach (var item in c)
            {
                Assert.IsInstanceOfType(item, typeof(float));
                Console.WriteLine(item);
            }

            Console.WriteLine("paramless");
            IList<char> c2 = r.GenericList(r.Char.AlphaNumeric, 100, false);
            Console.WriteLine(c2.Count);
            Assert.IsTrue(c2.Count <= 100);
            foreach (var item in c2)
            {
                Assert.IsInstanceOfType(item, typeof(char));
                Assert.IsTrue(char.IsLetterOrDigit(item));
                Console.WriteLine(item);
            }

            Console.WriteLine("with params");
            decimal? m = 200m;
            IList<decimal> c3 = r.GenericList(r.Random.Decimal, 100, m, 100000m, false);
            Console.WriteLine(c3.Count);
            Assert.IsTrue(c3.Count <= 100);
            foreach (var item in c3)
            {
                Assert.IsInstanceOfType(item, typeof(decimal));
                Assert.IsTrue(item >= 200m && item <= 100000m);
                Console.WriteLine(item);
            }
        }
        [TestMethod]
        public void RandomListParamlessTest()
        {
            RandomGenerator r = new RandomGenerator();
            IList<char> c = r.GenericList(r.Char.LowerCaseLetter, 30);
            Console.WriteLine(c.Count);
            foreach (var item in c)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(Char));
                Assert.IsTrue(char.IsLower(item));
            }
            //IList<DateTime> c2 = r.GenericList<DateTime>(r.Random.DateTime, 30);
            IList<DateTime> c2 = r.GenericList<DateTime>(30);
            Console.WriteLine(c.Count);
            foreach (var item in c2)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
            }
        }
        [TestMethod]
        public void RandomListParams()
        {
            RandomGenerator r = new RandomGenerator();
            IList<byte> c = r.GenericList(r.Random.Byte, 30, (byte)5, byte.MaxValue);
            Console.WriteLine(c.Count);
            foreach (var item in c)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(byte));
                Assert.IsTrue(item >= 5 && item <= byte.MaxValue);
            }
            // Nullable params - Func<TMember?, TMember?, TMember> overload
            double? d = 5d;
            IList<double> c2 = r.GenericList(r.Random.Double, 30, d, Double.MaxValue);
            Console.WriteLine(c.Count);
            foreach (var item in c2)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(double));
                Assert.IsTrue(item >= 5 && item < double.MaxValue);
            }
        }
        [TestMethod]
        public void RandomCollectionOfCollectionsTest()
        {
            RandomGenerator r = new RandomGenerator();
            ICollection<string> c = r.GenericCollection(r.String.AlphaNumeric, 20, true, 30, false);
            Console.WriteLine(c.Count);
            Assert.AreEqual(20, c.Count);
            foreach (var item in c)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(string));
                Assert.IsTrue(item.Length <= 30);
            }
            ICollection<ICollection<sbyte>> c2 = r.GenericCollection(r.GenericCollection<sbyte>, 20, false, 30, true);
            Console.WriteLine(c2.Count);
            Assert.IsTrue( c2.Count<=20);
            foreach (var item in c2)
            {
                foreach (var b in item)
                {
                    Console.Write("{0}, ", b);
                    Assert.IsInstanceOfType(b, typeof(sbyte));
                }
                Assert.IsInstanceOfType(item, typeof(ICollection<sbyte>));
                Assert.AreEqual(30,item.Count);
                Console.WriteLine();
            }
        }
        [TestMethod]
        public void RandomListOfCollectionsTest()
        {
            RandomGenerator r = new RandomGenerator();
            IList<string> c = r.GenericList(r.String.AlphaNumeric, 20, true, 30, false);
            Console.WriteLine(c.Count);
            Assert.AreEqual(20, c.Count);
            foreach (var item in c)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(string));
                Assert.IsTrue(item.Length <= 30);
            }
            IList<ICollection<sbyte>> c2 = r.GenericList(r.GenericCollection<sbyte>, 20, false, 30, true);
            Console.WriteLine(c2.Count);
            Assert.IsTrue(c2.Count <= 20);
            foreach (var item in c2)
            {
                foreach (var b in item)
                {
                    Console.Write("{0}, ", b);
                    Assert.IsInstanceOfType(b, typeof(sbyte));
                }
                Assert.IsInstanceOfType(item, typeof(ICollection<sbyte>));
                Assert.AreEqual(30, item.Count);
                Console.WriteLine();
            }
        }
        [TestMethod]
        public void RandomEnumerableParamlessTest()
        {
            int count = 30;
            RandomGenerator r = new RandomGenerator();
            IEnumerable<char> c = r.GenericEnumerable(r.Char.LowerCaseLetter, count, true);
            int counter = 0;
            foreach (var item in c)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(Char));
                Assert.IsTrue(char.IsLower(item));
                counter++;
            }
            Assert.AreEqual(count, counter);
            char[] chars = c.ToArray();

            counter = 0;
            IEnumerable<DateTime> c2 = r.GenericEnumerable<DateTime>(r.Random.DateTime, count, precise:false);
            foreach (var item in c2)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
                counter++;
            }
            Assert.IsTrue(counter <= count);
        }
        [TestMethod]
        public void RandomEnumerableParams()
        {
            int count = 30;
            RandomGenerator r = new RandomGenerator();
            IEnumerable<byte> c = r.GenericEnumerable(r.Random.Byte, count, (byte)5, byte.MaxValue);
            int counter = 0;
            foreach (var item in c)
            {
                Console.WriteLine("{0}: {1}",counter,item);
                Assert.IsInstanceOfType(item, typeof(byte));
                Assert.IsTrue(item >= 5 && item <= byte.MaxValue);
                counter++;
            }
            Assert.AreEqual(count, counter);
            counter = 0;
            // Nullable params - Func<TMember?, TMember?, TMember> overload
            double? d = 5d;
            IEnumerable<double> c2 = r.GenericEnumerable(r.Random.Double, count, d, Double.MaxValue, false);
            foreach (var item in c2)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(double));
                Assert.IsTrue(item >= 5 && item < double.MaxValue);
                counter++;
            }
            Assert.IsTrue(counter <= count);
        }
        [TestMethod]
        public void RandomEnumerableDefaultFuncTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            IEnumerable<ushort> c = r.GenericEnumerable<ushort>(count);
            Console.WriteLine("ushort");
            int counter = 0;
            foreach (var item in c)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ushort));
                counter++;
            }
            Assert.AreEqual(count, counter);

            IEnumerable<DateTime> c2 = r.GenericEnumerable<DateTime>(30, false);
            Console.WriteLine("DateTime");
            counter = 0;
            foreach (var item in c2)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
                counter++;
            }
            Assert.IsTrue(counter <= count);

            Assert.ThrowsException<FakerException>(() => { ICollection<ValueClass> c3 = r.GenericCollection<ValueClass>(30); });

            IEnumerable<char> c3 = r.GenericEnumerable<char>(30, false);
            Console.WriteLine("char");
            counter = 0;
            foreach (var item in c3)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(char));
                counter++;
            }
            Assert.IsTrue(counter <= count);
        }
        [TestMethod]
        public void RandomEnumOfCollectionsTest()
        {
            int outerCount = 20;
            int counter = 0;
            RandomGenerator r = new RandomGenerator();
            IEnumerable<string> c = r.GenericEnumerable(r.String.AlphaNumeric, outerCount, true, 30, false);
            foreach (var item in c)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(string));
                Assert.IsTrue(item.Length <= 30);
                counter++;
            }
            Assert.AreEqual(outerCount, counter);

            IEnumerable<IEnumerable<sbyte>> c2 = r.GenericEnumerable(r.GenericEnumerable<sbyte>, outerCount, false, 30, true);
            foreach (var item in c2)
            {
                int innerCounter = 0;
                foreach (var b in item)
                {
                    Console.Write("{0}, ", b);
                    Assert.IsInstanceOfType(b, typeof(sbyte));
                    innerCounter++;
                }
                Assert.IsInstanceOfType(item, typeof(IEnumerable<sbyte>));
                Assert.AreEqual(30, innerCounter);
                Console.WriteLine();
            }
            Assert.IsTrue(counter <= outerCount);
        }

        [TestMethod]
        public void RandomEnumerableEndlessParamlessTest()
        {
            int count = 30;
            RandomGenerator r = new RandomGenerator();
            IEnumerable<char> c = r.InfiniteGenericEnumerable(r.Char.LowerCaseLetter);
            int counter = 0;
            foreach (var item in c)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(Char));
                Assert.IsTrue(char.IsLower(item));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Assert.AreEqual(count, counter);

            counter = 0;
            IEnumerable<DateTime> c2 = r.InfiniteGenericEnumerable<DateTime>(r.Random.DateTime);
            foreach (var item in c2)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Assert.AreEqual(count, counter);
        }
        [TestMethod]
        public void RandomEnumerableEndlessParams()
        {
            int count = 30;
            RandomGenerator r = new RandomGenerator();
            IEnumerable<byte> c = r.InfiniteGenericEnumerable(r.Random.Byte, (byte)5, byte.MaxValue);
            int counter = 0;
            foreach (var item in c)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(byte));
                Assert.IsTrue(item >= 5 && item <= byte.MaxValue);
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Assert.AreEqual(count, counter);
            // Nullable params - Func<TMember?, TMember?, TMember> overload
            counter = 0;
            double? d = 5d;
            IEnumerable<double> c2 = r.InfiniteGenericEnumerable(r.Random.Double, d, Double.MaxValue);
            foreach (var item in c2)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(double));
                Assert.IsTrue(item >= 5 && item < double.MaxValue);
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Assert.AreEqual(count, counter);
        }
        [TestMethod]
        public void RandomEnumerableEndlessDefaultFuncTest()
        {
            RandomGenerator r = new RandomGenerator();
            int count = 30;
            IEnumerable<ushort> c = r.InfiniteGenericEnumerable<ushort>();
            Console.WriteLine("ushort");
            int counter = 0;
            foreach (var item in c)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(ushort));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Assert.AreEqual(count, counter);

            IEnumerable<DateTime> c2 = r.InfiniteGenericEnumerable<DateTime>();
            Console.WriteLine("DateTime");
            counter = 0;
            foreach (var item in c2)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Assert.IsTrue(counter <= count);

            Assert.ThrowsException<FakerException>(() => { ICollection<ValueClass> c3 = r.GenericCollection<ValueClass>(30); });

            IEnumerable<char> c3 = r.InfiniteGenericEnumerable<char>();
            Console.WriteLine("char");
            counter = 0;
            foreach (var item in c3)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(char));
                counter++;
                if (counter == count)
                {
                    break;
                }
            }
            Assert.IsTrue(counter <= count);
        }
        [TestMethod]
        public void RandomInfiniteEnumOfCollectionsTest()
        {
            int outerCount = 20;
            int counter = 0;
            RandomGenerator r = new RandomGenerator();
            IEnumerable<string> c = r.InfiniteGenericEnumerable(r.String.AlphaNumeric, 30, false);
            foreach (var item in c)
            {
                Console.WriteLine("{0}: {1}", counter, item);
                Assert.IsInstanceOfType(item, typeof(string));
                Assert.IsTrue(item.Length <= 30);
                counter++;
                if (counter == outerCount)
                {
                    break;
                }
            }
            Assert.AreEqual(outerCount, counter);

            counter = 0;
            IEnumerable<IEnumerable<sbyte>> c2 = r.InfiniteGenericEnumerable(r.GenericEnumerable<sbyte>, 30, true);
            foreach (var item in c2)
            {
                int innerCounter = 0;
                foreach (var b in item)
                {
                    Console.Write("{0}, ", b);
                    Assert.IsInstanceOfType(b, typeof(sbyte));
                    innerCounter++;
                }
                Assert.IsInstanceOfType(item, typeof(IEnumerable<sbyte>));
                Assert.AreEqual(30, innerCounter);
                Console.WriteLine();
                counter++;
                if (counter == outerCount)
                {
                    break;
                }
            }
            Assert.IsTrue(counter <= outerCount);
        }
    }
}

