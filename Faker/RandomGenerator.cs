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

        public Func<object> GetDefaultRandomFuncForType(object o)
        {
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
        public Guid RandomGuid()
        {
            throw new NotImplementedException();
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
