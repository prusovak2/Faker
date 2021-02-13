using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Exporters;
using Faker;


namespace FakerBenchmark
{
    /*[MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class NullableParamsCompoundBenchmark
    {
        public RandomGenerator random;

        [GlobalSetup]
        public void GlobalSetup()
        {
            random = new RandomGenerator();
        }

        public void DoubleNullableCompoundTest()
        {
            random.Random.DoubleNullableParams();
            random.Random.DoubleNullableParams(upper: -1);
            random.Random.DoubleNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void DoubleNullableCompound()
        {
            DoubleNullableCompoundTest();
        }

        public void DecimalNullableCompoundTest()
        {
            random.Random.DecimalNullableParams();
            random.Random.DecimalNullableParams(upper: -1);
            random.Random.DecimalNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void DecimalNullableCompound()
        {
            DecimalNullableCompoundTest();
        }

        public void IntNullableCompoundTest()
        {
            random.Random.IntNullableParams();
            random.Random.IntNullableParams(upper: -1);
            random.Random.IntNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void IntNullableCompound()
        {
            IntNullableCompoundTest();
        }

        public void LongNullableCompoundTest()
        {
            random.Random.LongNullableParams();
            random.Random.LongNullableParams(upper: -1);
            random.Random.LongNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void LongNullableCompound()
        {
            LongNullableCompoundTest();
        }

        public void DoubleCompoundTest()
        {
            random.Random.Double();
            random.Random.Double(upper: -1);
            random.Random.Double(-1000000, 1000000);
        }

        [Benchmark]
        public void DoubleCompound()
        {
            DoubleCompoundTest();
        }

        public void DecimalCompoundTest()
        {
            random.Random.Decimal();
            random.Random.Decimal(upper: -1);
            random.Random.Decimal(-1000000, 1000000);
        }

        [Benchmark]
        public void DecimalCompound()
        {
            DecimalCompoundTest();
        }

        public void IntCompoundTest()
        {
            random.Random.Int();
            random.Random.Int(upper: -1);
            random.Random.Int(-1000000, 1000000);
        }

        [Benchmark]
        public void IntCompound()
        {
            IntCompoundTest();
        }

        public void LongCompoundTest()
        {
            random.Random.Long();
            random.Random.Long(upper: -1);
            random.Random.Long(-1000000, 1000000);
        }

        [Benchmark]
        public void LongCompound()
        {
            LongCompoundTest();
        }
    }*/


    /*[MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class NullableParamsBasicBenchmarks
    {
        public RandomGenerator random;

        [GlobalSetup]
        public void GlobalSetup()
        {
            random = new RandomGenerator();
        }

        [Benchmark]
        public void DoubleNullableParamless()
        {
            random.Random.DoubleNullableParams();
        }

        [Benchmark]
        public void DoubleNullableOneParam()
        {
            random.Random.DoubleNullableParams(upper:-1) ;
        }

        [Benchmark]
        public void DoubleNullableTwoParams()
        {
            random.Random.DoubleNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void DecimalNullableParamless()
        {
            random.Random.DecimalNullableParams();
        }

        [Benchmark]
        public void DecimalNullableOneParam()
        {
            random.Random.DecimalNullableParams(upper: -1);
        }

        [Benchmark]
        public void DecimalNullableTwoParams()
        {
            random.Random.DecimalNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void IntNullableParamless()
        {
            random.Random.IntNullableParams();
        }

        [Benchmark]
        public void IntNullableOneParam()
        {
            random.Random.IntNullableParams(upper: -1);
        }

        [Benchmark]
        public void IntNullableTwoParams()
        {
            random.Random.IntNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void LongNullableParamless()
        {
            random.Random.LongNullableParams();
        }

        [Benchmark]
        public void LongNullableOneParam()
        {
            random.Random.LongNullableParams(upper: -1);
        }

        [Benchmark]
        public void LongNullableTwoParams()
        {
            random.Random.LongNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void DoubleParamless()
        {
            random.Random.Double();
        }

        [Benchmark]
        public void DoubleOneParam()
        {
            random.Random.Double(upper: -1);
        }

        [Benchmark]
        public void DoubleTwoParams()
        {
            random.Random.Double(-1000000, 1000000);
        }

        [Benchmark]
        public void DecimalParamless()
        {
            random.Random.Decimal();
        }

        [Benchmark]
        public void DecimalOneParam()
        {
            random.Random.Decimal(upper: -1);
        }

        [Benchmark]
        public void DecimalTwoParams()
        {
            random.Random.Decimal(-1000000, 1000000);
        }

        [Benchmark]
        public void IntParamless()
        {
            random.Random.Int();
        }

        [Benchmark]
        public void IntOneParam()
        {
            random.Random.Int(upper: -1);
        }

        [Benchmark]
        public void IntTwoParams()
        {
            random.Random.Int(-1000000, 1000000);
        }

        [Benchmark]
        public void LongParamless()
        {
            random.Random.LongNullableParams();
        }

        [Benchmark]
        public void LongOneParam()
        {
            random.Random.Long(upper: -1);
        }

        [Benchmark]
        public void LongTwoParams()
        {
            random.Random.Long(-1000000, 1000000);
        }
    }*/

