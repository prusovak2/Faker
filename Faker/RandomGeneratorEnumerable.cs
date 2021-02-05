using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public partial class RandomGenerator
    {
        public partial class RandomEnumerable
        {
            /// <summary>
            /// reference to instance of Random generator that has reference to this instance of RandomEnumerable
            /// </summary>
            internal RandomGenerator RG { get; }
            public RandomEnumerable(RandomGenerator rg)
            {
                this.RG = rg;
            }
            /// <summary>
            /// returns IEnumerable of at most count random ints from interval [lower, upper]<br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public IEnumerable<int> Int(int? count = null, int lower = int.MinValue, int upper = int.MaxValue, bool precise = true)
            {
                if(count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<int>(this.RG.Random.Int, lower, upper);
                }
                return this.RG.GenericEnumerable<int>(this.RG.Random.Int, lower, upper, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random uints from interval [lower, upper]<br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public IEnumerable<uint> Uint(int? count = null, uint lower = uint.MinValue, uint upper = uint.MaxValue, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<uint>(this.RG.Random.Uint, lower, upper);
                }
                return this.RG.GenericEnumerable<uint>(this.RG.Random.Uint, lower, upper, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random shorts from interval [lower, upper]<br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public IEnumerable<short> Short(int? count = null, short lower = short.MinValue, short upper = short.MaxValue, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<short>(this.RG.Random.Short, lower, upper);
                }
                return this.RG.GenericEnumerable<short>(this.RG.Random.Short, lower, upper, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random ushorts from interval [lower, upper]<br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public IEnumerable<ushort> Ushort(int? count = null, ushort lower = ushort.MinValue, ushort upper = ushort.MaxValue, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<ushort>(this.RG.Random.Ushort, lower, upper);
                }
                return this.RG.GenericEnumerable<ushort>(this.RG.Random.Ushort, lower, upper, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random sbytes from interval [lower, upper]<br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public IEnumerable<sbyte> Sbyte(int? count = null, sbyte lower = sbyte.MinValue, sbyte upper = sbyte.MaxValue, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<sbyte>(this.RG.Random.Sbyte, lower, upper);
                }
                return this.RG.GenericEnumerable<sbyte>(this.RG.Random.Sbyte, lower, upper, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random bytes from interval [lower, upper]<br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public IEnumerable<byte> Byte(int? count = null, byte lower = byte.MinValue, byte upper = byte.MaxValue, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<byte>(this.RG.Random.Byte, lower, upper);
                }
                return this.RG.GenericEnumerable<byte>(this.RG.Random.Byte, lower, upper, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random longs from interval [lower, upper]<br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public IEnumerable<long> Long(int? count = null, long lower = long.MinValue, long upper = long.MaxValue, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<long>(this.RG.Random.Long, lower, upper);
                }
                return this.RG.GenericEnumerable<long>(this.RG.Random.Long, lower, upper, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random ulongs from interval [lower, upper]<br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public IEnumerable<ulong> Ulong(int? count = null, ulong lower = ulong.MinValue, ulong upper = ulong.MaxValue, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<ulong>(this.RG.Random.Ulong, lower, upper);
                }
                return this.RG.GenericEnumerable<ulong>(this.RG.Random.Ulong, lower, upper, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random doubles from interval [lower, upper)<br/>
            /// default interval is [0,1), when only one of boarders is not specified Min/Max value is used instead <br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public IEnumerable<double> Double(int? count = null, double? lower = null, double? upper = null, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<double>(this.RG.Random.Double, lower, upper);
                }
                return this.RG.GenericEnumerable<double>(this.RG.Random.Double, lower, upper, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random floats from interval [lower, upper)<br/>
            /// default interval is [0,1), when only one of boarders is not specified Min/Max value is used instead <br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public IEnumerable<float> Float(int? count = null, float? lower = null, float? upper = null, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<float>(this.RG.Random.Float, lower, upper);
                }
                return this.RG.GenericEnumerable<float>(this.RG.Random.Float, lower, upper, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random decimals from interval [lower, upper)<br/>
            ///  default interval is [0,1), when only one of boarders is not specified Min/Max value is used instead <br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public IEnumerable<Decimal> Decimal(int? count = null, decimal? lower = null, decimal? upper = null, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<decimal>(this.RG.Random.Decimal, lower, upper);
                }
                return this.RG.GenericEnumerable<decimal>(this.RG.Random.Decimal, lower, upper, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random DateTimes from interval [lower, upper]<br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IEnumerable<DateTime> DateTime(DateTime lower, DateTime upper, int? count = null, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<DateTime>(this.RG.Random.DateTime, lower, upper);
                }
                return this.RG.GenericEnumerable<DateTime>(this.RG.Random.DateTime, lower, upper, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random DateTimes from interval [DateTime.MinValue, DateTime.MaxValue]<br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IEnumerable<DateTime> DateTime(int? count = null, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<DateTime>(this.RG.Random.DateTime);
                }
                return this.RG.GenericEnumerable<DateTime>(this.RG.Random.DateTime, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random Guids <br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IEnumerable<Guid> Guid(int? count = null, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<Guid>(this.RG.Random.Guid);
                }
                return this.RG.GenericEnumerable<Guid>(this.RG.Random.Guid, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random Bools <br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IEnumerable<bool> Bool(int? count = null, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<bool>(this.RG.Random.Bool);
                }
                return this.RG.GenericEnumerable<bool>(this.RG.Random.Bool, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random chars from interval [lower, upper]<br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public IEnumerable<char> Char(int? count = null, char lower = char.MinValue, char upper = char.MaxValue, bool precise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<char>(this.RG.Random.Char, lower, upper);
                }
                return this.RG.GenericEnumerable<char>(this.RG.Random.Char, lower, upper, count.Value, precise);
            }
            /// <summary>
            /// returns IEnumerable of at most count random strings <br/>
            /// when count is null, returned IEnumerable is INFINITE <br/>
            /// when precise is false and count not null, a random number less or equal to count is generated and used as a count<br/>
            /// length of each string is strLen when strLenPrecise is true, less or equal to strLen otherwise
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <param name="strLen"></param>
            /// <param name="strLenPrecise"></param>
            /// <returns></returns>
            public IEnumerable<string> String(int? count = null, bool precise = true, int strLen = 255, bool strLenPrecise = true)
            {
                if (count is null)
                {
                    return this.RG.InfiniteGenericEnumerable<string>(this.RG.String.String, strLen, strLenPrecise);
                }
                return this.RG.GenericEnumerable<string>(this.RG.String.String, count.Value, precise, strLen, strLenPrecise);
            }
        }

    }

}
