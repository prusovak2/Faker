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
            /// <summary>
            /// https://math.stackexchange.com/questions/788814/a-binomial-random-number-generating-algorithm-that-works-when-n-times-p-is
            /// </summary>
            /// <param name="p"></param>
            /// <returns></returns>
            public int Geometric(double p)
            {
                if ((p > 1) || (p < 0))
                {
                    throw new ArgumentException("Argument p must be a probability eg. a number from interval [0,1].");
                }

                return (int)Math.Ceiling(Math.Log(RG.RandomGeneratorAlg.Next01Double()) / Math.Log(1 - p));
            }
            //TODO: add arg check
            public int Binomial(int n, double p)
            {
                int count = 0;
                for (; ; )
                {
                    int wait = Geometric(p);
                    if (wait > n)
                    {
                        return count;
                    }
                    count++;
                    n -= wait;
                }
            }

            public int BinomialNaive(int n, double p)
            {
                int counter = 0;
                for (int i = 0; i < n; i++)
                {
                    counter += BernoulliInt(p);
                }
                return counter;
            }

            public double Gamma(double a)
            {
                if (a <= 1)
                {
                    throw new ArgumentException("Argument a must be greater than 1");
                }
                // according to Knuth's book
                //The average number of times step is performed is < 1.902 when a ≥ 3.
                for (; ; ) 
                {
                    double y = Math.Tan(Math.PI * RG.RandomGeneratorAlg.Next01Double());
                    double x = Math.Sqrt(2 * a - 1) * y + a - 1;
                    if (x > 0)
                    {
                        if(RG.RandomGeneratorAlg.Next01NonZeroDouble() <=
                             (1 + y * y) * Math.Exp((a - 1) * Math.Log(x / (a - 1)) - Math.Sqrt(2 * a - 1) * y))
                        {
                            return x;
                        }
                    }
                }
            }

            public double Beta(double a, double b)
            {
                if(a<0 || b < 0)
                {
                    throw new ArgumentException("Arguments must be positive.");
                }
                //gamma distributions in ineffective for a<3 and does not work for a<1
                if((a<3 && b<3) || (a<1 || b<1))
                {
                    for (; ; )
                    {
                        double y1 = Math.Exp(Math.Log(RG.RandomGeneratorAlg.Next01Double() / a));
                        double y2 = Math.Exp(Math.Log(RG.RandomGeneratorAlg.Next01Double() / b));
                        if (y1 + y2 < 1)
                        {
                            return y1 / (y1 + y2);
                        }
                    }
                }
                else
                {
                    double x1 = Gamma(a);
                    double x2 = Gamma(b);
                    return x1 / (x1 + x2);
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="v">degree of freedom</param>
            /// <returns></returns>
            public double ChiSquare(int v)
            {
                if (v < 1)
                {
                    throw new ArgumentException("Degree of freedom must be grater than or equal to 1");
                }
                if (v == 1)
                {
                    double y = Normal();
                    return y * y;
                }
                if (v == 2)
                {
                    double y1 = Normal();
                    double y2 = Normal();
                    return y1 * y1 + y2 * y2;
                }
                else
                {
                    double y = Gamma((double)v / 2d);
                    return 2 * y;
                }
                
            }
        }
    }
}