    class NonNullableArchive
    {
        /// <summary>
        /// generates random double from interval [lower, upper) <br/>
        /// when lower/upper bound is not specified, 0/1 is used 
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        /*public double Double(double lower = 0d, double upper = 1d)
        {
            //to make a generating of doubles from [0,1) interval as fast as possible
            if (lower == 0d && upper == 1d)
            {
                return this.RandomZeroToOneDouble();
            }

            // swap numbers so that lower is actually lower
            if (lower.EpsilonEquals(upper))
            {
                return lower;
            }
            if (lower > upper)
            {
                double tmp = lower;
                lower = upper;
                upper = tmp;
            }
            double rangeLenght = Math.Abs(upper - lower); //this will be infinity for too large interval

            //when interval is too large to store its size in double, divide it into two interval of a half size
            if (double.IsInfinity(rangeLenght))
            {
                double middlePoint = (lower + upper) / 2;
                //determine randomly, which of halfintervals is to be used
                bool useLower = this.Bool();
                if (useLower)
                {
                    double lowerRandom = this.Double(lower, middlePoint);
                    return lowerRandom;
                }
                else
                {
                    double upperRandom = this.Double(middlePoint, upper);
                    return upperRandom;
                }
            }

            double random01 = this.RandomZeroToOneDouble();
            double scaled = random01 * rangeLenght;
            //shift scaled random number to interval required
            double random = lower + scaled;
            return random;
        }*/

        /// <summary>
        /// generates a random decimal from interval [lower,upper) <br/>
        /// when lower/upper bound is not specified, 0/1 is used 
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        /*public decimal Decimal(decimal lower = 0m, decimal upper = 1m)
        {
            //to make a generating of doubles from [0,1) interval as fast as possible
            if (lower == 0m && upper == 1m)
            {
                return this.RandomZeroToOneDecimal();
            }

            // swap numbers so that lower is actually lower
            if (lower.EpsilonEquals(upper, out bool RangeTooLarge))
            {
                return lower;
            }
            if (lower > upper)
            {
                decimal tmp = lower;
                lower = upper;
                upper = tmp;
            }
            //when interval is too large to store its size in ddecimal, divide it into two interval of a half size
            if (RangeTooLarge)
            {
                decimal middlePoint = (lower + upper) / 2;
                bool useLower = this.Bool();
                if (useLower)
                {
                    decimal lowerRandom = this.Decimal(lower, middlePoint);
                    return lowerRandom;
                }
                else
                {
                    decimal upperRandom = this.Decimal(middlePoint, upper);
                    return upperRandom;
                }
            }
            //only reached, when interval size is small enough to fit into decimal
            decimal rangeLenght = Math.Abs(upper - lower);
            decimal random01 = this.RandomZeroToOneDecimal();
            decimal scaled = random01 * rangeLenght;
            //shift scaled random number to interval required
            decimal random = lower + scaled;
            return random;
        }*/

        /*public long LongNullableParams(long? lower = long.MinValue, long? upper = long.MaxValue)
        {
            double randomDouble = this.Double(lower.Value, upper.Value);
            long randomLong = (long)Math.Round(randomDouble);
            return randomLong;
        }*/

        /*public int IntNullableParams(int? lower = int.MinValue, int? upper = int.MaxValue)
        {
            double randomDouble = this.Double(lower.Value, upper.Value);
            int randomInt = (int)Math.Round(randomDouble);
            return randomInt;
        }*/
    }
}
