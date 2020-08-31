using System;
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
                char c = r.Char();
                Assert.IsInstanceOfType(c, typeof(Char));
                Console.WriteLine(c);
            }
        }
        [TestMethod]
        public void RandomCharRangeTest()
        {
            RandomGenerator r = new RandomGenerator();
            Dictionary<char, int> counter = new Dictionary<char, int>();

            for (int i = 0; i < 200; i++)
            {
                char c = r.Char('A','Z');
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
                char c = r.Digit();
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
        public void RandomHexDigitTest()
        {
            RandomGenerator r = new RandomGenerator();
            Dictionary<char, int> counter = new Dictionary<char, int>();

            for (int i = 0; i < 200; i++)
            {
                char c = r.HexadecimalDigit();
                Console.WriteLine("hex:{0}",c);
                if (!counter.ContainsKey(c))
                {
                    counter.Add(c, 0);
                }
                counter[c]++;
                Assert.IsInstanceOfType(c, typeof(Char));
                Assert.IsTrue(char.IsLetterOrDigit(c));
                string s = new string(new char[] { c});
                byte val = Convert.ToByte(s, 16);
                Console.WriteLine("dec:{0}",val);

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
                char c = r.LowerCaseLetter();
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
                char c = r.UpperCaseLetter();
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
                char c = r.Letter();
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
                char c = r.AlphaNumeric();
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
                char c = r.Ascii();
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
                char c = r.Vowel();
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
                char c = r.Consonant();
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
        [TestMethod]
        public void RandomStringTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                string s = r.String();
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                }
            }
        }
        [TestMethod]
        public void RandomStringParamsTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                string s = r.String(10, false);
                Assert.IsTrue(s.Length <= 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                }
            }
            for (int i = 0; i < 30; i++)
            {
                string s = r.String(10, true);
                Assert.IsTrue(s.Length == 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                }
            }
        }
        [TestMethod]
        public void RandomLoweCaseStringTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                string s = r.LowerCaseString();
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsLower(item));
                }
            }
        }
        [TestMethod]
        public void RandomLowerCaseStringParamsTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                string s = r.LowerCaseString(10, false);
                Assert.IsTrue(s.Length <= 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsLower(item));
                }
            }
            for (int i = 0; i < 30; i++)
            {
                string s = r.LowerCaseString(10, true);
                Assert.IsTrue(s.Length == 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsLower(item));
                }
            }
        }
        [TestMethod]
        public void RandomUpperCaseStringParamsTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                string s = r.UpperCaseLetters(10, false);
                Assert.IsTrue(s.Length <= 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsUpper(item));
                }
            }
            for (int i = 0; i < 30; i++)
            {
                string s = r.UpperCaseLetters(10, true);
                Assert.IsTrue(s.Length == 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsUpper(item));
                }
            }
        }
        [TestMethod]
        public void RandomUpperCaseStringTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                string s = r.UpperCaseLetters();
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsUpper(item));
                }
            }
        }
        [TestMethod]
        public void RandomLettersTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                string s = r.Letters();
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsLetter(item));
                }
            }
        }
        [TestMethod]
        public void RandomLettersParamsTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                string s = r.Letters(10, false);
                Assert.IsTrue(s.Length <= 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsLetter(item));
                }
            }
            for (int i = 0; i < 30; i++)
            {
                string s = r.Letters(10, true);
                Assert.IsTrue(s.Length == 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsLetter(item));
                }
            }
        }
        [TestMethod]
        public void RandomAplhanumericStringTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                string s = r.AlphaNumericString();
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsLetterOrDigit(item));
                }
            }
        }
        [TestMethod]
        public void RandomAlphanumeriStringParamsTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                string s = r.AlphaNumericString(10, false);
                Assert.IsTrue(s.Length <= 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsLetterOrDigit(item));
                }
            }
            for (int i = 0; i < 30; i++)
            {
                string s = r.AlphaNumericString(10, true);
                Assert.IsTrue(s.Length == 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsLetterOrDigit(item));
                }
            }
        }
        [TestMethod]
        public void RandomHexStringRangeTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 40; i++)
            {
                string hex = r.HexadecimalString(42, 10000);
                Console.WriteLine("hex:{0}",hex);
                ulong val = Convert.ToUInt64(hex, 16);
                Console.WriteLine("dec:{0}",val);
                Assert.IsTrue(val >= 42 && val <= 10000);
            }
        }
        [TestMethod]
        public void RandomHexStringTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 40; i++)
            {
                string hex = r.HexadecimalString(5, false);
                Console.WriteLine("hex:{0}", hex);
                Assert.IsTrue(hex.Length <= 5);
                ulong val = Convert.ToUInt64(hex, 16);
                Console.WriteLine("dec:{0}", val);
            }
        }
    }
}
