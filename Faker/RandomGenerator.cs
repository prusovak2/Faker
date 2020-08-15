using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public class RandomGenerator
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
        public double RandomZeroToOneDouble()
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
        public double RandomDoubleFromRange(double lower, double upper)
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
                double lowerRandom = this.RandomDoubleFromRange(lower, middlePoint);
                //determine randomly, which of halfintervals is to be used
                bool useLower = this.RandomBool();
                if (useLower)
                {
                    return lower;
                }
                else
                {
                    double upperRandom = this.RandomDoubleFromRange(middlePoint, upper);
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
        /// generates a random double
        /// </summary>
        /// <returns></returns>
        public double RandomDouble()
        {
            double randomDouble = this.RandomDoubleFromRange(double.MinValue, double.MaxValue);
            return randomDouble;
        }
        public float RandomFloat()
        {
            double inFloatRange = this.RandomDoubleFromRange(float.MinValue, float.MaxValue);
            float randomFloat = (float)inFloatRange;
            return randomFloat;
        }
        //TODO:check wheather this works and concider generating floats independentally on doubles
        public float RandomFloatFromRange(float lower, float upper)
        {
            return (float) this.RandomDoubleFromRange(lower, upper);
        }
    }
}
