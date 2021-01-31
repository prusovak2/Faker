using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;

namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorTests
    {
        public class SomeClass
        {
            public int somePrime { get; set; }
        }
        public class SomeFaker : BaseFaker<SomeClass>
        {
            public SomeFaker()
            {
                RuleFor(c => c.somePrime, rg => rg.Pick(2, 3, 5, 7, 13, 17, 73));
            }
        }


        [TestMethod]
        public void PickIListTest()
        {
            RandomGenerator rg = new RandomGenerator();
            int lower = 0;
            int upper = 100;
            int count = 50;
            IList<int> list = rg.List.Int(count, lower, upper);
            for (int i = 0; i < 1000; i++)
            {
                int picked = rg.Pick(list);
                Console.WriteLine(picked);
                Assert.IsTrue(list.Contains(picked));
            }

            DateTime lower2 = new DateTime(2010, 1, 1);
            DateTime upper2 = new DateTime(2020, 3, 3);
            IList<DateTime> list2 = rg.List.DateTime(count, lower2, upper2);
            for (int i = 0; i < 1000; i++)
            {
                DateTime picked = rg.Pick(list2);
                Console.WriteLine(picked);
                Assert.IsTrue(list2.Contains(picked));
            }
        }

        [TestMethod]
        public void PickParamsTest()
        {
            List<int> primes = new List<int> { 2, 3, 5, 7, 13, 17, 73 };
            SomeClass s;
            SomeFaker faker = new SomeFaker();
            for (int i = 0; i < 50; i++)
            {
                s = faker.Generate();
                Console.WriteLine(s.somePrime);
                Assert.IsTrue(primes.Contains(s.somePrime));
            }


            RandomGenerator rg = new RandomGenerator();
            List<int> items = new List<int> { 55, 43, 43, 74, 121, 13, 17, 666, 1000001010, 8, 4 };
            for (int i = 0; i < 100; i++)
            {
                int picked = rg.Pick(55, 43, 43, 74, 121, 13, 17, 666, 1000001010, 8, 4);
                Console.WriteLine(picked);
                Assert.IsTrue(items.Contains(picked));
            }

            List<char> items2 = new List<char> { 'a', 'b', 'r', 'a', 'k', 'a', 'd', 'a', 'b', 'r', 'a'};
            for (int i = 0; i < 100; i++)
            {
                char picked = rg.Pick('a', 'b', 'r', 'a', 'k', 'a', 'd', 'a', 'b', 'r', 'a');
                Console.WriteLine(picked);
                Assert.IsTrue(items2.Contains(picked));
            }
        }

        [TestMethod]
        public void PickIListExtensionsTest()
        {
            RandomGenerator rg = new RandomGenerator();
            List<Guid> guids = (List<Guid>)rg.List.Guid(50);
            for (int i = 0; i < 100; i++)
            {
                Guid picked = guids.PickRandom();
                Console.WriteLine(picked);
                Assert.IsTrue(guids.Contains(picked));
            }

            List<float> floats = (List<float>)rg.List.Float(50);
            for (int i = 0; i < 100; i++)
            {
                float picked = floats.PickRandom(rg);
                Console.WriteLine(picked);
                Assert.IsTrue(floats.Contains(picked));
            }
        }
        [TestMethod]
        public void PickIcollectionTest()
        {
            RandomGenerator rg = new RandomGenerator();
            int lower = 0;
            int upper = 100;
            int count = 50;
            ICollection<int> col = rg.List.Int(count, lower, upper);
            for (int i = 0; i < 1000; i++)
            {
                int picked = rg.Pick(col);
                Console.WriteLine(picked);
                Assert.IsTrue(col.Contains(picked));
            }

            DateTime lower2 = new DateTime(2010, 1, 1);
            DateTime upper2 = new DateTime(2020, 3, 3);
            ICollection<DateTime> col2 = rg.List.DateTime(count, lower2, upper2);
            for (int i = 0; i < 1000; i++)
            {
                DateTime picked = rg.Pick(col2);
                Console.WriteLine(picked);
                Assert.IsTrue(col2.Contains(picked));
            }
        }
        [TestMethod]
        public void PickICollectionExtensionTest()
        {
            RandomGenerator rg = new RandomGenerator();
            ICollection<Guid> guids = rg.List.Guid(50);
            for (int i = 0; i < 100; i++)
            {
                Guid picked = guids.PickRandom();
                Console.WriteLine(picked);
                Assert.IsTrue(guids.Contains(picked));
            }

            ICollection<float> floats = rg.List.Float(50);
            for (int i = 0; i < 100; i++)
            {
                float picked = floats.PickRandom(rg);
                Console.WriteLine(picked);
                Assert.IsTrue(floats.Contains(picked));
            }
        }


        [TestMethod]
        public void DefaultFuncDictTest()
        {
            RandomGenerator r = new RandomGenerator();
            var f = r.GetDefaultRandomFuncForType(typeof(ValueClass));
            Assert.IsNull(f);
            Console.WriteLine("int");
            var v = r.GetDefaultRandomFuncForType(typeof(int).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                int s = (int)v();
                Console.WriteLine(s);
            }
            Console.WriteLine("byte");
            var v2 = r.GetDefaultRandomFuncForType(typeof(byte).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                byte s = (byte)v2();
                Console.WriteLine(s);
            }
            Console.WriteLine("sbyte");
            var v3 = r.GetDefaultRandomFuncForType(typeof(sbyte).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                sbyte s = (sbyte)v3();
                Console.WriteLine(s);
            }
            Console.WriteLine("short");
            var v4 = r.GetDefaultRandomFuncForType(typeof(short).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                short s = (short)v4();
                Console.WriteLine(s);
            }
            Console.WriteLine("ushort");
            var v5 = r.GetDefaultRandomFuncForType(typeof(ushort).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                ushort s = (ushort)v5();
                Console.WriteLine(s);
            }
            Console.WriteLine("uint");
            var v6 = r.GetDefaultRandomFuncForType(typeof(uint).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                uint s = (uint)v6();
                Console.WriteLine(s);
            }
            Console.WriteLine("long");
            var v7 = r.GetDefaultRandomFuncForType(typeof(long).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                long s = (long)v7();
                Console.WriteLine(s);
            }
            Console.WriteLine("ulong");
            var v8 = r.GetDefaultRandomFuncForType(typeof(ulong).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                ulong s = (ulong)v8();
                Console.WriteLine(s);
            }
            Console.WriteLine("float");
            var v9 = r.GetDefaultRandomFuncForType(typeof(float).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                float s = (float)v9();
                Console.WriteLine(s);
            }
            Console.WriteLine("double");
            var v10 = r.GetDefaultRandomFuncForType(typeof(double).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                double s = (double)v10();
                Console.WriteLine(s);
            }
            Console.WriteLine("decimal");
            var v11 = r.GetDefaultRandomFuncForType(typeof(decimal).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                decimal s = (decimal)v11();
                Console.WriteLine(s);
            }
            Console.WriteLine("bool");
            var v12 = r.GetDefaultRandomFuncForType(typeof(bool).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                bool s = (bool)v12();
                Console.WriteLine(s);
            }
            Console.WriteLine("char");
            var v13 = r.GetDefaultRandomFuncForType(typeof(char).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                char s = (char)v13();
                Console.WriteLine(s);
            }
            Console.WriteLine("string");
            var v14 = r.GetDefaultRandomFuncForType(typeof(string).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                string s = (string)v14();
                Console.WriteLine(s);
            }
            Console.WriteLine("DateTime");
            var v15 = r.GetDefaultRandomFuncForType(typeof(DateTime).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                DateTime s = (DateTime)v15();
                Console.WriteLine(s);
            }
            Console.WriteLine("Guid");
            var v16 = r.GetDefaultRandomFuncForType(typeof(Guid).GetSampleInstance());
            for (int i = 0; i < 30; i++)
            {
                Guid s = (Guid)v16();
                Console.WriteLine(s);
            }
        }

    }   
}
