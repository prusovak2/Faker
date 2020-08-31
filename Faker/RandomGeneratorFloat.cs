using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public partial class RandomGenerator
    {
        /// <summary>
        /// generates a random double from interval [0,1)
        /// </summary>
        /// <returns></returns>
        internal double RandomZeroToOneDouble()
        {
            ulong random = this.RandomGeneratorAlg.Next();
            random >>= 11; //throw away lower 11 bits of uncertain quality, use remaining 53 bits as significand of [0,1) double
            double result = random / (double) 9007199254740992; // divide by 2^53 - normalize number to [0-1) interval
            return result;
        }
        /// <summary>
        /// generates random double from inteval [lower, upper) <br/>
        /// when lower/upper bound is not specified, 0/1 is used 
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public double Double(double lower = 0d, double upper = 1d)
        {
            //TODO: are all corner cases solved?

            //to make a generating of doubles from [0,1) interval as fast as possible
            if(lower==0d && upper == 1d)
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
                bool useLower = this.RandomBool();
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
        }

        /// <summary>
        /// generates a random float from interval [0,1)
        /// </summary>
        /// <returns></returns>
        internal float RandomZeroToOneFloat()
        {
            ulong random = this.RandomGeneratorAlg.Next();
            random >>= 41; //it's enough to have 23 random bits to generate a random significand of float number
            float result = random / (float)8388608; // divide by 2^23 - normalize number to [0-1) interval
            return result;
        }
        /// <summary>
        /// generates a random float from interval [lower,upper) <br/>
        /// when lower/upper bound is not specified, 0/1 is used 
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public float Float(float lower = 0f, float upper = 1f) 
        {
            //TODO: are all corner cases solved?

            //to make a generating of floats from [0,1) interval as fast as possible
            if (lower == 0f && upper == 1f)
            {
                return this.RandomZeroToOneFloat();
            }
            // swap numbers so that lower is actually lower
            if (lower.EpsilonEquals(upper))
            {
                return lower;
            }
            if (lower > upper)
            {
                float tmp = lower;
                lower = upper;
                upper = tmp;
            }
            float rangeLenght = Math.Abs(upper - lower); //this will be infinity for too large interval

            //when interval is too large to store its size in float, divide it into two interval of a half size
            if (float.IsInfinity(rangeLenght))
            {
                float middlePoint = (lower + upper) / 2;
                //determine randomly, which of halfintervals is to be used
                bool useLower = this.RandomBool();
                if (useLower)
                {
                    float lowerRandom = this.Float(lower, middlePoint);
                    return lowerRandom;
                }
                else
                {
                    float upperRandom = this.Float(middlePoint, upper);
                    return upperRandom;
                }
            }

            float random01 = this.RandomZeroToOneFloat();
            float scaled = random01 * rangeLenght;
            //shift scaled random number to interval required
            float random = lower + scaled;
            return random;
        }
        /// <summary>
        /// generates a random decimal from interval [0,1)
        /// </summary>
        /// <returns></returns>
        internal decimal RandomZeroToOneDecimal()
        {
            decimal value = 1m;
            while (value >= 1) //it is possible but unlikely to generate combination if low, mid and high, that gives decimal >=1
            {
                int low = this.Int();
                int mid = this.Int();
                int high = this.Int(0, 542101087); //value of high of  0.9999999999999999999999999999m - to keep decimal in [0-1)
                value = new decimal(low, mid, high, false, 28);
            }
            return value;
        }
        /// <summary>
        /// generates a random decimal from interval [lower,upper) <br/>
        /// when lower/upper bound is not specified, 0/1 is used 
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public decimal Decimal(decimal lower = 0m, decimal upper = 1m)
        {
            //TODO: are all corner cases solved?
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
            //TODO: Adjust this for decimals
            //when interval is too large to store its size in ddecimal, divide it into two interval of a half size
            if (RangeTooLarge)
            {
                decimal middlePoint = (lower + upper) / 2;
                bool useLower = this.RandomBool();
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
        }
    }
}
