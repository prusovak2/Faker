using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public class RandomGenerator
    {
        private IRandomGeneratorAlg RandomGeneratorAlg { get; set; } = new Xoshiro256starstar();
        public double ZeroToOneDouble()
        {
            ulong random = this.RandomGeneratorAlg.Next();
            random >>= 11; //throw away lower 11 bits of uncertain quality, use remaining 53 bits as significand of [0,1) double
            double result = random / (double) 9007199254740992; // divide by 2^53 - normilize number to [0-1) interval
            return result;
        }
    }
}
