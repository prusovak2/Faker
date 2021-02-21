using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker;

namespace Faker
{
    internal abstract class Ziggurat
    {
        protected const int numBlocks = 128;

        protected abstract double r { get; }

        protected abstract double area { get; }

        protected static double[] x { get; set; } = new double[numBlocks + 1];

        protected static double[] y { get; set; } = new double[numBlocks];

        protected static ulong[] xRatios { get; set; } = new ulong[numBlocks];

        protected const ulong max53bitUlong = (1UL << 53) - 1;

        protected const double scaleTo01 = 1.0 / max53bitUlong;

        protected IRandomGeneratorAlg generatorAlg;

        protected static void Initialize()
        {
            throw new NotImplementedException();
        }

        protected double Generate()
        {
            ulong uMantis = generatorAlg.Next();
            uMantis >>= 11; //throw away lower 11 bits of uncertain quality

            ulong uSector = generatorAlg.Next();

            //use the most significant bit (bit 63) to determine sign
            //necessary for normal distribution as alg only covers its right half, to get values from left half, sign is randomly generated
            //empty operation returning 1 for exponential distribution 
            double sign = Sign(uSector);

            //get bits 62 to 56 (7 bits determining sector number from {0, 1, ..., 127} )
            uSector >>= 56;
            uSector &= 0x7F; //0111_1111
            Debug.Assert((uSector >= 0) && (uSector <= 127));

            
            throw new NotImplementedException();
        }

        protected abstract double SampleTail();

        protected abstract double ProbabilityDensityFunction(double x);

        protected abstract double ProbabilytyDensityInverz(double y);

        protected abstract double Sign(ulong random=0);
    }

    sealed class NormalDistribution : Ziggurat
    {
        protected override double r => 3.442619855899;
        protected override double area => 9.91256303526217e-3;

        protected override double ProbabilityDensityFunction(double x)
        {
            throw new NotImplementedException();
        }

        protected override double ProbabilytyDensityInverz(double y)
        {
            throw new NotImplementedException();
        }

        protected override double SampleTail()
        {
            throw new NotImplementedException();
        }

        protected override double Sign(ulong random)
        {
            ulong uppermostBit = random & 0x8000_0000_0000_0000; //one and 63 zeros
            ulong bitsOfDoubleOne = 0x3ff0_0000_0000_0000UL; //double one in binary representation
            double sign = BitConverter.Int64BitsToDouble(unchecked((long)(bitsOfDoubleOne | uppermostBit)));
            Debug.Assert((sign == 1) || (sign == -1));
            return sign;
        }
    }

    sealed class ExponentialDistribution : Ziggurat
    {
        protected override double r => 7.69711747013104972;

        protected override double area => 0.0039496598225815571993;

        protected override double ProbabilityDensityFunction(double x)
        {
            throw new NotImplementedException();
        }

        protected override double ProbabilytyDensityInverz(double y)
        {
            throw new NotImplementedException();
        }

        protected override double SampleTail()
        {
            throw new NotImplementedException();
        }

        protected override double Sign(ulong random = 0)
        {
            return 1;
        }
    }
}
