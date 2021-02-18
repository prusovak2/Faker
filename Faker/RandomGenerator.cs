using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Faker
{
    public partial class RandomGenerator
    {
        /// <summary>
        /// underlying algorithm for generating random bit sequences that all other random entities are derived from
        /// </summary>
        internal IRandomGeneratorAlg RandomGeneratorAlg { get; set; }

        public CultureInfo Culture { get; } = new CultureInfo("en-US");

        /// <summary>
        /// random letter, lower/upper case letter, digit, alphanumeric char, ASCII char...
        /// </summary>
        public RandomChar Char { get; private set; }
        /// <summary>
        /// random lower/upper case string, sequence of letters, alphanumeric string, string representation of hexadecimal number
        /// </summary>
        public RandomString String { get; private set; }
    
        /// <summary>
        /// all C# basic types (int, double, decimal, sbyte...) DateTime, Guid, Bool, odd and even numbers...
        /// </summary>
        public RandomBasicTypes Random { get; private set; }

        /// <summary>
        /// enumerables of basic types
        /// </summary>
        public RandomEnumerable Enumerable { get; private set; }
        /// <summary>
        /// lists of basic types
        /// </summary>
        public RandomList List { get; private set; }

        internal partial void RandomGeneratorInitializationGenerated();

        internal void RandomGeneratorInitialization()
        {
            this.Char = new RandomChar(this);
            this.String = new RandomString(this);
            this.Random = new RandomBasicTypes(this);
            this.Enumerable = new RandomEnumerable(this);
            this.List = new RandomList(this);
        }

        public RandomGenerator()
        {
            this.RandomGeneratorAlg = new Xoshiro256starstar();
            this.RandomGeneratorInitialization();
            this.RandomGeneratorInitializationGenerated();
        }
        public RandomGenerator(ulong seed)
        {
            this.RandomGeneratorAlg = new Xoshiro256starstar(seed);
            this.RandomGeneratorInitialization();
            this.RandomGeneratorInitializationGenerated();
        }
        public ulong Seed => this.RandomGeneratorAlg.Seed;
        /// <summary>
        /// returns delegate to default function of this instance of RandomGenerator to provide values of given type <br/>
        /// returns NULL when there is no default random function for given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Func<object> GetDefaultRandomFuncForType(Type type)
        {
            var sampleInstance = type.GetSampleInstance();
            var defaultFunc = this.GetDefaultRandomFuncForType(sampleInstance);
            return defaultFunc;
        }
        /// <summary>
        /// returns delegate to default function of this instance of RandomGenerator to provide values of type of given object <br/>
        /// returns NULL when there is no default random function for given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Func<object> GetDefaultRandomFuncForType(object o)
        {
            //only way to switch by a type is switch by a type of an instance, one cannot switch by the value of variable of Type type
            //thats why Type.GetSampleInstance is used to provide input values for this function - to avoid using reflexion to create an instance -  slow
            {
                switch (o)
                {
                    case byte b:
                        return () => this.Random.Byte();
                    case sbyte sb:
                        return () => this.Random.Sbyte();
                    case short s:
                        return () => this.Random.Short();
                    case ushort us:
                        return () => this.Random.Ushort();
                    case int i:
                        return () => this.Random.Int();
                    case uint ui:
                        return () => this.Random.Uint();
                    case long l:
                        return () => this.Random.Long();
                    case ulong ul:
                        return () => this.Random.Ulong();
                    case float f:
                        return () => this.Random.Float();
                    case double d:
                        return () => this.Random.Double();
                    case decimal d:
                        return () => this.Random.Decimal();
                    case char c:
                        return () => this.Char.Char();
                    case bool b:
                        return () => this.Random.Bool();
                    case string s:
                        return () => this.String.String();
                    case DateTime dt:
                        return () => this.Random.DateTime();
                    case Guid g:
                        return () => this.Random.Guid();
                    default:
                        return null;
                }
            }
        }
        /// <summary>
        /// Randomly picks one item from the IList
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="pickFrom"></param>
        /// <returns></returns>
        public Titem Pick<Titem>(IList<Titem> pickFrom)
        {
            int randomIndex = this.Random.Int(0, pickFrom.Count-1);
            return pickFrom[randomIndex];
        }
        /// <summary>
        /// Randomly picks one of items passed as arguments
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="pickFrom"></param>
        /// <returns></returns>
        public Titem Pick<Titem>(params Titem[] pickFrom)
        {
            return Pick((IList<Titem>)pickFrom);
        }
        /// <summary>
        /// Randomly picks item from the ICollection <br/>
        /// LINEAR TIME COMPLEXITY!
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="pickFrom"></param>
        /// <returns></returns>
        public Titem Pick<Titem>(ICollection<Titem> pickFrom)
        {
            int randomIndex = this.Random.Int(0, pickFrom.Count - 1);
            int counter = 0;
            foreach (Titem item in pickFrom)
            {
                if (counter == randomIndex)
                {
                    return item;
                }
                counter++;
            }
            throw new NotImplementedException("Unexpected");
        }
    }
}
