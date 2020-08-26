using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public partial class RandomGenerator
    {
        private IRandomGeneratorAlg RandomGeneratorAlg { get; set; }
        public RandomGenerator()
        {
            this.RandomGeneratorAlg = new Xoshiro256starstar();
        }
        public RandomGenerator(ulong seed)
        {
            this.RandomGeneratorAlg = new Xoshiro256starstar(seed);
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
                    return () => this.RandomByte();
                case sbyte sb:
                    return () => this.RandomSbyte();
                case short s:
                    return () => this.RandomShort();
                case ushort us:
                    return () => this.RandomUshort();
                case int i:
                    return () => this.RandomInt();
                case uint ui:
                    return () => this.RandomUint();
                case long l:
                    return () => this.RandomLong();
                case ulong ul:
                    return () => this.RandomUlong();
                case float f:
                    return () => this.RandomFloat();
                case double d:
                    return () => this.RandomDouble();
                case decimal d:
                    return () => this.RandomDecimal();
                case char c:
                    return () => this.RandomChar();
                case bool b:
                    return () => this.RandomBool();
                case string s:
                    return () => this.RandomString();
                case DateTime dt:
                    return () => this.RandomDateTime();
                case Guid g:
                    return () => this.RandomGuid();
                default:
                    return null;
                 
            }
        }

        /// <summary>
        /// generates a random boolean
        /// </summary>
        /// <returns></returns>
        public bool RandomBool()
        {
            double random = this.RandomZeroToOneDouble();
            if (random < 0.5)
            {
                return true;
            }
            return false;
        }
        public string RandomString()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// returns random Guid
        /// </summary>
        /// <returns></returns>
        public Guid RandomGuid()
        {
            //get 159 random bits of satisfying quality
            ulong upper = this.RandomGeneratorAlg.Next();
            ulong middle = this.RandomGeneratorAlg.Next();
            ulong lower = this.RandomGeneratorAlg.Next();
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
        public DateTime RandomDateTime()
        {
            throw new NotImplementedException();
        }
        public char RandomChar()
        {
            throw new NotImplementedException();
        }
    }
}
