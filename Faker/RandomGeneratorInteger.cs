using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Faker
{
    public partial class RandomGenerator
    {
        /// <summary>
        /// generates a random integer from interval [lower,upper] <br/>
        /// when lower/upper bound is not specified, int.MinValue/int.MaxValue is used 
        /// </summary>
        public int RandomInt(int lower = int.MinValue, int upper=int.MaxValue)
        {
            double randomDouble = this.RandomDouble(lower, upper);
            int randomInt =(int) Math.Round(randomDouble);
            return randomInt;
        }
        /// <summary>
        /// generates a random unsigned integer from interval [lower,upper] <br/>
        /// when lower/upper bound is not specified, uint.MinValue/uint.MaxValue is used 
        /// </summary>
        public uint RandomUint(uint lower = uint.MinValue, uint upper = uint.MaxValue)
        {
            double randomDouble = this.RandomDouble(lower, upper);
            uint randomUint = (uint)Math.Round(randomDouble);
            return randomUint;
        }
        /// <summary>
        /// generates a random short from interval [lower,upper] <br/>
        /// when lower/upper bound is not specified, short.MinValue/short.MaxValue is used 
        /// </summary>
        public short RandomShort(short lower = short.MinValue, short upper = short.MaxValue)
        {
            int randomInt = this.RandomInt(lower, upper);
            return (short)randomInt;
        }
        /// <summary>
        /// generates a random unsigned short from interval [lower,upper] <br/>
        /// when lower/upper bound is not specified, ushort.MinValue/ushort.MaxValue is used 
        /// </summary>
        public ushort RandomUshort(ushort lower = ushort.MinValue, ushort upper = ushort.MaxValue)
        {
            uint randomUint = this.RandomUint(lower, upper);
            return (ushort)randomUint;
        }
        /// <summary>
        /// generates a random signed byte from interval [lower,upper] <br/>
        /// when lower/upper bound is not specified, sbyte.MinValue/sbyte.MaxValue is used 
        /// </summary>
        public sbyte RandomSbyte(sbyte lower = sbyte.MinValue, sbyte upper=sbyte.MaxValue)
        {
            int randomInt = this.RandomInt(lower, upper);
            return (sbyte)randomInt;
        }
        /// <summary>
        /// generates a random byte from interval [lower,upper] <br/>
        /// when lower/upper bound is not specified, byte.MinValue/byte.MaxValue is used 
        /// </summary>
        public byte RandomByte(byte lower = byte.MinValue, byte upper = byte.MaxValue)
        {
            uint randomUint = this.RandomUint(lower, upper);
            return (byte)randomUint;
        }
        /// <summary>
        /// generates a random long from interval [lower,upper] <br/>
        /// when lower/upper bound is not specified, long.MinValue/long.MaxValue is used 
        /// </summary>
        public long RandomLong(long lower = long.MinValue, long upper = long.MaxValue)
        {
            double randomDouble = this.RandomDouble(lower, upper);
            long randomLong = (long)Math.Round(randomDouble);
            return randomLong;
        }
        /// <summary>
        /// generates a random unsigned long from interval [lower,upper] <br/>
        /// when lower/upper bound is not specified, ulong.MinValue/ulong.MaxValue is used 
        /// </summary>
        public ulong RandomUlong(ulong lower = ulong.MinValue, ulong upper = ulong.MaxValue)
        {
            double randomDouble = this.RandomDouble(lower, upper);
            ulong randomUlong = (ulong)Math.Round(randomDouble);
            return randomUlong;
        }
        public int RandomEvenInt(int lower = int.MinValue, int upper = int.MaxValue)
        {
            int halfRangeInt = this.RandomInt(lower / 2, upper / 2);
            return 2 * halfRangeInt;
        }
        public int RandomOddInt(int lower = int.MinValue, int upper = int.MaxValue)
        {
            if (lower % 2 == 0)
            {
                lower++;
            }
            if (upper % 2 == 0)
            {
                upper--;
            }
            int halfRangeInt = this.RandomInt(lower / 2, upper / 2);
            return (2 * halfRangeInt) + 1;
        }
    }
}
