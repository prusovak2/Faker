using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    public partial class RandomGenerator
    {
        /// <summary>
        /// Represents user defined discrete distribution
        /// immutable
        /// </summary>
        /// <typeparam name="TLabel"></typeparam>
        public class DiscreteDistribution<TLabel>
        {
            /// <summary>
            /// The probabilities with which random events described by this distribution occurs <br/>
            /// read only
            /// </summary>
            public IReadOnlyList<double> Probabilities {
                get { return Array.AsReadOnly(_probabilities); }
            }
            private double[] _probabilities;
            /// <summary>
            /// Random events (possible outcomes of this distribution) <br/>
            /// read only
            /// </summary>
            public IReadOnlyList<TLabel> Labels
            {
                get { return Array.AsReadOnly(_labels); }
            }
            private TLabel[] _labels;
            /// <summary>
            /// number of the possible outcomes
            /// </summary>
            public int Count => _probabilities.Length;
            /// <summary>
            /// source of Random numbers used when DiscreteDistribution is used directly by user (not via RandomGenerator instance) <br/>
            /// when DiscreteDistribution is used via RandomGenerator instance, RandomGenerator's source of randomness is passed to the discreteDistribution.Next method instead 
            /// </summary>
            internal Lazy<Xoshiro256starstar> generatorAlgLazy = new Lazy<Xoshiro256starstar>();
            /// <summary>
            /// Creates new custom discrete distribution with given labels and their probabilities
            /// </summary>
            /// <param name="probabilities"></param>
            /// <param name="labels"></param>
            public DiscreteDistribution(double[] probabilities, TLabel[] labels)
            {
                if(probabilities.Length != labels.Length)
                {
                    throw new ArgumentException("Labels and probabilities arrays must have an equal length.");
                }
                this._probabilities = probabilities;
                this._labels = labels;

                this.NomalizeProbabilities();
            } 
            /// <summary>
            /// Normalizes probabilities of this instance of DiscreteDistribution so that their sum is 1
            /// </summary>
            internal void NomalizeProbabilities()
            {
                if(this._probabilities.Length == 0)
                {
                    throw new ArgumentException("Array of probabilities must be non empty to be normalized");
                }

                // add up all probabilities
                double sum = 0;
                for (int i = 0; i < this._probabilities.Length; i++)
                {
                    sum += this._probabilities[i];
                }

                if (sum.EpsilonEquals(1))
                {
                    return;
                }

                // almost zero sum of probabilities, assign probabilities evenly
                if (sum.EpsilonEquals(0))
                {
                    double prob = 1.0 / this._probabilities.Length;
                    for (int i = 0; i < this._probabilities.Length; i++)
                    {
                        this._probabilities[i] = prob;
                    }
                    return;
                }

                double factor = 1 / sum;
                for (int i = 0; i < this._probabilities.Length; i++)
                {
                    this._probabilities[i] *= factor;
                }
            }
            /// <summary>
            /// Randomly selects one of the labels accordingly to given probabilities
            /// </summary>
            /// <param name="generatorAlg"></param>
            /// <returns></returns>
            internal TLabel Next(IRandomGeneratorAlg generatorAlg)
            {
                double random = generatorAlg.Next01Double();
                double sum = 0.0;
                int lastNonZero = -1;

                for (int i = 0; i < this._probabilities.Length; i++)
                {
                    sum += this._probabilities[i];
                    if (sum > random)
                    {
                        return _labels[i];
                    }
                    if (_probabilities[i] != 0)
                    {
                        lastNonZero = i;
                    }
                }

                // some issue with imprecise floating point arithmetics may cause that sum of probabilities is less than one
                if(lastNonZero >= 0)
                {
                    return _labels[lastNonZero];
                }

                //should no be reached, distribution should be normalized
                throw new InvalidOperationException("Only zero probabilities contained in probability array.");
            }
            /// <summary>
            /// Randomly selects one of the labels accordingly to given probabilities
            /// </summary>
            /// <returns></returns>
            public TLabel Next()
            {
                return Next(this.generatorAlgLazy.Value);
            }
            /// <summary>
            /// Returns a new instance of DiscreteDistribution that is the copy of this instance but does not contain given label <br/>
            /// probabilities are normalized after the label is removed
            /// </summary>
            /// <param name="label"></param>
            /// <returns></returns>
            public DiscreteDistribution<TLabel> WithoutLabel(TLabel label)
            {
                int labelIndex = Array.IndexOf(this._labels, label);
                if(labelIndex < 0)
                {
                    throw new ArgumentException("Non existing label");
                }
                double[] newProbs = new double[this._probabilities.Length - 1];
                TLabel[] newLabels = new TLabel[this._labels.Length - 1];

                for (int i = 0; i < labelIndex; i++)
                {
                    newProbs[i] = this._probabilities[i];
                    newLabels[i] = this._labels[i];
                }
                for (int oldIdx = labelIndex+1, newIdx = labelIndex; oldIdx < _probabilities.Length; oldIdx++, newIdx++)
                {
                    newProbs[newIdx] = _probabilities[oldIdx];
                    newLabels[newIdx] = _labels[oldIdx];
                }

                // ctor will normalize probabilities
                return new DiscreteDistribution<TLabel>(newProbs, newLabels);
            }
            /// <summary>
            /// Returns a new instance of DiscreteDistribution that is the copy of this instance but contains one more additional label <br/>
            /// probabilities are normalized after the label is added
            /// </summary>
            /// <param name="label"></param>
            /// <param name="probability"></param>
            /// <returns></returns>
            public DiscreteDistribution<TLabel> WithLabel(TLabel label, double probability)
            {
                int labelIndex = Array.IndexOf(this._labels, label);
                if (labelIndex != -1)
                {
                    throw new ArgumentException("Label is already present in this distribution.");
                }
                double[] newProbs = new double[this._probabilities.Length + 1];
                TLabel[] newLabels = new TLabel[this._labels.Length + 1];
                for (int i = 0; i < _labels.Length; i++)
                {
                    newProbs[i] = _probabilities[i];
                    newLabels[i] = _labels[i];
                }
                newProbs[newProbs.Length - 1] = probability;
                newLabels[newLabels.Length - 1] = label;

                // ctor will normalize probabilities
                return new DiscreteDistribution<TLabel>(newProbs, newLabels);
            }
            /// <summary>
            /// returns true if the probabilities of this instance are normalized (which should be always true) <br/>
            /// just to test whether things are working as supposed
            /// </summary>
            /// <param name="ProbSum"></param>
            /// <returns></returns>
            internal bool AreNormalized(out double ProbSum)
            {
                if (this._probabilities.Length == 0)
                {
                    throw new ArgumentException("Array of probabilities must be non empty.");
                }

                // add up all probabilities
                double sum = 0;
                for (int i = 0; i < this._probabilities.Length; i++)
                {
                    sum += this._probabilities[i];
                }
                ProbSum = sum;
                return sum.EpsilonEquals(1d);
            }
        }
    }
}
