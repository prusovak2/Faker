using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;

namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorNullableTests
    {
       
        [TestMethod]
        public void AlwaysNullNeverNull()
        {
            RandomGenerator r = new RandomGenerator();
            //always null
            for (int i = 0; i < 10000; i++)
            {
                int? ni = r.Random.NullableGeneric<int>(1f);
                Assert.IsNull(ni);
            }
            //never null
            for (int i = 0; i < 10000; i++)
            {
                int? ni = r.Random.NullableGeneric<int>(0f);
                Assert.IsNotNull(ni);
            }
        }

        [TestMethod]
        public void InvalidProbability()
        {
            RandomGenerator r = new RandomGenerator();
            Assert.ThrowsException<ArgumentException>(() => { bool? nb = r.Random.NullableGeneric<bool>(42); });
            Assert.ThrowsException<ArgumentException>(() => { bool? nb = r.Random.NullableGeneric<bool>(-73); });
        }

        [TestMethod]
        public void NullableByteTest()
        {
            RandomGenerator r = new RandomGenerator();
            byte? x;
            byte lower = 42;
            byte upper = 73;
            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableByte(5/10f, lower, upper);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= lower && x <= upper) || (x is null));
                if(x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Assert.IsFalse(nullCounter == 0);
            nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableByte(5 / 10f);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }

        [TestMethod]
        public void NullableSbyteTest()
        {
            RandomGenerator r = new RandomGenerator();
            sbyte? x;
            sbyte lower = -42;
            sbyte upper = 73;
            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableSbyte(5 / 10f, lower, upper);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= lower && x <= upper) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Assert.IsFalse(nullCounter == 0);
            nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableSbyte(5 / 10f);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }
        [TestMethod]
        public void NullableShortTest()
        {
            RandomGenerator r = new RandomGenerator();
            short? x;
            short lower = -42;
            short upper = 73;
            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableShort(5/ 10f, lower, upper);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= lower && x <= upper) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Assert.IsFalse(nullCounter == 0);
            nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableShort(5 / 10f);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }
        [TestMethod]
        public void NullableUshortTest()
        {
            RandomGenerator r = new RandomGenerator();
            ushort? x;
            ushort lower = 42;
            ushort upper = 73;
            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableUshort(5 / 10f, lower, upper);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= lower && x <= upper) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Assert.IsFalse(nullCounter == 0);
            nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableUshort(5 / 10f);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }
        [TestMethod]
        public void NullableIntTest()
        {
            RandomGenerator r = new RandomGenerator();
            int? x;
            int lower = -4200;
            int upper = 73000;
            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableInt(5 / 10f, lower, upper);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= lower && x <= upper) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Assert.IsFalse(nullCounter == 0);
            nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableInt(5 / 10f);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }
        [TestMethod]
        public void NullableUintTest()
        {
            RandomGenerator r = new RandomGenerator();
            uint? x;
            uint lower = 42;
            uint upper = 73000;
            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableUint(5 / 10f, lower, upper);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= lower && x <= upper) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Assert.IsFalse(nullCounter == 0);
            nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableUint(5 / 10f);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }
        [TestMethod]
        public void NullableLongTest()
        {
            RandomGenerator r = new RandomGenerator();
            long? x;
            long lower = -4200;
            long upper = 73000;
            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableLong(5 / 10f, lower, upper);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= lower && x <= upper) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Assert.IsFalse(nullCounter == 0);
            nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableLong(5 / 10f);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }
        [TestMethod]
        public void NullableUlongTest()
        {
            RandomGenerator r = new RandomGenerator();
            ulong? x;
            ulong lower = 42;
            ulong upper = 730000;
            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableUlong(5 / 10f, lower, upper);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= lower && x <= upper) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Assert.IsFalse(nullCounter == 0);
            nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableUlong(5 / 10f);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }

        [TestMethod]
        public void NullableCharTest()
        {
            RandomGenerator r = new RandomGenerator();
            char? x;
            char lower = 'a';
            char upper = 'z';
            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableChar(5 / 10f, lower, upper);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= lower && x <= upper) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Assert.IsFalse(nullCounter == 0);
            nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableChar(5 / 10f);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }
        [TestMethod]
        public void NullableFloatTest()
        {
            RandomGenerator r = new RandomGenerator();
            float? x;
            float lower = 42;
            float upper = 730000;
            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableFloat(5 / 10f, lower, upper);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= lower && x <= upper) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Assert.IsFalse(nullCounter == 0);
            nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableFloat(5 / 10f);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= 0 && x <= 1) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }
        [TestMethod]
        public void NullableDoubleTest()
        {
            RandomGenerator r = new RandomGenerator();
            double? x;
            double lower = 42;
            double upper = 730000;
            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableDouble(5 / 10f, lower, upper);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= lower && x <= upper) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Assert.IsFalse(nullCounter == 0);
            nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableDouble(5 / 10f);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= 0 && x <= 1) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }
        [TestMethod]
        public void NullableDecimalTest()
        {
            RandomGenerator r = new RandomGenerator();
            decimal? x;
            decimal lower = 42;
            decimal upper = 730000;
            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableDecimal(5 / 10f, lower, upper);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= lower && x <= upper) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Assert.IsFalse(nullCounter == 0);
            nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableDecimal(5 / 10f);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= 0 && x <= 1) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }
        [TestMethod]
        public void NullableDatetimeTest()
        {
            RandomGenerator r = new RandomGenerator();
            DateTime? x;
            DateTime lower = new DateTime(1000, 1, 1);
            DateTime upper = new DateTime(6000, 3, 3);
            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableDateTime(5 / 10f, lower, upper);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                Assert.IsTrue((x >= lower && x <= upper) || (x is null));
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Assert.IsFalse(nullCounter == 0);
            nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableDateTime(5 / 10f);
                if (x is object)
                {
                    Console.WriteLine(x);
                }
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }
        [TestMethod]
        public void NullableGuidTest()
        {
            RandomGenerator r = new RandomGenerator();
            Guid? x;
            
            int nullCounter = 0;
            HashSet<Guid?> areUnique = new HashSet<Guid?>();
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableGuid(5/10f);
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
                else
                {
                    Console.WriteLine(x);
                }
                areUnique.Add(x);
            }
            Assert.IsFalse(nullCounter == 0);
        }
        [TestMethod]
        public void NullableBoolTest()
        {
            RandomGenerator r = new RandomGenerator();
            bool? x;

            int nullCounter = 0;
            for (int i = 0; i < 500; i++)
            {
                x = r.Random.NullableBool(5 / 10f);
                if (x is null)
                {
                    nullCounter++;
                    Console.WriteLine("null");
                }
                else
                {
                    Console.WriteLine(x);
                }
            }
            Assert.IsFalse(nullCounter == 0);
        }
    }
}
