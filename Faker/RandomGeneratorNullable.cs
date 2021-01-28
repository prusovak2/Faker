using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public partial class RandomGenerator
    {
        public partial class RandomBasicTypes
        {
            //the rest of the implementation of this class is to be found in files RandomGeneratorInteger, RandomGeneratorOtherBasic and RandomGeneratotFloat

            /// <summary>
            /// Generates random value of nullable version of Tval, probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval <br\>
            /// uses default random function for Tval type is such exists, throws ArgumentException otherwise <br\>
            /// </summary>
            /// <typeparam name="Tval"></typeparam>
            /// <param name="nullProbability">lies in [0,1], probability that null is generated</param>
            /// <returns></returns>
            internal Tval? NullableGeneric<Tval>(float nullProbability) where Tval : struct
            {
                if ((nullProbability > 1) || (nullProbability < 0))
                {
                    throw new ArgumentException("nullProbanility must belong to interval [0,1]");
                }
                float generateNull = RandomZeroToOneFloat();
                if (generateNull < nullProbability)
                {
                    return null;
                }
                Type type = typeof(Tval);
                Func<object> randomFunc = RG.GetDefaultRandomFuncForType(type);
                if(randomFunc is null)
                {
                    throw new ArgumentException("Tval type does not have the default random function specified for it");
                }
                return (Tval)randomFunc();
            }

            /// <summary>
            /// Generates random value of nullable version of Tval, probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval <br\>
            /// uses random function passed as arg <br\>
            /// </summary>
            /// <typeparam name="Tval"></typeparam>
            /// <param name="nullProbability">>lies in [0,1], probability that null is generated</param>
            /// <param name="randomFunc">random function used to get not nullable random value</param>
            /// <param name="lower">lower bound for random value if null is not generated</param>
            /// <param name="upper">upper bound for random value if null is not generated</param>
            /// <returns></returns>
            internal Tval? NullableGeneric<Tval>(float nullProbability, Func<Tval, Tval, Tval> randomFunc, Tval lower, Tval upper) where Tval : struct
            {
                if ((nullProbability > 1) || (nullProbability < 0))
                {
                    throw new ArgumentException("nullProbanility must belong to interval [0,1]");
                }
                float generateNull = RandomZeroToOneFloat();
                if (generateNull < nullProbability)
                {
                    return null;
                }
                return randomFunc(lower, upper);
            }

            /// <summary>
            /// generates a random nullable byte from interval [lower,upper] <br/>
            /// probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval <br/>
            /// when lower/upper bound is not specified, byte.MinValue/byte.MaxValue is used 
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public byte? NullableByte(float NullProbability, byte lower = byte.MinValue, byte upper = byte.MaxValue)
            {
                return NullableGeneric<Byte>(NullProbability, this.Byte, lower, upper);
            }

            /// <summary>
            /// generates a random nullable sbyte from interval [lower,upper] <br/>
            /// probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval <br/>
            /// when lower/upper bound is not specified, sbyte.MinValue/sbyte.MaxValue is used 
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public sbyte? NullableSbyte(float NullProbability, sbyte lower = sbyte.MinValue, sbyte upper = sbyte.MaxValue)
            {
                return NullableGeneric<sbyte>(NullProbability, this.Sbyte, lower, upper);
            }

            /// <summary>
            /// generates a random nullable short from interval [lower,upper] <br/>
            /// probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval <br/>
            /// when lower/upper bound is not specified, short.MinValue/short.MaxValue is used 
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public short? NullableShort(float NullProbability, short lower = short.MinValue, short upper = short.MaxValue)
            {
                return NullableGeneric<short>(NullProbability, this.Short, lower, upper);
            }

            /// <summary>
            /// generates a random nullable ushort from interval [lower,upper] <br/>
            /// probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval <br/>
            /// when lower/upper bound is not specified, ushort.MinValue/ushort.MaxValue is used 
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public ushort? NullableUshort(float NullProbability, ushort lower = ushort.MinValue, ushort upper = ushort.MaxValue)
            {
                return NullableGeneric<ushort>(NullProbability, this.Ushort, lower, upper);
            }

            /// <summary>
            /// generates a random nullable int from interval [lower,upper] <br/>
            /// probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval <br/>
            /// when lower/upper bound is not specified, int.MinValue/int.MaxValue is used 
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public int? NullableInt(float NullProbability, int lower = int.MinValue, int upper = int.MaxValue)
            {
                return NullableGeneric<int>(NullProbability, this.Int, lower, upper);
            }

            /// <summary>
            /// generates a random nullable uint from interval [lower,upper] <br/>
            /// probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval <br/>
            /// when lower/upper bound is not specified, uint.MinValue/uint.MaxValue is used 
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public uint? NullableUint(float NullProbability, uint lower = uint.MinValue, uint upper = uint.MaxValue)
            {
                return NullableGeneric<uint>(NullProbability, this.Uint, lower, upper);
            }
            /// <summary>
            /// generates a random nullable long from interval [lower,upper] <br/>
            /// probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval <br/>
            /// when lower/upper bound is not specified, long.MinValue/long.MaxValue is used 
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public long? NullableLong(float NullProbability, long lower = long.MinValue, long upper = long.MaxValue)
            {
                return NullableGeneric<long>(NullProbability, this.Long, lower, upper);
            }
            /// <summary>
            /// generates a random nullable ulong from interval [lower,upper] <br/>
            /// probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval <br/>
            /// when lower/upper bound is not specified, ulong.MinValue/ulong.MaxValue is used 
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public ulong? NullableUlong(float NullProbability, ulong lower = ulong.MinValue, ulong upper = ulong.MaxValue)
            {
                return NullableGeneric<ulong>(NullProbability, this.Ulong, lower, upper);
            }
            /// <summary>
            /// generates a random nullable char from interval [lower,upper] <br/>
            /// probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval <br/>
            /// when lower/upper bound is not specified, char.MinValue/char.MaxValue is used 
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public char? NullableChar(float NullProbability, char lower = char.MinValue, char upper = char.MaxValue)
            {
                return NullableGeneric<char>(NullProbability, this.Char, lower, upper);
            }
            /// <summary>
            /// generates a random nullable float from interval [lower,upper) <br/>
            /// probability that null is generated is passed as the first param
            /// nullProbability param must belong to [0,1] interval <br/>
            /// when lower/upper bound is not specified, 0/1 is used   <br/>
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public float? NullableFloat(float NullProbability, float lower = 0f, float upper = 1f)
            {
                return NullableGeneric<float>(NullProbability, this.Float, lower, upper);
            }
            /// <summary>
            /// generates a random nullable double from interval [lower,upper) <br/>
            /// probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval <br/>
            /// when lower/upper bound is not specified, 0/1 is used  
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public double? NullableDouble(float NullProbability, double lower = 0d, double upper = 1d)
            {
                return NullableGeneric<double>(NullProbability, this.Double, lower, upper);
            }
            /// <summary>
            /// generates a random nullable decimal from interval [lower,upper) <br/>
            /// probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval <br/>
            /// when lower/upper bound is not specified, 0/1 is used  
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public decimal? NullableDecimal(float NullProbability, decimal lower = 0m, decimal upper = 1m)
            {
                return NullableGeneric<decimal>(NullProbability, this.Decimal, lower, upper);
            }

            /// <summary>
            /// generates a random nullable bool <br/>
            /// probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval <br/>
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public bool? NullableBool(float NullProbability)
            {
                return NullableGeneric<bool>(NullProbability);
            }

            /// <summary>
            /// returns random nullable DateTime from [DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks] interval  <br/>
            /// probability that null is generated is passed as the first param <br/>
            /// nullProbability param must belong to [0,1] interval 
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public DateTime? NullableDateTime(float NullProbability)
            {
                return NullableGeneric<DateTime>(NullProbability);
            }
            /// <summary>
            /// returns random nullable DateTime from [lower,upper] interval <br/>
            /// probability that null is generated is passed as the first param
            /// nullProbability param must belong to [0,1] interval 
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public DateTime? NullableDateTime(float NullProbability, DateTime lower, DateTime upper)
            {
                return NullableGeneric<DateTime>(NullProbability, this.RG.Random.DateTime, lower, upper);
            }

            /// <summary>
            /// returns random nullable Guid <br/>
            /// probability that null is generated is passed as the first param
            /// nullProbability param must belong to [0,1] interval 
            /// <param name="NullProbability">lies in [0,1], probability that null is generated</param>
            /// </summary>
            public Guid? NullableGuid(float NullProbability)
            {
                return NullableGeneric<Guid>(NullProbability);
            }
        }

    

    }
}
