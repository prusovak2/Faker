using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public partial class RandomGenerator
    {
        public class RandomList
        {
            /// <summary>
            /// reference to instance of Random generator that has reference to this instance of RandomChar
            /// </summary>
            internal RandomGenerator RG { get; }
            public RandomList(RandomGenerator rg)
            {
                this.RG = rg;
            }

            /// <summary>
            /// returns IList of at most count random bytes from interval [lower, upper]<br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<byte> Byte(int count, byte lower = byte.MinValue, byte upper = byte.MaxValue, bool precise = true)
            {
                return RG.GenericList<byte>(RG.Random.Byte, lower, upper, count, precise);
            }
            /// <summary>
            /// returns IList of at most count random sbytes from interval [lower, upper]<br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<sbyte> Sbyte(int count, sbyte lower = sbyte.MinValue, sbyte upper = sbyte.MaxValue, bool precise = true)
            {
                return RG.GenericList<sbyte>(RG.Random.Sbyte, lower, upper, count, precise);
            }
            /// <summary>
            /// returns IList of at most count random shorts from interval [lower, upper]<br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<short> Short(int count, short lower = short.MinValue, short upper = short.MaxValue, bool precise = true)
            {
                return RG.GenericList<short>(RG.Random.Short, lower, upper, count, precise);
            }
            /// <summary>
            /// returns IList of at most count random ushorts from interval [lower, upper]<br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<ushort> Ushort(int count, ushort lower = ushort.MinValue, ushort upper = ushort.MaxValue, bool precise = true)
            {
                return RG.GenericList<ushort>(RG.Random.Ushort, lower, upper, count, precise);
            }
            /// <summary>
            /// returns IList of at most count random ints from interval [lower, upper]<br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<int> Int(int count, int lower = int.MinValue, int upper = int.MaxValue, bool precise = true)
            {
                return RG.GenericList<int>(RG.Random.Int, lower, upper, count, precise);
            }
            /// <summary>
            /// returns IList of at most count random uints from interval [lower, upper]<br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<uint> Uint(int count, uint lower = uint.MinValue, uint upper = uint.MaxValue, bool precise = true)
            {
                return RG.GenericList<uint>(RG.Random.Uint, lower, upper, count, precise);
            }
            /// <summary>
            /// returns IList of at most count random longs from interval [lower, upper]<br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<long> Long(int count, long lower = long.MinValue, long upper = long.MaxValue, bool precise = true)
            {
                return RG.GenericList<long>(RG.Random.Long, lower, upper, count, precise);
            }
            /// <summary>
            /// returns IList of at most count random ulongs from interval [lower, upper]<br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<ulong> Ulong(int count, ulong lower = ulong.MinValue, ulong upper = ulong.MaxValue, bool precise = true)
            {
                return RG.GenericList<ulong>(RG.Random.Ulong, lower, upper, count, precise);
            }
            /// <summary>
            /// returns IList of at most count random floats from interval [lower, upper) <br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<float> Float(int count, float lower = 0f, float upper = 1f, bool precise = true)
            {
                return RG.GenericList<float>(RG.Random.Float, lower, upper, count, precise);
            }
            /// <summary>
            /// returns IList of at most count random doubles from interval [lower, upper) <br/>
            /// default interval is [0,1), when only one of boarders is not specified Min/Max value is used instead
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<double> Double(int count, double? lower = null, double? upper = null, bool precise = true)
            {
                return RG.GenericList<double>(RG.Random.Double, lower, upper, count, precise);
            }
            /// <summary>
            /// returns IList of at most count random decimals from interval [lower, upper) <br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<decimal> Decimal(int count, decimal lower = 0m, decimal upper = 1m, bool precise = true)
            {
                return RG.GenericList<decimal>(RG.Random.Decimal, lower, upper, count, precise);
            }
            /// <summary>
            /// returns IList of at most count random chars from interval [lower, upper) <br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<char> Char(int count, char lower = char.MinValue, char upper = char.MaxValue, bool precise = true)
            {
                return RG.GenericList<char>(RG.Random.Char, lower, upper, count, precise);
            }
            /// <summary>
            /// returns IList of at most count random bools <br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<bool> Bool(int count, bool precise = true)
            {
                return RG.GenericList<bool>(count, precise);
            }
            /// <summary>
            /// returns IList of at most count random DateTimes from interval [lower, upper] <br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<DateTime> DateTime(int count, DateTime lower, DateTime upper, bool precise = true)
            {
                return RG.GenericList<DateTime>(RG.Random.DateTime, lower, upper, count, precise);
            }
            /// <summary>
            /// returns IList of at most count random DateTimes from interval [DateTime.MinValue, DateTime.MaxValue] <br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<DateTime> DateTime(int count, bool precise = true)
            {
                return RG.GenericList<DateTime>(count, precise);
            }
            /// <summary>
            /// returns IList of at most count random Guids <br/>
            /// when precise is false a random number less or equal to count is generated and used as a count
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public IList<Guid> Guid(int count, bool precise = true)
            {
                return RG.GenericList<Guid>(count, precise);
            }
            /// <summary>
            /// returns IList of at most count random strings <br/>
            /// when precise is false a random number less or equal to count is generated and used as a count <br/>
            /// length of each string is strLen when strLenPrecise is true, less or equal to strLen otherwise
            /// </summary>
            /// <param name="count"></param>
            /// <param name="precise"></param>
            /// <param name="strLen"></param>
            /// <param name="strLenPrecise"></param>
            /// <returns></returns>
            public IList<string> String(int count, bool precise = true, int strLen = 255, bool strLenPrecise = true)
            {
                return RG.GenericList<string>(RG.String.String, count, precise, strLen, strLenPrecise);
            }
        }
    }
  
}
