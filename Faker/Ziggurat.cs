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
        /// <summary>
        /// num of rectangles to be used to cover a distribution
        /// </summary>
        protected const int numBlocks = 128;

        /// <summary>
        /// Right hand x coord of the base rectangle, thus also the left hand x coord of the tail <br/>
        /// precomputed, taken from https://www.researchgate.net/publication/5142790_The_Ziggurat_Method_for_Generating_Random_Variables
        /// </summary>
        protected abstract double r { get; }
        /// <summary>
        /// the area of one sector, that is area of rectangles apart from the base one and the area of base rectangle plus the area of the tail 
        /// precomputed, taken from https://www.researchgate.net/publication/5142790_The_Ziggurat_Method_for_Generating_Random_Variables
        /// </summary>
        protected abstract double area { get; }

        /// <summary>
        /// coord of right side of rectangles
        /// </summary>
        protected double[] x { get; private set; } = new double[numBlocks + 1];
        /// <summary>
        /// coord of upper edge of rectangles
        /// </summary>
        protected double[] y { get; private set; } = new double[numBlocks];
        /// <summary>
        /// probability that point belonging to this segment lies in the left part of the rectangle that is completely under the curve <br/>
        /// xi+1/xi ratio multiplied by 2^53-1 so that value can be compared with generated random 53 ulong right away using integer comparison 
        /// </summary>
        protected ulong[] xRatios { get; private set; } = new ulong[numBlocks];

        protected const ulong max53bitUlong = (1UL << 53) - 1;

        protected const double scaleTo01 = 1.0 / max53bitUlong;

        protected double __A_Div_Y0 { get; private set; }

        protected void Initialize()
        {
            // Determine top right position of the base rectangle R0 (the rectangle with the Gaussian tale attached).
            // x[0] also describes the right-hand edge of R1 as R0 is all under the curve and R1 right lower corner lies on the curve
            x[0] = r; // x0 = r
            y[0] = ProbabilityDensityFunction(r); // y0 = f(r)

            x[1] = r; //R0 and R1 share the x coord of right hand edge
            y[1] = y[0] + (area / x[1]); //y1 = y0 + (hight of R1) = y0 + area/(length of the bottom edge)

            // Calc right hand upper corner coords of remaining rectangles
            for (int i = 2; i < numBlocks; i++)
            {
                //yi-1 is y coord of the bottom of the i-th rectangle, rectangle touches the curve with its right lower corner,
                // y coord of this corner is yi-1, count the x coord as inverse at the y value
                x[i] = ProbabilytyDensityInverse(y[i - 1]); // xi = f^-1(yi-1)
                y[i] = y[i - 1] + (area / x[i]); // yi = yi-1 + area/(length of the bottom edge)
            }

            //up most box with no area
            x[numBlocks] = 0.0;

            __A_Div_Y0 = area / y[0];

            //the base segment consist of the rectangular part and the tail of the distribution
            //xRatios[0] is a probability that sample point from the base segment lies in it's rectangular part,
            //that lies all under the distribution curve
            xRatios[0] = (ulong)(((r * y[0]) / area) * max53bitUlong);

            //count remaining ratios 
            for (int i = 1; i < numBlocks - 1; i++)
            {
                xRatios[i] = (ulong)((x[i + 1] / x[i]) * (double)max53bitUlong);
            }
            xRatios[numBlocks - 1] = 0;  //unnecessary?

            // Sanity check. Test that the top edge of the topmost rectangle is at y=1.0.
            Debug.Assert(Math.Abs(1.0 - y[numBlocks - 1]) < 1e-10);
        }

        protected double Generate(IRandomGeneratorAlg generatorAlg)
        {
            for(; ; ) //rejection sampling algorithm
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

                // Base segment.
                if (uSector == 0)
                {
                    if (uMantis < xRatios[0])
                    {
                        // Generated sample lies within the rectangular part of the base segment
                        return uMantis * scaleTo01 * __A_Div_Y0 * sign;
                    }
                    // Generated sample lies in the tail of the distribution.
                    return SampleTail(generatorAlg) * sign;
                }

                // All other segments.
                if (uMantis < xRatios[uSector])
                {
                    // Generated x is within the rectangle.
                    return  uMantis * scaleTo01 * this.x[uSector] * sign;
                }

                // Generated x is in ordinary segment outside of the rectangle.
                // Generate a random y coordinate and test if (x,y) is under the distribution curve.
                // if not, reject the sample and run another iteration of for cycle
                // This execution path is relatively slow/expensive (makes a call to Math.Exp()) but is relatively rarely executed,
                // although more often than the 'tail' path (above).
                double xSample = uMantis * scaleTo01 * x[uSector];
                //generate y coord within selected segment
                double ySample = y[uSector - 1] + ((y[uSector] - y[uSector - 1]) * generatorAlg.Next01Double());
                // if generated (x,y) point is below the curve, return it else reject it 
                if (ySample < ProbabilityDensityFunction(xSample))
                {
                    return xSample * sign;
                }
            }
        }

        protected abstract double SampleTail(IRandomGeneratorAlg generatorAlg);

        protected abstract double ProbabilityDensityFunction(double x);

        protected abstract double ProbabilytyDensityInverse(double y);

        protected abstract double Sign(ulong random=0);
    }

    sealed class NormalDistribution : Ziggurat
    {
        /// <summary>
        /// Right hand x coord of the base rectangle, thus also the left hand x coord of the tail
        /// precomputed, taken from https://www.researchgate.net/publication/5142790_The_Ziggurat_Method_for_Generating_Random_Variables
        /// </summary>
        protected override double r => 3.442619855899;
        /// <summary>
        /// the area of one sector, that is area of rectangles apart from the base one and the area of base rectangle plus the area of the tail 
        /// precomputed, taken from https://www.researchgate.net/publication/5142790_The_Ziggurat_Method_for_Generating_Random_Variables
        /// </summary>
        protected override double area => 9.91256303526217e-3;

        /// <summary>
        /// Gaussian probability density function, denormalised, that is, y = e^-(x^2/2).
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected override double ProbabilityDensityFunction(double x)
        {
            return Math.Exp(-(x * x / 2.0));
        }
        /// <summary>
        /// Inverse function to Gaussian probability density function 
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        protected override double ProbabilytyDensityInverse(double y)
        {
            //TODO:delete this copied comment ?
            // Operates over the y interval (0,1], which happens to be the y interval of the pdf,
            // with the exception that it does not include y=0, but we would never call with
            // y=0 so it doesn't matter. Note that a Gaussian effectively has a tail going
            // into infinity on the x-axis, hence asking what is x when y=0 is an invalid question
            // in the context of this class.
            return Math.Sqrt(-2.0 * Math.Log(y)); ;
        }

        protected override double SampleTail(IRandomGeneratorAlg generatorAlg)
        {
            double x, y;
            do
            {
                // Log(0) returns -Infinity thats why only non zero doubles are generated
                x = -Math.Log(generatorAlg.Next01NonZeroDouble()) / r;
                y = -Math.Log(generatorAlg.Next01NonZeroDouble());
            }
            while (y + y < x * x);
            return r + x;
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
        /// <summary>
        /// Right hand x coord of the base rectangle, thus also the left hand x coord of the tail
        /// precomputed, taken from https://www.researchgate.net/publication/5142790_The_Ziggurat_Method_for_Generating_Random_Variables
        /// </summary>
        protected override double r => 7.69711747013104972;
        /// <summary>
        /// the area of one sector, that is area of rectangles apart from the base one and the area of base rectangle plus the area of the tail 
        /// precomputed, taken from https://www.researchgate.net/publication/5142790_The_Ziggurat_Method_for_Generating_Random_Variables
        /// </summary>
        protected override double area => 0.0039496598225815571993;

        protected override double ProbabilityDensityFunction(double x)
        {
            return Math.Exp(-x);
        }

        protected override double ProbabilytyDensityInverse(double y)
        {
            return -Math.Log(y);
        }

        protected override double SampleTail(IRandomGeneratorAlg generatorAlg)
        {
            return (r - Math.Log(generatorAlg.Next01NonZeroDouble()));
        }

        protected override double Sign(ulong random = 0)
        {
            return 1;
        }
    }
}
