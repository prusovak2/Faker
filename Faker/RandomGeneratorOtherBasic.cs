using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public partial class RandomGenerator
    {
        
        public partial class RandomBasicTypes
        {
            //the rest of the implementation of this class is to be found in files RandomGeneratorInteger, RandomGeneratorNullable and RandomGeneratotFloat

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
            /// <summary>
            ///generates a random char from interval[lower, upper] <br/>
            /// when lower/upper bound is not specified, char.MinValue/char.MaxValue is used 
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public char Char(char lower = char.MinValue, char upper = char.MaxValue)
            {
                //just to provide random char among basic types as well as among chars
                //just call RandomChar.Char()
                char randomChar = this.RG.Char.Char(lower, upper);
                return randomChar;
            }
            /// <summary>
            /// generates a random string 255 characters long
            /// </summary>
            /// <returns></returns>
            public string String()
            {
                //just to provide random string among basic types as well as among strings
                //just call RandomString.String()
                return this.RG.String.String();
            }
            /// <summary>
            /// generates a random string, when precise is true, length is used as length of string<br/>
            /// otherwise a random int less or equal to length is generated and used as length of string
            /// </summary>
            /// <param name="length"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public string String(int length, bool precise = true)
            {
                //just to provide random string among basic types as well as among strings
                //just call RandomString.String()
                return this.RG.String.String(length, precise);
            }
        }
    }
}
