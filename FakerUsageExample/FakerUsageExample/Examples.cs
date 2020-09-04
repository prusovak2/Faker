using Faker;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakerUsageExample
{
    public static class Examples
    {
        public static void BasicPersonFakerExample()
        {
            Console.WriteLine("BasicPersonFakerExample");
            BasicPersonFaker basicPersonFaker = new BasicPersonFaker();
            Console.WriteLine("Generate:");
            for (int i = 0; i < 5; i++)
            {
                //generate
                Person p = basicPersonFaker.Generate();
                Console.WriteLine(p);
            }
            Console.WriteLine();

            //populate
            Console.WriteLine("Populate");
            Person AnotherPerson = new Person();
            basicPersonFaker.Populate(AnotherPerson);
            Console.WriteLine(AnotherPerson);
            Console.WriteLine();
        }
        public static void SimplePersonFakerExample()
        {
            Console.WriteLine("SimplePersonFakerExample:");
            SimplePersonFaker simplePersonFaker = new SimplePersonFaker();
            for (int i = 0; i < 5; i++)
            {
                Person p = simplePersonFaker.Generate();
                Console.WriteLine(p);
            }
            Console.WriteLine();
        }
        public static void RandomGeneratorBasicExample()
        {
            Console.WriteLine("RandomGeneratorBasicExample");
            RandomGenerator rg = new RandomGenerator();
            Console.WriteLine("Random DateTime");
            DateTime date = rg.Random.DateTime(new DateTime(2000, 1, 1), new DateTime(2020, 1, 1));
            Console.WriteLine(date);
            Console.WriteLine("Enumerable of random sbytes");
            IEnumerable<sbyte> sbytes = rg.Enumerable.Sbyte(42);
            foreach (var item in sbytes)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Random decimal");
            decimal d = rg.Random.Decimal();
            Console.WriteLine(d);
            Console.WriteLine();
        }
        public static void StorageFakerExample()
        {
            Console.WriteLine("StorageFakerExample");
            StorageFaker storageFaker = new StorageFaker();
            for (int i = 0; i < 5; i++)
            {
                Storage s = storageFaker.Generate();
                Console.WriteLine(s);
            }
            Console.WriteLine();
        }
       /* public static void IntOverloadsExample()
        {
            Console.WriteLine("IntOverloadsExample");
            RandomGenerator rg = new RandomGenerator();
            Console.WriteLine("AllRange");
            int randomInt = rg.Random.Int();
            Console.WriteLine(randomInt);
            Console.WriteLine("Less than 42");
            randomInt = rg.Random.Int(upper: 42);
            Console.WriteLine(randomInt);
            Console.WriteLine("Greater than 42");
            randomInt = rg.Random.Int(lower: 42);
            Console.WriteLine(randomInt);
            Console.WriteLine("between 42 and 73");
            randomInt = rg.Random.Int(42, 73);
            Console.WriteLine(randomInt);
            Console.WriteLine("Odd between 42 and 73");
            randomInt = rg.Random.IntOdd(42, 73);
            Console.WriteLine(randomInt);
            Console.WriteLine();
        }*/
        public static void IntOverloadsExample()
        {
            Console.WriteLine("IntOverloadsExample");
            RandomGenerator rg = new RandomGenerator();
            Console.WriteLine("AllRange");
            for (int i = 0; i < 5; i++)
            {
                int randomInt = rg.Random.Int();
                Console.WriteLine(randomInt);
            }
            Console.WriteLine("Less than 42");
            for (int i = 0; i < 5; i++)
            {
                int randomInt = rg.Random.Int(upper: 42);
                Console.WriteLine(randomInt);
            }
            Console.WriteLine("Greater than 42");
            for (int i = 0; i < 5; i++)
            {
                int randomInt = rg.Random.Int(lower: 42);
                Console.WriteLine(randomInt);
            }
            Console.WriteLine("between 42 and 73");
            for (int i = 0; i < 5; i++)
            {
                int randomInt = rg.Random.Int(42, 73);
                Console.WriteLine(randomInt);
            }
            Console.WriteLine("Odd between 42 and 73");
            for (int i = 0; i < 5; i++)
            {
                int randomInt = rg.Random.IntOdd(42, 73);
                Console.WriteLine(randomInt);
            }
            Console.WriteLine();
        }
        public static void RandomEnumerableExample()
        {
            RandomGenerator random = new RandomGenerator();
            Console.WriteLine("Infinite Enumerable of bytes");
            IEnumerable<byte> bytes = random.Enumerable.Byte();
            int counter = 0;
            foreach (var item in bytes)
            {
                Console.WriteLine(item);
                counter++;
                if (counter >= 5)
                {
                    break;
                }
            }
            Console.WriteLine("Enumerable of 3 bytes from [0,42] interval");
            bytes = random.Enumerable.Byte(3, 0, 42);
            foreach (var item in bytes)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Enumerable of at most 10 bytes");
            bytes = random.Enumerable.Byte(10, precise: false);
            foreach (var item in bytes)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
        public static void StringExample()
        {
            RandomGenerator random = new RandomGenerator();
            string randomString = random.String.Letters();
            Console.WriteLine($"Letters: {randomString}");
            randomString = random.String.AlphaNumeric(20);
            Console.WriteLine($"Alphanumeric: {randomString}");
            randomString = random.String.HexadecimalString(1,100);
            Console.WriteLine($"Hexadecimal: 0x{randomString}");
        }
    }
}
