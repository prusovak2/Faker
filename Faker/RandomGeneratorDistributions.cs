using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    public partial class RandomGenerator
    {
        public class RandomDistributions
        {
            /// <summary>
            /// reference to instance of Random generator that has reference to this instance of RandomChar
            /// </summary>
            internal RandomGenerator RG { get; }
            public RandomDistributions(RandomGenerator rg)
            {
                this.RG = rg;
            }

            public double Normal(double mean = 0, double stdDev = 1)
            {
                return NormalDistribution.Double(RG.RandomGeneratorAlg, mean, stdDev);
            }

            public double Exponential(double lambda)
            {
                return ExponentialDistribution.Double(RG.RandomGeneratorAlg, lambda);
            }

            public double Exponential()
            {
                return ExponentialDistribution.Double(RG.RandomGeneratorAlg);
            }

            public bool Bernoulli(double p)
            {
                if( (p > 1) || (p < 0))
                {
                    throw new ArgumentException("Argument p must be a probability eg. a number from interval [0,1].");
                }
                return RG.RandomGeneratorAlg.Next01Double() < p;
            }

            public int BernoulliInt(double p)
            {
                if ((p > 1) || (p < 0))
                {
                    throw new ArgumentException("Argument p must be a probability eg. a number from interval [0,1].");
                }
                if(RG.RandomGeneratorAlg.Next01Double() < p)
                {
                    return 1;
                }
                return 0;
            }

            public int Geometric(double p)
            {
                if ((p > 1) || (p < 0))
                {
                    throw new ArgumentException("Argument p must be a probability eg. a number from interval [0,1].");
                }

                return (int)Math.Ceiling(Math.Log(RG.RandomGeneratorAlg.Next01Double()) / Math.Log(1 - p));
            }
        }
    }
}
