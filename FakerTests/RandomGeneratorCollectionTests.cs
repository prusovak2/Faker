using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System.Reflection;


namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorCollectionTests
    {
        [TestMethod]
        public void RandomCollectionParams()
        {
            RandomGenerator r = new RandomGenerator();
            ICollection<byte> c = r.RandomCollection(r.RandomByte, (byte)5, byte.MaxValue, 30);
            Console.WriteLine(c.Count);
            foreach (var item in c)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(byte));
                Assert.IsTrue(item >= 5 && item <= byte.MaxValue);
            }
            ICollection<double> c2 = r.RandomCollection(r.RandomDouble, 5d, Double.MaxValue, 30);
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
            ICollection<ushort> c = r.RandomCollection<ushort>(30);
            Console.WriteLine("ushort");
            foreach (var item in c)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(ushort));
            }
            ICollection<DateTime> c2 = r.RandomCollection<DateTime>(30);
            Console.WriteLine("DateTime");
            foreach (var item in c2)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
            }

            Assert.ThrowsException<FakerException>(() => { ICollection<ValueClass> c3 = r.RandomCollection<ValueClass>(30); });

            ICollection<Char> c3 = r.RandomCollection<Char>(30);
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
            IList<ushort> c = r.RandomList<ushort>(30);
            Console.WriteLine("ushort");
            foreach (var item in c)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(ushort));
            }
            IList<DateTime> c2 = r.RandomList<DateTime>(30);
            Console.WriteLine("DateTime");
            foreach (var item in c2)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(DateTime));
            }

            Assert.ThrowsException<FakerException>(() => { IList<ValueClass> c3 = r.RandomList<ValueClass>(30); });

            IList<Char> c3 = r.RandomList<Char>(30);
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
            ICollection<char> c = r.RandomCollection(r.RandomLowerCaseLetter, 30);
            Console.WriteLine(c.Count);
            foreach (var item in c)
            {
                Console.WriteLine(item);
                Assert.IsInstanceOfType(item, typeof(Char));
                Assert.IsTrue(char.IsLower(item));
            }
            ICollection<DateTime> c2 = r.RandomCollection(r.RandomDateTime, 30);
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
            ICollection<float> c= r.RandomCollection<float>(100, false);
            Console.WriteLine(c.Count);
            Assert.IsTrue(c.Count <= 100);
            foreach (var item in c)
            {
                Assert.IsInstanceOfType(item, typeof(float));
                Console.WriteLine(item);
            }

            Console.WriteLine("paramless");
            ICollection<char> c2 = r.RandomCollection(r.RandomAlphanumericChar, 100, false);
            Console.WriteLine(c2.Count);
            Assert.IsTrue(c2.Count <= 100);
            foreach (var item in c2)
            {
                Assert.IsInstanceOfType(item, typeof(char));
                Assert.IsTrue(char.IsLetterOrDigit(item));
                Console.WriteLine(item);
            }

            Console.WriteLine("with params");
            ICollection<decimal> c3 = r.RandomCollection(r.RandomDecimal,200m, 100000m,100,false);
            Console.WriteLine(c3.Count);
            Assert.IsTrue(c3.Count <= 100);
            foreach (var item in c3)
            {
                Assert.IsInstanceOfType(item, typeof(decimal));
                Assert.IsTrue(item >= 200m && item <= 100000m);
                Console.WriteLine(item);
            }
        }
    }
}

