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
            /// reference to instance of Random generator that has reference to this instance of RandomChar
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
        }

    }

}
