using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public class RandomGenerator
    {
        private IRandomGeneratorAlg RandomGeneratorAlg { get; set; } = new Xoshiro256starstar();
        public RandomGenerator()
        {
            this.RandomGeneratorAlg = new Xoshiro256starstar();
        }
        public RandomGenerator(ulong seed)
        {
            this.RandomGeneratorAlg = new Xoshiro256starstar(seed);
        }
        public double ZeroToOneDouble()
        {
            ulong random = this.RandomGeneratorAlg.Next();
            random >>= 11; //throw away lower 11 bits of uncertain quality, use remaining 53 bits as significand of [0,1) double
            double result = random / (double) 9007199254740992; // divide by 2^53 - normalize number to [0-1) interval
            return result;
        }
        public double DoubleRangePossitiveForATimeBeing(double lower, double upper)
        {
            //TODO: finish! SOLVE ALL CORNER CASES, THIS IS JUST A  BAREBONE IDEA! NEGATIVE NUMBERS NOT TAKEN INTO ACCOUNT AT ALL!
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
            double rangeLenght = upper - lower;
            double random01 = this.ZeroToOneDouble();
            double scaled = random01 * rangeLenght;
            double random = lower + scaled;
            return random;
        }
    }
}
