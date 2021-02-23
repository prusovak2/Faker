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
            //TODO: add poisson distribution? Knuth's Art of computer programing page 137
            //TODO: make some of the calculations multi threaded?

            /// <summary>
            /// reference to instance of Random generator that has reference to this instance of RandomChar
            /// </summary>
            internal RandomGenerator RG { get; }
            public RandomDistributions(RandomGenerator rg)
            {
                this.RG = rg;
            }
            /// <summary>
            /// Returns double from Normal (Gaussian) distribution
            /// </summary>
            /// <param name="mean">mean of distribution</param>
            /// <param name="stdDev">standard deviation of distribution</param>
            /// <returns></returns>
            public double Normal(double mean = 0, double stdDev = 1)
            {
                return NormalDistribution.Double(RG.RandomGeneratorAlg, mean, stdDev);
            }

            /// <summary>
            /// Returns double from Exponential distribution with parameter lambda 
            /// </summary>
            /// <param name="lambda"></param>
            /// <returns></returns>
            public double Exponential(double lambda)
            {
                return ExponentialDistribution.Double(RG.RandomGeneratorAlg, lambda);
            }
            /// <summary>
            /// Returns double from Exponential distribution
            /// </summary>
            /// <returns></returns>
            public double Exponential()
            {
                return ExponentialDistribution.Double(RG.RandomGeneratorAlg);
            }
           /// <summary>
           /// Returns true with the probability p and false with probability (1-p)
           /// </summary>
           /// <param name="p">must belong to [0,1]</param>
           /// <returns></returns>
            public bool Bernoulli(double p)
            {
                if( (p > 1) || (p < 0))
                {
                    throw new ArgumentException("Argument p must be a probability eg. a number from interval [0,1].");
                }
                return RG.RandomGeneratorAlg.Next01Double() < p;
            }
            /// <summary>
            /// Returns 1 with the probability p and 0 with probability (1-p)
            /// </summary>
            /// <param name="p">must belong to [0,1]</param>
            /// <returns></returns>
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
            /// Returns int from Geometric distribution with parameter p 
            /// </summary>
            /// <param name="p">probability that Bernoulli trial is successful, must belong to [0,1]</param>
            /// <returns></returns>
            public int Geometric(double p)
            {
                if ((p > 1) || (p < 0))
                {
                    throw new ArgumentException("Argument p must be a probability eg. a number from interval [0,1].");
                }

                return (int)Math.Ceiling(Math.Log(RG.RandomGeneratorAlg.Next01Double()) / Math.Log(1 - p));
            }
            /// <summary>
            /// Returns int from Binomial distribution
            /// ineffective!
            /// </summary>
            /// <param name="n"></param>
            /// <param name="p">must belong to [0,1]</param>
            /// <returns></returns>
            public int Binomial(int n, double p)
            {
                if ((p > 1) || (p < 0) || (n<1))
                {
                    throw new ArgumentException("Argument p must be a probability eg. a number from interval [0,1] and n must be greater than or equal to one.");
                }
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

            internal int BinomialNaive(int n, double p)
            {
                if ((p > 1) || (p < 0) || (n < 1))
                {
                    throw new ArgumentException("Argument p must be a probability eg. a number from interval [0,1] and n must be greater than or equal to one.");
                }
                int counter = 0;
                for (int i = 0; i < n; i++)
                {
                    counter += BernoulliInt(p);
                }
                return counter;
            }
            /// <summary>
            /// based on Knuth's book, but histogram looks weird
            /// </summary>
            /// <param name="n"></param>
            /// <param name="p">must belong to [0,1]</param>
            /// <returns></returns>
            internal int BinomialClever(int n, double p)
            {
                if (n < 50)
                {
                    return Binomial(n, p);
                }
                if ((p > 1) || (p < 0))
                {
                    throw new ArgumentException("Argument p must be a probability eg. a number from interval [0,1].");
                }
                int a = 1 + n / 2;
                int b = n + 1 - a;
                double x = Beta(a, b);
                if (x > p)
                {
                    return Binomial(a - 1, p / x);
                }
                else
                {
                    return a + Binomial(b - 1, (p - x)*(1 - x));
                }
            }
            
            /// <summary>
            /// Returns double from Gamma distribution
            /// a must be greater than 1
            /// </summary>
            /// <param name="a"></param>
            /// <returns></returns>
            public double Gamma(double a)
            {
                if (a <= 1)
                {
                    throw new ArgumentException("Argument a must be greater than 1");
                }
                // according to Knuth's book
                // The average number of times step is performed is < 1.902 when a ≥ 3.
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

            /// <summary>
            /// Returns double from Beta distribution
            /// </summary>
            /// <param name="a">must be positive</param>
            /// <param name="b">must be positive</param>
            /// <returns></returns>
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
            /// Returns double from Chi-Squared distribution
            /// </summary>
            /// <param name="v">degree of freedom, must be grater than or equal to 1</param>
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
