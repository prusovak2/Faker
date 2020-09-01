using System;
using System.Text;

namespace Faker
{
    public partial class RandomGenerator
    {
        /// <summary>
        /// underlying algorithm for generating random bit sequences that all other random entities are derived from
        /// </summary>
        internal IRandomGeneratorAlg RandomGeneratorAlg { get; set; }
        /// <summary>
        /// random letter, lower/upper case letter, digit, alphanumeric char, ASCII char...
        /// </summary>
        public RandomChar Char { get; }
        /// <summary>
        /// random lower/upper case string, sequence of letters, alphanumeric string, string representation of hexadecimal number
        /// </summary>
        public RandomString String { get; }
        /// <summary>
        /// all C# basic types (int, double, decimal, sbyte...) DateTime, Guid, Bool, odd and even numbers...
        /// </summary>
        public RandomBasicTypes Random { get; }
        /// <summary>
        /// enumerables of basic types
        /// </summary>
        public RandomEnumerable Enumerable { get; }

        public RandomGenerator()
        {
            this.RandomGeneratorAlg = new Xoshiro256starstar();
            this.Char = new RandomChar(this);
            this.String = new RandomString(this);
            this.Random = new RandomBasicTypes(this);
            this.Enumerable = new RandomEnumerable(this);
        }
        public RandomGenerator(ulong seed)
        {
            this.RandomGeneratorAlg = new Xoshiro256starstar(seed);
            this.Char = new RandomChar(this);
            this.String = new RandomString(this);
            this.Random = new RandomBasicTypes(this);
        }
        public ulong Seed => this.RandomGeneratorAlg.Seed;
        /// <summary>
        /// returns delegate to default function of this instance of RandomGenerator to provide values of given type
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
        /// returns delegate to default function of this instance of RandomGenerator to provide values of type of given object
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Func<object> GetDefaultRandomFuncForType(object o)
        {
            //only way to switch by a type is switch by a type of an instance, one cannot switch by the value of variable of Type type
            //thats way Type.GetSampleInstance is used to provide input values for this function - to avoid using reflexion to create an instance -  slow
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
}
