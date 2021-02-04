using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Faker
{
    public partial class RandomGenerator
    {
        public partial class RandomBasicTypes
        {
            /// <summary>
            /// generates a random integer from interval [lower,upper] <br/>
            /// when lower/upper bound is not specified, int.MinValue/int.MaxValue is used 
            /// </summary>
            public int Int(int lower = int.MinValue, int upper = int.MaxValue)
            {
                double randomDouble = this.Double(lower, upper);
                int randomInt = (int)Math.Round(randomDouble);
                return randomInt;
            }
            public int IntNullableParams(int? lower = int.MinValue, int? upper = int.MaxValue)
            {
                double randomDouble = this.Double(lower.Value, upper.Value);
                int randomInt = (int)Math.Round(randomDouble);
                return randomInt;
            }

            /// <summary>
            /// generates a random unsigned integer from interval [lower,upper] <br/>
            /// when lower/upper bound is not specified, uint.MinValue/uint.MaxValue is used 
            /// </summary>
            public uint Uint(uint lower = uint.MinValue, uint upper = uint.MaxValue)
            {
                double randomDouble = this.Double(lower, upper);
                uint randomUint = (uint)Math.Round(randomDouble);
                return randomUint;
            }
            /// <summary>
            /// generates a random short from interval [lower,upper] <br/>
            /// when lower/upper bound is not specified, short.MinValue/short.MaxValue is used 
            /// </summary>
            public short Short(short lower = short.MinValue, short upper = short.MaxValue)
            {
                int randomInt = this.Int(lower, upper);
                return (short)randomInt;
            }
            /// <summary>
            /// generates a random unsigned short from interval [lower,upper] <br/>
            /// when lower/upper bound is not specified, ushort.MinValue/ushort.MaxValue is used 
            /// </summary>
            public ushort Ushort(ushort lower = ushort.MinValue, ushort upper = ushort.MaxValue)
            {
                uint randomUint = this.Uint(lower, upper);
                return (ushort)randomUint;
            }
            /// <summary>
            /// generates a random signed byte from interval [lower,upper] <br/>
            /// when lower/upper bound is not specified, sbyte.MinValue/sbyte.MaxValue is used 
            /// </summary>
            public sbyte Sbyte(sbyte lower = sbyte.MinValue, sbyte upper = sbyte.MaxValue)
            {
                int randomInt = this.Int(lower, upper);
                return (sbyte)randomInt;
            }
            /// <summary>
            /// generates a random byte from interval [lower,upper] <br/>
            /// when lower/upper bound is not specified, byte.MinValue/byte.MaxValue is used 
            /// </summary>
            public byte Byte(byte lower = byte.MinValue, byte upper = byte.MaxValue)
            {
                uint randomUint = this.Uint(lower, upper);
                return (byte)randomUint;
            }
            /// <summary>
            /// generates a random long from interval [lower,upper] <br/>
            /// when lower/upper bound is not specified, long.MinValue/long.MaxValue is used 
            /// </summary>
            public long Long(long lower = long.MinValue, long upper = long.MaxValue)
            {
                double randomDouble = this.Double(lower, upper);
                long randomLong = (long)Math.Round(randomDouble);
                return randomLong;
            }

            public long LongNullableParams(long? lower = long.MinValue, long? upper = long.MaxValue)
            {
                double randomDouble = this.Double(lower.Value, upper.Value);
                long randomLong = (long)Math.Round(randomDouble);
                return randomLong;
            }

            /// <summary>
            /// generates a random unsigned long from interval [lower,upper] <br/>
            /// when lower/upper bound is not specified, ulong.MinValue/ulong.MaxValue is used 
            /// </summary>
            public ulong Ulong(ulong lower = ulong.MinValue, ulong upper = ulong.MaxValue)
            {
                double randomDouble = this.Double(lower, upper);
                ulong randomUlong = (ulong)Math.Round(randomDouble);
                return randomUlong;
            }
            public int IntEven(int lower = int.MinValue, int upper = int.MaxValue)
            {
                if ((lower == upper))
                {
                    if (lower % 2 == 0)
                    {
                        return lower;
                    }
                    throw new ArgumentException();
                }
                if (lower > upper)
                {
                    int tmp = lower;
                    lower = upper;
                    upper = tmp;
                }
                int halfLower = lower / 2;
                int halfUpper = upper / 2;
                if ((lower % 2 != 0) && (lower > 0))
                {
                    //lower=21    (21/2)*2 = 20 - result of int division gets rounded down - we would get number out of the interval
                    // we have to round up (add one)
                    halfLower++;
                }
                if ((upper % 2 != 0) && upper < 0)
                {
                    //upper=-21   (-21/2)*2=-21 - result of int division gets rounded up when its negative - we would get number out of the interval
                    //we have to round down (subtract one)
                    halfUpper--;
                }
                int halfRangeInt = this.Int(halfLower, halfUpper);
                //bijection between all number from half sized interval and even numbers from original interval
                return 2 * halfRangeInt;
            }
            public int IntOdd(int lower = int.MinValue, int upper = int.MaxValue)
            {
                if ((lower == upper))
                {
                    if (lower % 2 != 0)
                    {
                        return lower;
                    }
                    throw new ArgumentException("There is no odd number in the interval you specified.");
                }
                if (lower > upper)
                {
                    int tmp = lower;
                    lower = upper;
                    upper = tmp;
                }
                //both odd
                if (lower % 2 != 0 && upper % 2 != 0)
                {
                    //there is one more odd number than there is even numbers in the interval
                    //shift lower to add one more even number to interval to gain bijection between odd and even numbers
                    lower--;
                    int even = this.IntEven(lower, upper);
                    //map even number to closest higher odd number
                    return even + 1;
                }
                if (lower % 2 == 0 && upper % 2 == 0)
                {
                    //there is one less odd number than there is even numbers in the interval
                    //shift upper so that we get rid of one extra even number
                    upper--;
                    int even = this.IntEven(lower, upper);
                    //map even number to closest higher odd number
                    return even + 1;
                }
                if (lower % 2 == 0 && upper % 2 != 0)
                {
                    //same number of odd and even
                    int even = this.IntEven(lower, upper);
                    //interval starts with even - shift up (+1)
                    return even + 1;
                }
                if (lower % 2 != 0 && upper % 2 == 0)
                {
                    //same number of odd and even
                    int even = this.IntEven(lower, upper);
                    //interval ends with even - shift down (-1)
                    return even - 1;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
