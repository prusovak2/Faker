using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;

namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorStringTests
    {
        [TestMethod]
        public void RandomStringTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                string s = r.String.String();
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                }
                string s2 = r.Random.String();
                Console.WriteLine(s2);
                foreach (var item in s2)
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
                string s = r.String.String(10, false);
                Assert.IsTrue(s.Length <= 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    Assert.IsInstanceOfType(item, typeof(char));
                }
            }
            for (int i = 0; i < 30; i++)
            {
                string s = r.String.String(10, true);
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
                string s = r.String.LowerCaseLetters();
                Console.WriteLine(s);
                foreach (var item in s)
                {
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
                string s = r.String.LowerCaseLetters(10, false);
                Assert.IsTrue(s.Length <= 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    //Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsLower(item));
                }
            }
            for (int i = 0; i < 30; i++)
            {
                string s = r.String.LowerCaseLetters(10, true);
                Assert.IsTrue(s.Length == 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    //Assert.IsInstanceOfType(item, typeof(char));
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
                string s = r.String.UpperCaseLetters(10, false);
                Assert.IsTrue(s.Length <= 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    //Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsUpper(item));
                }
            }
            for (int i = 0; i < 30; i++)
            {
                string s = r.String.UpperCaseLetters(10, true);
                Assert.IsTrue(s.Length == 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    //Assert.IsInstanceOfType(item, typeof(char));
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
                string s = r.String.UpperCaseLetters();
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    //Assert.IsInstanceOfType(item, typeof(char));
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
                string s = r.String.Letters();
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    //Assert.IsInstanceOfType(item, typeof(char));
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
                string s = r.String.Letters(10, false);
                Assert.IsTrue(s.Length <= 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    //Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsLetter(item));
                }
            }
            for (int i = 0; i < 30; i++)
            {
                string s = r.String.Letters(10, true);
                Assert.IsTrue(s.Length == 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    //Assert.IsInstanceOfType(item, typeof(char));
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
                string s = r.String.AlphaNumeric();
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    //Assert.IsInstanceOfType(item, typeof(char));
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
                string s = r.String.AlphaNumeric(10, false);
                Assert.IsTrue(s.Length <= 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    //Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsLetterOrDigit(item));
                }
            }
            for (int i = 0; i < 30; i++)
            {
                string s = r.String.AlphaNumeric(10, true);
                Assert.IsTrue(s.Length == 10);
                Console.WriteLine(s);
                foreach (var item in s)
                {
                    //Assert.IsInstanceOfType(item, typeof(char));
                    Assert.IsTrue(char.IsLetterOrDigit(item));
                }
            }
        }
        [TestMethod]
        public void RandomWhitespaceTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 30; i++)
            {
                char white = r.Char.AsciiWhitespace();
                Console.WriteLine("{0} {1}", (int)white, white);
                Assert.IsTrue(char.IsWhiteSpace(white));
            }
        }

        [TestMethod]
        public void RandomHexStringRangeTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 40; i++)
            {
                string hex = r.String.HexadecimalString(42, 10000);
                Console.WriteLine("hex: {0}", hex);
                ulong val = Convert.ToUInt64(hex, 16);
                Console.WriteLine("dec: {0}", val);
                Assert.IsTrue(val >= 42 && val <= 10000);
            }
        }
        [TestMethod]
        public void RandomHexStringTest()
        {
            RandomGenerator r = new RandomGenerator();
            for (int i = 0; i < 40; i++)
            {
                string hex = r.String.HexadecimalString(5, false);
                Console.WriteLine("hex:{0}", hex);
                Assert.IsTrue(hex.Length <= 7);
                ulong val = Convert.ToUInt64(hex, 16);
                Console.WriteLine("dec:{0}", val);
            }
        }
        
      
    }
}
