using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public partial class RandomGenerator
    {
        public partial class RandomBasicTypes
        {
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
                return (Tval)randomFunc();
            }

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

            public byte? NullableByte(float NullProbability, byte lower = byte.MinValue, byte upper = byte.MaxValue)
            {
                return NullableGeneric<Byte>(NullProbability, this.Byte, lower, upper);
            }

            public sbyte? NullableSbyte(float NullProbability, sbyte lower = sbyte.MinValue, sbyte upper = sbyte.MaxValue)
            {
                return NullableGeneric<sbyte>(NullProbability, this.Sbyte, lower, upper);
            }

            public short? NullableShort(float NullProbability, short lower = short.MinValue, short upper = short.MaxValue)
            {
                return NullableGeneric<short>(NullProbability, this.Short, lower, upper);
            }

            public ushort? NullableUshort(float NullProbability, ushort lower = ushort.MinValue, ushort upper = ushort.MaxValue)
            {
                return NullableGeneric<ushort>(NullProbability, this.Ushort, lower, upper);
            }

            public int? NullableInt(float NullProbability, int lower = int.MinValue, int upper = int.MaxValue)
            {
                return NullableGeneric<int>(NullProbability, this.Int, lower, upper);
            }

            public uint? NullableUint(float NullProbability, uint lower = uint.MinValue, uint upper = uint.MaxValue)
            {
                return NullableGeneric<uint>(NullProbability, this.Uint, lower, upper);
            }

            public long? NullableLong(float NullProbability, long lower = long.MinValue, long upper = long.MaxValue)
            {
                return NullableGeneric<long>(NullProbability, this.Long, lower, upper);
            }

            public ulong? NullableUlong(float NullProbability, ulong lower = ulong.MinValue, ulong upper = ulong.MaxValue)
            {
                return NullableGeneric<ulong>(NullProbability, this.Ulong, lower, upper);
            }

            public char? NullableChar(float NullProbability, char lower = char.MinValue, char upper = char.MaxValue)
            {
                return NullableGeneric<char>(NullProbability, this.Char, lower, upper);
            }

            public float? NullableFloat(float NullProbability, float lower = 0f, float upper = 1f)
            {
                return NullableGeneric<float>(NullProbability, this.Float, lower, upper);
            }

            public double? NullableDouble(float NullProbability, double lower = 0d, double upper = 1d)
            {
                return NullableGeneric<double>(NullProbability, this.Double, lower, upper);
            }

            public decimal? NullableDecimal(float NullProbability, decimal lower = 0m, decimal upper = 1m)
            {
                return NullableGeneric<decimal>(NullProbability, this.Decimal, lower, upper);
            }

            public bool? NullableBool(float NullProbability)
            {
                return NullableGeneric<bool>(NullProbability);
            }

            public DateTime? NullableDateTime(float NullProbability)
            {
                return NullableGeneric<DateTime>(NullProbability);
            }

            public DateTime? NullableDateTime(float NullProbability, DateTime lower, DateTime upper)
            {
                return NullableGeneric<DateTime>(NullProbability, this.RG.Random.DateTime, lower, upper);
            }

            public Guid? NullableGuid(float NullProbability)
            {
                return NullableGeneric<Guid>(NullProbability);
            }
        }

    

    }
}
