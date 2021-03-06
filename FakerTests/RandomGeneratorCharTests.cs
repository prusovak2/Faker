﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System.Collections.Immutable;
using System.Linq;

namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorCharTests
    {
        [TestMethod]
        public void RandomCharTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 200; i++)
            {
                char c = r.Char.Char();
                Assert.IsInstanceOfType(c, typeof(Char));
                Console.WriteLine(c);
                char c2 = r.Random.Char();
                Assert.IsInstanceOfType(c2, typeof(Char));
                Console.WriteLine(c2);
            }
        }
        [TestMethod]
        public void RandomCharRangeTest()
        {
            RandomGenerator r = new RandomGenerator();
            Dictionary<char, int> counter = new Dictionary<char, int>();

            for (int i = 0; i < 200; i++)
            {
                char c = r.Char.Char('A','Z');
                Console.WriteLine(c);
                if (!counter.ContainsKey(c))
                {
                    counter.Add(c, 0);
                }
                counter[c]++;
                Assert.IsInstanceOfType(c, typeof(Char));
                Assert.IsTrue(char.IsUpper(c));

            }
            Console.WriteLine();
            Console.WriteLine("counter dictionary");
            var sorted = counter.ToImmutableSortedDictionary();
            foreach (var item in sorted)
            {
                Console.WriteLine("{0} appeared {1} times", item.Key, item.Value);
            }
        }
        [TestMethod]
        public void RandomDigitTest()
        {
            RandomGenerator r = new RandomGenerator();
            Dictionary<char, int> counter = new Dictionary<char, int>();

            for (int i = 0; i < 200; i++)
            {
                char c = r.Char.Digit();
                Console.WriteLine(c);
                if (!counter.ContainsKey(c))
                {
                    counter.Add(c, 0);
                }
                counter[c]++;
                Assert.IsInstanceOfType(c, typeof(Char));
                Assert.IsTrue(char.IsDigit(c));
                
            }
            Console.WriteLine();
            Console.WriteLine("counter dictionary");
            var sorted =counter.ToImmutableSortedDictionary();
            foreach (var item in sorted)
            {
                Console.WriteLine("{0} appeared {1} times", item.Key, item.Value);
            }
        }
        [TestMethod]
        public void RandomHexDigitUpperTest()
        {
            RandomGenerator r = new RandomGenerator();
            Dictionary<char, int> counter = new Dictionary<char, int>();

            for (int i = 0; i < 200; i++)
            {
                char c = r.Char.HexadecimalDigitUpper();
                Console.WriteLine("hex: {0}",c);
                if (!counter.ContainsKey(c))
                {
                    counter.Add(c, 0);
                }
                counter[c]++;
                
                Assert.IsTrue(char.IsDigit(c) || char.IsUpper(c));
                string s = new string(new char[] { c});
                byte val = Convert.ToByte(s, 16);
                Console.WriteLine("dec: {0}",val);

            }
            Console.WriteLine();
            Console.WriteLine("counter dictionary");
            var sorted = counter.ToImmutableSortedDictionary();
            foreach (var item in sorted)
            {
                Console.WriteLine("{0} appeared {1} times", item.Key, item.Value);
            }
        }
        [TestMethod]
        public void RandomHexDigitLowerTest()
        {
            RandomGenerator r = new RandomGenerator();
            Dictionary<char, int> counter = new Dictionary<char, int>();

            for (int i = 0; i < 200; i++)
            {
                char c = r.Char.HexadecimalDigitLower();
                Console.WriteLine("hex: {0}", c);
                if (!counter.ContainsKey(c))
                {
                    counter.Add(c, 0);
                }
                counter[c]++;

                Assert.IsTrue(char.IsDigit(c) || char.IsLower(c));
                string s = new string(new char[] { c });
                byte val = Convert.ToByte(s, 16);
                Console.WriteLine("dec: {0}", val);

            }
            Console.WriteLine();
            Console.WriteLine("counter dictionary");
            var sorted = counter.ToImmutableSortedDictionary();
            foreach (var item in sorted)
            {
                Console.WriteLine("{0} appeared {1} times", item.Key, item.Value);
            }
        }

        [TestMethod]
        public void RandomLowerCaseTest()
        {
            RandomGenerator r = new RandomGenerator();
            Dictionary<char, int> counter = new Dictionary<char, int>();

            for (int i = 0; i < 200; i++)
            {
                char c = r.Char.LowerCaseLetter();
                Console.WriteLine(c);
                if (!counter.ContainsKey(c))
                {
                    counter.Add(c, 0);
                }
                counter[c]++;
                Assert.IsInstanceOfType(c, typeof(Char));
                Assert.IsTrue(char.IsLower(c));

            }
            Console.WriteLine();
            Console.WriteLine("counter dictionary");
            var sorted = counter.ToImmutableSortedDictionary();
            foreach (var item in sorted)
            {
                Console.WriteLine("{0} appeared {1} times", item.Key, item.Value);
            }
        }
        [TestMethod]
        public void RandomUpperCaseTest()
        {
            RandomGenerator r = new RandomGenerator();
            Dictionary<char, int> counter = new Dictionary<char, int>();

            for (int i = 0; i < 200; i++)
            {
                char c = r.Char.UpperCaseLetter();
                Console.WriteLine(c);
                if (!counter.ContainsKey(c))
                {
                    counter.Add(c, 0);
                }
                counter[c]++;
                Assert.IsInstanceOfType(c, typeof(Char));
                Assert.IsTrue(char.IsUpper(c));

            }
            Console.WriteLine();
            Console.WriteLine("counter dictionary");
            var sorted = counter.ToImmutableSortedDictionary();
            foreach (var item in sorted)
            {
                Console.WriteLine("{0} appeared {1} times", item.Key, item.Value);
            }
        }
        [TestMethod]
        public void RandomLetterTest()
        {
            RandomGenerator r = new RandomGenerator();
            Dictionary<char, int> counter = new Dictionary<char, int>();

            for (int i = 0; i < 200; i++)
            {
                char c = r.Char.Letter();
                Console.WriteLine(c);
                if (!counter.ContainsKey(c))
                {
                    counter.Add(c, 0);
                }
                counter[c]++;
                Assert.IsInstanceOfType(c, typeof(Char));
                Assert.IsTrue(char.IsLetter(c));

            }
            Console.WriteLine();
            Console.WriteLine("counter dictionary");
            var sorted = counter.ToImmutableSortedDictionary();
            foreach (var item in sorted)
            {
                Console.WriteLine("{0} appeared {1} times", item.Key, item.Value);
            }
        }
        [TestMethod]
        public void RandomAplhanumericCharTest()
        {
            RandomGenerator r = new RandomGenerator();
            Dictionary<char, int> counter = new Dictionary<char, int>();

            for (int i = 0; i < 400; i++)
            {
                char c = r.Char.AlphaNumeric();
                Console.WriteLine(c);
                if (!counter.ContainsKey(c))
                {
                    counter.Add(c, 0);
                }
                counter[c]++;
                Assert.IsInstanceOfType(c, typeof(Char));
                Assert.IsTrue(char.IsLetterOrDigit(c));

            }
            Console.WriteLine();
            Console.WriteLine("counter dictionary");
            var sorted = counter.ToImmutableSortedDictionary();
            foreach (var item in sorted)
            {
                Console.WriteLine("{0} appeared {1} times", item.Key, item.Value);
            }
        }
        [TestMethod]
        public void RandomAsciiTest()
        {
            RandomGenerator r = new RandomGenerator();
            Dictionary<char, int> counter = new Dictionary<char, int>();

            for (int i = 0; i < 400; i++)
            {
                char c = r.Char.Ascii();
                Console.WriteLine(c);
                if (!counter.ContainsKey(c))
                {
                    counter.Add(c, 0);
                }
                counter[c]++;
                Assert.IsInstanceOfType(c, typeof(Char));
                Assert.IsTrue(c<=127);

            }
            Console.WriteLine();
            Console.WriteLine("counter dictionary");
            var sorted = counter.ToImmutableSortedDictionary();
            foreach (var item in sorted)
            {
                Console.WriteLine("{0} appeared {1} times", item.Key, item.Value);
            }
        }
        [TestMethod]
        public void RandomVowelTest()
        {
            RandomGenerator r = new RandomGenerator();
            Dictionary<char, int> counter = new Dictionary<char, int>();
            char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };

            for (int i = 0; i < 100; i++)
            {
                char c = r.Char.Vowel();
                Console.WriteLine(c);
                if (!counter.ContainsKey(c))
                {
                    counter.Add(c, 0);
                }
                counter[c]++;
                Assert.IsInstanceOfType(c, typeof(Char));
                Assert.IsTrue(vowels.Contains(c));

            }
            Console.WriteLine();
            Console.WriteLine("counter dictionary");
            var sorted = counter.ToImmutableSortedDictionary();
            foreach (var item in sorted)
            {
                Console.WriteLine("{0} appeared {1} times", item.Key, item.Value);
            }
        }
        [TestMethod]
        public void RandomConsonantTest()
        {
            RandomGenerator r = new RandomGenerator();
            Dictionary<char, int> counter = new Dictionary<char, int>();
            string cons = "BCDFGHJKLMNPQRSTVWXZ".ToLower();
            
            char[] consonant = cons.ToCharArray();
            Console.WriteLine(consonant.Length);
            for (int i = 0; i < 100; i++)
            {
                char c = r.Char.Consonant();
                Console.WriteLine(c);
                if (!counter.ContainsKey(c))
                {
                    counter.Add(c, 0);
                }
                counter[c]++;
                Assert.IsInstanceOfType(c, typeof(Char));
                Assert.IsTrue(consonant.Contains(c));

            }
            Console.WriteLine();
            Console.WriteLine("counter dictionary");
            var sorted = counter.ToImmutableSortedDictionary();
            foreach (var item in sorted)
            {
                Console.WriteLine("{0} appeared {1} times", item.Key, item.Value);
            }
        }
    }
}
