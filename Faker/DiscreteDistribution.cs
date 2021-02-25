using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    public partial class RandomGenerator
    {
        public class DiscreteDistribution<TLabel>
        {
            public double[] Probabilities { get; }

            public TLabel[] Labels { get; }

            public int Count => Probabilities.Length;

            internal Lazy<Xoshiro256starstar> generatorAlgLazy = new Lazy<Xoshiro256starstar>();

            public DiscreteDistribution(double[] probabilities, TLabel[] labels)
            {
                if(probabilities.Length != labels.Length)
                {
                    throw new ArgumentException("Labels and probabilities arrays must have an equal length.");
                }
                this.Probabilities = probabilities;
                this.Labels = labels;
                this.NomalizeProbabilities();
            } 

            internal void NomalizeProbabilities()
            {
                if(this.Probabilities.Length == 0)
                {
                    throw new ArgumentException("Array of probabilities must be non empty to be normalized");
                }

                // add up all probabilities
                double sum = 0;
                for (int i = 0; i < this.Probabilities.Length; i++)
                {
                    sum += this.Probabilities[i];
                }

                if (sum.EpsilonEquals(1))
                {
                    return;
                }

                // almost zero sum of probabilities, assign probabilities evenly
                if (sum.EpsilonEquals(0))
                {
                    double prob = 1.0 / this.Probabilities.Length;
                    for (int i = 0; i < this.Probabilities.Length; i++)
                    {
                        this.Probabilities[i] = prob;
                    }
                    return;
                }

                double factor = 1 / sum;
                for (int i = 0; i < this.Probabilities.Length; i++)
                {
                    this.Probabilities[i] *= factor;
                }
            }

            internal TLabel Next(IRandomGeneratorAlg generatorAlg)
            {
                double random = generatorAlg.Next01Double();
                double sum = 0.0;
                int lastNonZero = -1;

                for (int i = 0; i < this.Probabilities.Length; i++)
                {
                    sum += this.Probabilities[i];
                    if (sum > random)
                    {
                        return Labels[i];
                    }
                    if (Probabilities[i] != 0)
                    {
                        lastNonZero = i;
                    }
                }

                // some issue with imprecise floating point arithmetics may cause that sum of probabilities is less than one
                if(lastNonZero >= 0)
                {
                    return Labels[lastNonZero];
                }

                //should no be reached, distribution should be normalized
                throw new InvalidOperationException("Only zero probabilities contained in probability array.");
            }

            public TLabel Next()
            {
                return Next(this.generatorAlgLazy.Value);
            }

            public DiscreteDistribution<TLabel> WithoutLabel(TLabel label)
            {
                int labelIndex = Array.IndexOf(this.Labels, label);
                if(labelIndex < 0)
                {
                    throw new ArgumentException("Non existing label");
                }
                double[] newProbs = new double[this.Probabilities.Length - 1];
                TLabel[] newLabels = new TLabel[this.Labels.Length - 1];

                for (int i = 0; i < labelIndex; i++)
                {
                    newProbs[i] = this.Probabilities[i];
                    newLabels[i] = this.Labels[i];
                }
                for (int oldIdx = labelIndex+1, newIdx = labelIndex; oldIdx < Probabilities.Length; oldIdx++, newIdx++)
                {
                    newProbs[newIdx] = Probabilities[oldIdx];
                    newLabels[newIdx] = Labels[oldIdx];
                }

                // ctor will normalize probabilities
                return new DiscreteDistribution<TLabel>(newProbs, newLabels);
            }

            public DiscreteDistribution<TLabel> WithLabel(TLabel label, double probability)
            {
                int labelIndex = Array.IndexOf(this.Labels, label);
                if (labelIndex != -1)
                {
                    throw new ArgumentException("Label is already present in this distribution.");
                }
                double[] newProbs = new double[this.Probabilities.Length + 1];
                TLabel[] newLabels = new TLabel[this.Labels.Length + 1];
                for (int i = 0; i < Labels.Length; i++)
                {
                    newProbs[i] = Probabilities[i];
                    newLabels[i] = Labels[i];
                }
                newProbs[newProbs.Length - 1] = probability;
                newLabels[newLabels.Length - 1] = label;

                // ctor will normalize probabilities
                return new DiscreteDistribution<TLabel>(newProbs, newLabels);
            }

            internal bool AreNormalized(out double ProbSum)
            {
                if (this.Probabilities.Length == 0)
                {
                    throw new ArgumentException("Array of probabilities must be non empty.");
                }

                // add up all probabilities
                double sum = 0;
                for (int i = 0; i < this.Probabilities.Length; i++)
                {
                    sum += this.Probabilities[i];
                }
                ProbSum = sum;
                return sum.EpsilonEquals(1d);
            }
        }
    }
}
