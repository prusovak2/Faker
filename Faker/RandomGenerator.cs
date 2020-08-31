using System;
using System.Text;

namespace Faker
{
    public partial class RandomGenerator
    {
        internal IRandomGeneratorAlg RandomGeneratorAlg { get; set; }
        public RandomChar Char { get; }
        public RandomString String { get; }
        public RandomBasicTypes Random { get; }

        public RandomGenerator()
        {
            this.RandomGeneratorAlg = new Xoshiro256starstar();
            this.Char = new RandomChar(this);
            this.String = new RandomString(this);
            this.Random = new RandomBasicTypes(this);
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

        public partial class RandomBasicTypes
        {
            /// <summary>
            /// reference to instance of Random generator that has reference to this instance of RandomString
            /// </summary>
            internal RandomGenerator RG { get; }
            public RandomBasicTypes(RandomGenerator rg)
            {
                this.RG = rg;
            }
            /// <summary>
            /// generates a random boolean
            /// </summary>
            /// <returns></returns>
            public bool Bool()
            {
                double random = this.RandomZeroToOneDouble();
                if (random < 0.5)
                {
                    return true;
                }
                return false;
            }

            /// <summary>
            /// returns random Guid
            /// </summary>
            /// <returns></returns>
            public Guid Guid()
            {
                //get 159 random bits of satisfying quality
                ulong upper = this.RG.RandomGeneratorAlg.Next();
                ulong middle = this.RG.RandomGeneratorAlg.Next();
                ulong lower = this.RG.RandomGeneratorAlg.Next();
                upper >>= 24; //keep upper 40 bits
                middle >>= 24; //keep upper 40 bits
                lower >>= 16; //keep upper 48 bits
                byte[] upperBytes = BitConverter.GetBytes(upper);
                byte[] middleBytes = BitConverter.GetBytes(middle);
                byte[] lowerBytes = BitConverter.GetBytes(lower);

                //extract bytes with nonzero value
                byte[] bytesForGuid = new byte[16];
                if (BitConverter.IsLittleEndian)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        bytesForGuid[i] = upperBytes[i];
                        bytesForGuid[i + 5] = middleBytes[i];
                        bytesForGuid[i + 10] = lowerBytes[i];
                    }
                    bytesForGuid[15] = lowerBytes[5];
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        bytesForGuid[i] = upperBytes[7 - i];
                        bytesForGuid[i + 5] = middleBytes[7 - i];
                        bytesForGuid[i + 10] = lowerBytes[7 - i];
                    }
                    bytesForGuid[15] = lowerBytes[7 - 5];
                }
                Guid randomGuid = new Guid(bytesForGuid);
                return randomGuid;
            }
            /// <summary>
            /// returns random DateTime from [lower,upper] interval
            /// </summary>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public DateTime DateTime(DateTime lower, DateTime upper)
            {
                long randomLong = this.Long(lower.Ticks, upper.Ticks);
                DateTime randomDateTime = new DateTime(randomLong);
                return randomDateTime;
            }
            /// <summary>
            /// returns random DateTime from [DateTime.MinValue,DateTime.Maxvalue] interval
            /// </summary>
            /// <returns></returns>
            public DateTime DateTime()
            {
                long randomLong = this.Long(System.DateTime.MinValue.Ticks, System.DateTime.MaxValue.Ticks);
                DateTime randomDateTime = new DateTime(randomLong);
                return randomDateTime;
            }
            /// <summary>
            /// returns random DateTime from interval [DateTime.MinValue,border] when upper is true<br/>
            /// or [border, DateTime.MaxValue] when upper is false
            /// </summary>
            /// <param name="border"></param>
            /// <param name="upper">is border an upper border</param>
            /// <returns></returns>
            public DateTime DateTime(DateTime border, bool upper)
            {
                if (upper)
                {
                    long randomLong = this.Long(System.DateTime.MinValue.Ticks, border.Ticks);
                    DateTime randomDateTime = new DateTime(randomLong);
                    return randomDateTime;
                }
                else
                {
                    long randomLong = this.Long(border.Ticks, System.DateTime.MaxValue.Ticks);
                    DateTime randomDateTime = new DateTime(randomLong);
                    return randomDateTime;
                }
            }
        }


    }

}
