using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System.Xml.Serialization;

namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorTests
    {
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
