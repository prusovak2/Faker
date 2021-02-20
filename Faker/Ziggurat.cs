using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker;

namespace Faker
{
    internal abstract class Ziggurat
    {
        protected const int numBlocks = 128;

        protected const double r = 3.442619855899;

        protected const double area = 9.91256303526217e-3;

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

            //
            ulong uSector = generatorAlg.Next();
            ulong uppermostBit = uSector & 0x8000_0000_0000_0000; //one and 63 zeros



            Sign(42);
            throw new NotImplementedException();
        }

        protected abstract double SampleTail();

        protected abstract double ProbabilityDensityFunction(double x);

        protected abstract double ProbabilytyDensityInverz(double y);

        protected abstract double Sign(ulong random);
    }

    sealed class NormalDistribution : Ziggurat
    {
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
            throw new NotImplementedException();
        }
    }
}
