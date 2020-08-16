using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public partial class RandomGenerator
    {
        private IRandomGeneratorAlg RandomGeneratorAlg { get; set; } 
        public RandomGenerator()
        {
            this.RandomGeneratorAlg = new Xoshiro256starstar();
        }
        public RandomGenerator(ulong seed)
        {
            this.RandomGeneratorAlg = new Xoshiro256starstar(seed);
        }
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
        /// generates random double from inteval [lower, upper)
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public double RandomDouble(double lower, double upper)
        {
            //TODO: are all corner cases solved?
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
                //Console.WriteLine("middle point {0}", middlePoint);
                double lowerRandom = this.RandomDouble(lower, middlePoint);
                //determine randomly, which of halfintervals is to be used
                bool useLower = this.RandomBool();
                if (useLower)
                {
                    return lower;
                }
                else
                {
                    double upperRandom = this.RandomDouble(middlePoint, upper);
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
        /// generates a random boolean
        /// </summary>
        /// <returns></returns>
        public bool RandomBool()
        {
            double random = this.RandomZeroToOneDouble();
            if (random < 0.5)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// generates a random double from interval [double.MinValue,Double.MaxValue)
        /// </summary>
        /// <returns></returns>
        public double RandomDouble()
        {
            double randomDouble = this.RandomDouble(double.MinValue, double.MaxValue);
            return randomDouble;
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
        /// generates a random float from interval [lowrr,upper)
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public float RandomFloat(float lower, float upper) 
        {
            //TODO: are all corner cases solved?
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
                float lowerRandom = this.RandomFloat(lower, middlePoint);
                //determine randomly, which of halfintervals is to be used
                bool useLower = this.RandomBool();
                if (useLower)
                {
                    return lower;
                }
                else
                {
                    float upperRandom = this.RandomFloat(middlePoint, upper);
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
        /// generates a random float from interval [float.MinValue,floa.MaxValue)
        /// </summary>
        /// <returns></returns>
        public float RandomFloat()
        {
            float randomFloat = this.RandomFloat(float.MinValue, float.MaxValue);
            return randomFloat;
        }
    }
}
