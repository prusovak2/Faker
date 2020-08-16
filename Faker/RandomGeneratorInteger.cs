using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    /// <summary>
    /// generates a random integer from interval [lower,upper]
    /// </summary>
    public partial class RandomGenerator
    {
        public int RandomInt(int lower, int upper)
        {
            double randomDouble = this.RandomDouble(lower, upper);
            int randomInt =(int) Math.Round(randomDouble);
            return randomInt;
        }
        public int RandomInt()
        {
            double randomDouble = this.RandomDouble(int.MinValue, int.MaxValue);
            int randomInt = (int)Math.Round(randomDouble);
            return randomInt;
        }
        public uint RandomUint(uint lower, uint upper)
        {
            double randomDouble = this.RandomDouble(lower, upper);
            uint randomUint = (uint)Math.Round(randomDouble);
            return randomUint;
        }
        public uint RandomUint()
        {
            double randomDouble = this.RandomDouble(uint.MinValue, uint.MaxValue);
            uint randomUint = (uint)Math.Round(randomDouble);
            return randomUint;
        }
        public short RandomShort(short lower, short upper)
        {
            int randomInt = this.RandomInt(lower, upper);
            return (short)randomInt;
        }
        public short RandomShort()
        {
            int randomInt = this.RandomInt(short.MinValue, short.MaxValue);
            return (short)randomInt;
        }
        public ushort RandomUshort(ushort lower, ushort upper)
        {
            uint randomUint = this.RandomUint(lower, upper);
            return (ushort)randomUint;
        }
        public ushort RandomUshort()
        {
            uint randomUint = this.RandomUint(ushort.MinValue, ushort.MaxValue);
            return (ushort)randomUint;
        }
        public sbyte RandomSbyte(sbyte lower, sbyte upper)
        {
            int randomInt = this.RandomInt(lower, upper);
            return (sbyte)randomInt;
        }
        public sbyte RandomSbyte()
        {
            int randomInt = this.RandomInt(sbyte.MinValue, sbyte.MaxValue);
            return (sbyte)randomInt;
        }
        public byte RandomByte(byte lower, byte upper)
        {
            uint randomUint = this.RandomUint(lower, upper);
            return (byte)randomUint;
        }
        public byte RandomByte()
        {
            uint randomUint = this.RandomUint(byte.MinValue, byte.MaxValue);
            return (byte)randomUint;
        }
        public long RandomLong(long lower, long upper)
        {
            double randomDouble = this.RandomDouble(lower, upper);
            long randomLong = (long)Math.Round(randomDouble);
            return randomLong;
        }
        public long RandomLong()
        {
            double randomDouble = this.RandomDouble(long.MinValue, long.MaxValue);
            long randomLong = (long)Math.Round(randomDouble);
            return randomLong;
        }
        public ulong RandomUlong(ulong lower, ulong upper)
        {
            double randomDouble = this.RandomDouble(lower, upper);
            ulong randomUlong = (ulong)Math.Round(randomDouble);
            return randomUlong;
        }
        public ulong RandomUlong()
        {
            double randomDouble = this.RandomDouble(ulong.MinValue, ulong.MaxValue);
            ulong randomUlong = (ulong)Math.Round(randomDouble);
            return randomUlong;
        }
    }
}
