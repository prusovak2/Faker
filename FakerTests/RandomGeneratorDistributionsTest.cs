using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;

namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorDistributionsTest
    {
        [TestMethod]
        public void NormalTest()
        {
            RandomGenerator rg = new();
            for (int i = 0; i < 100; i++)
            {
                double d = rg.Distribution.Normal();
                Console.WriteLine(d);
            }
        }

        [TestMethod]
        public void BernoulliTest()
        {
            RandomGenerator rg = new();

            int numIterations = 1000;
            int trueCounter = 0;
            int falseCounter = 0;
            for (int i = 0; i < numIterations; i++)
            {
                bool b = rg.Distribution.Bernoulli(0.5);
                if (b)
                {
                    trueCounter++;
                }
                else
                {
                    falseCounter++;
                }
            }

            // may potentially fail even for working Bernoulli distribution
            Assert.IsTrue(Math.Abs(trueCounter - falseCounter) < (numIterations / 10));
            Console.WriteLine($"True: {trueCounter}");
            Console.WriteLine($"False: {falseCounter}");

            for (int i = 0; i < 20; i++)
            {
                bool b = rg.Distribution.Bernoulli(1);
                Console.WriteLine(b);
                Assert.IsTrue(b);
            }

            for (int i = 0; i < 20; i++)
            {
                bool b = rg.Distribution.Bernoulli(0);
                Assert.IsFalse(b);
            }
        }

        [TestMethod]
        public void BernoulliIntTest()
        {
            RandomGenerator rg = new();

            int numIterations = 1000;
            int counter = 0;
            for (int i = 0; i < numIterations; i++)
            {
                int x = rg.Distribution.BernoulliInt(0.5);
                counter += x;
            }

            // may potentially fail even for working Bernoulli distribution
            Console.WriteLine($"trueCount: {counter}");
            Assert.IsTrue(Math.Abs(counter - numIterations/2) < (numIterations / 10));

            for (int i = 0; i < 20; i++)
            {
                int x = rg.Distribution.BernoulliInt(1);
                Console.WriteLine(i);
                Assert.IsTrue(x==1);
            }

            for (int i = 0; i < 20; i++)
            {
                int x = rg.Distribution.BernoulliInt(0);
                Assert.IsTrue(x==0);
            }
        }

        [TestMethod]
        public void DiscreteNormalizeProbsTest()
        {
            double[] probs = { 0.5, 1, 5, 10 };
            int[] labels = { 1, 2, 3, 4 };

            RandomGenerator.DiscreteDistribution<int> dist = new(probs, labels);

            bool normalized = dist.AreNormalized(out double sum);
            Console.WriteLine(sum);
            foreach (var item in dist.Probabilities)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            Assert.IsTrue(normalized);
            Assert.IsTrue(sum.EpsilonEquals(1));

            double[] probs2 = { 10, 100, 5, 10 };

            dist = new(probs2, labels);

            normalized = dist.AreNormalized(out sum);
            Console.WriteLine(sum);
            foreach (var item in dist.Probabilities)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            Assert.IsTrue(normalized);
            Assert.IsTrue(sum.EpsilonEquals(1));

            double[] probs3 = { 1, 2, 3, 4, 5, 6 };
            int[] labels2 = { 1, 2, 3, 4, 5, 6 };

            dist = new(probs3, labels2);

            normalized = dist.AreNormalized(out sum);
            Console.WriteLine(sum);
            foreach (var item in dist.Probabilities)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            Assert.IsTrue(normalized);
            Assert.IsTrue(sum.EpsilonEquals(1));

            double[] probs4 = { 0.2, 0.4 };
            string[] labels3 = { "abraka", "dabra" };

            RandomGenerator.DiscreteDistribution<string> dist2 = new(probs4, labels3);

            normalized = dist2.AreNormalized(out sum);
            Console.WriteLine(sum);
            foreach (var item in dist2.Probabilities)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            Assert.IsTrue(normalized);
            Assert.IsTrue(sum.EpsilonEquals(1));

            // already normalized
            double[] probs5 = { 0.2, 0.4, 0.2, 0.2  };
            string[] labels4 = { "abraka", "dabra", "label1", "label2" };

            dist2 = new(probs5, labels4);

            normalized = dist2.AreNormalized(out sum);
            Console.WriteLine(sum);
            foreach (var item in dist2.Probabilities)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            Assert.IsTrue(normalized);
            Assert.IsTrue(sum.EpsilonEquals(1));

            // all zeros
            double[] probs6 = { 0, 0, 0, 0 };
            int[] labels5 = { 1, 2, 3, 4 };
            dist = new(probs6, labels5);
            normalized = dist.AreNormalized(out sum);
            Console.WriteLine(sum);
            foreach (var item in dist.Probabilities)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            Assert.IsTrue(normalized);
            Assert.IsTrue(sum.EpsilonEquals(1));

            // lengths of arrays differ
            Assert.ThrowsException<ArgumentException>(() => { dist = new(probs4, labels2); });
        }

        [TestMethod]
        public void DiscreteWithoutLabelTest()
        {
            double[] probs = { 1.3, 0, 2.3, 4.5 };
            byte[] labels = { 1, 2, 42, 4 };
            RandomGenerator.DiscreteDistribution<byte> dist = new(probs, labels);
            RandomGenerator.DiscreteDistribution<byte> distWithout = dist.WithoutLabel(42);
            bool normalized = distWithout.AreNormalized(out double sum);
            Console.WriteLine(sum);
            foreach (var item in distWithout.Probabilities)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            foreach (var item in distWithout.Labels)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            Assert.IsTrue(normalized);
            Assert.IsTrue(sum.EpsilonEquals(1));
            Assert.IsTrue((dist.Probabilities.Length - 1) == (distWithout.Probabilities.Length));
            Assert.IsTrue(Array.IndexOf<byte>(distWithout.Labels, 42) == -1);
            

            double[] probs2 = { 0.2, 0.4 };
            string[] labels2 = { "abraka", "dabra" };
            RandomGenerator.DiscreteDistribution<string> dist2 = new(probs2, labels2);
            RandomGenerator.DiscreteDistribution<string> dist2Without = dist2.WithoutLabel("abraka");
            normalized = dist2Without.AreNormalized(out sum);
            Console.WriteLine(sum);
            foreach (var item in dist2Without.Probabilities)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            foreach (var item in dist2Without.Labels)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            Assert.IsTrue(normalized);
            Assert.IsTrue(sum.EpsilonEquals(1));
            Assert.IsTrue((dist2.Probabilities.Length - 1) == (dist2Without.Probabilities.Length));
            Assert.IsTrue(Array.IndexOf<string>(dist2Without.Labels, "abraka") == -1);



            double[] probs3 = { 133211, 197129, 42000 };
            string[] labels3 = { "abraka", "dabra", "remove" };
            dist2 = new(probs3, labels3);
            dist2Without = dist2.WithoutLabel("remove");
            normalized = dist2Without.AreNormalized(out sum);
            Console.WriteLine(sum);
            foreach (var item in dist2Without.Probabilities)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            foreach (var item in dist2Without.Labels)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            Assert.IsTrue(normalized);
            Assert.IsTrue(sum.EpsilonEquals(1));
            Assert.IsTrue((dist2.Probabilities.Length - 1) == (dist2Without.Probabilities.Length));
            Assert.IsTrue(Array.IndexOf<string>(dist2Without.Labels, "remove") == -1);


            Assert.ThrowsException<ArgumentException>(() => { dist2.WithoutLabel("non existent label"); });
        }

        [TestMethod]
        public void DiscreteWithLabelTest()
        {
            double[] probs = { 1.3, 0, 2.3, 4.5 };
            byte[] labels = { 1, 2, 3, 4 };
            RandomGenerator.DiscreteDistribution<byte> dist = new(probs, labels);

            Assert.AreEqual(probs.Length, dist.Count);

            RandomGenerator.DiscreteDistribution<byte> distWith = dist.WithLabel(42, 0.5);
            bool normalized = distWith.AreNormalized(out double sum);
            Console.WriteLine(sum);
            foreach (var item in distWith.Probabilities)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            foreach (var item in distWith.Labels)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            Assert.IsTrue(normalized);
            Assert.IsTrue(sum.EpsilonEquals(1));
            Assert.IsTrue((dist.Probabilities.Length + 1) == (distWith.Probabilities.Length));
            Assert.IsTrue(Array.IndexOf<byte>(distWith.Labels, 42) != -1);


            double[] probs2 = { 0.2, 0.4 };
            string[] labels2 = { "abraka", "dabra" };
            RandomGenerator.DiscreteDistribution<string> dist2 = new(probs2, labels2);
            RandomGenerator.DiscreteDistribution<string> dist2With = dist2.WithLabel("added", 0.2);
            normalized = dist2With.AreNormalized(out sum);
            Console.WriteLine(sum);
            foreach (var item in dist2With.Probabilities)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            foreach (var item in dist2With.Labels)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            Assert.IsTrue(normalized);
            Assert.IsTrue(sum.EpsilonEquals(1));
            Assert.IsTrue((dist2.Probabilities.Length + 1) == (dist2With.Probabilities.Length));
            Assert.IsTrue(Array.IndexOf<string>(dist2With.Labels, "added") != -1);



            double[] probs3 = { 133211, 197129, 42000 };
            string[] labels3 = { "abraka", "dabra", "label" };
            dist2 = new(probs3, labels3);
            dist2With = dist2.WithLabel("added", 233342);
            normalized = dist2With.AreNormalized(out sum);
            Console.WriteLine(sum);
            foreach (var item in dist2With.Probabilities)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            foreach (var item in dist2With.Labels)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            Assert.IsTrue(normalized);
            Assert.IsTrue(sum.EpsilonEquals(1));
            Assert.IsTrue((dist2.Probabilities.Length + 1) == (dist2With.Probabilities.Length));
            Assert.IsTrue(Array.IndexOf<string>(dist2With.Labels, "added") != -1);


            Assert.ThrowsException<ArgumentException>(() => { dist2.WithLabel("abraka", 73); });
        }

        [TestMethod]
        public void DiscreteNextTest()
        {
            double[] probs = { 0.5, 0.1, 0.2, 0.2 };
            string[] labels= { "half", "0.1", "0.2", "0.2 second" };
            RandomGenerator.DiscreteDistribution<string> dist = new(probs, labels);
            Dictionary<string, int> counter = new();

            int numIterations = 1000;
            for (int i = 0; i < numIterations; i++)
            {
                string label = dist.Next();
                TestUtils.IncInDic(counter, label);
            }
            for (int i = 0; i < dist.Probabilities.Length; i++)
            {
                TestUtils.CheckPropotion(counter, numIterations, dist.Probabilities[i], dist.Labels[i]);
            }
        
            Console.WriteLine();
            double[] probs2 = { 1, 0 };
            string[] labels2 = { "sure", "never" };
            RandomGenerator.DiscreteDistribution<string> dist2 = new(probs2, labels2);
            for (int i = 0; i < 30; i++)
            {
                string label = dist2.Next();
                Assert.AreEqual("sure", label);
            }

            Console.WriteLine("********** originally zeros**********");
            double[] probs3= { 0, 0, 0, 0};
            int[] labels3 = { 1, 2, 3, 4};
            Dictionary<int, int> counter2 = new();
            RandomGenerator.DiscreteDistribution<int> dist3 = new(probs3, labels3);
            for (int i = 0; i < numIterations; i++)
            {
                int label = dist3.Next();
                TestUtils.IncInDic(counter2, label);
            }
            for (int i = 0; i < dist3.Probabilities.Length; i++)
            {
                TestUtils.CheckPropotion(counter2, numIterations, dist3.Probabilities[i], dist3.Labels[i]);
            }
        }

        [TestMethod]
        public void DiscreteViaRandomGeneratortest()
        {
            RandomGenerator rg = new();
            double[] probs = { 0.5, 0.1, 0.2, 0.2 };
            string[] labels = { "half", "0.1", "0.2", "0.2 second" };

            Dictionary<string, int> counter = new();
            int numIterations = 1000;

            for (int i = 0; i < numIterations; i++)
            {
                string label = rg.Distribution.Discrete(probs, labels);
                TestUtils.IncInDic(counter, label);
            }
            for (int i = 0; i < probs.Length; i++)
            {
                TestUtils.CheckPropotion(counter, numIterations, probs[i], labels[i]);
            }

            Console.WriteLine();
            counter.Clear();
            RandomGenerator.DiscreteDistribution<string> dist = new(probs, labels); 
            for (int i = 0; i < numIterations; i++)
            {
                string label = rg.Distribution.Discrete(dist);
                TestUtils.IncInDic(counter, label);
            }
            for (int i = 0; i < dist.Probabilities.Length; i++)
            {
                TestUtils.CheckPropotion(counter, numIterations, dist.Probabilities[i], dist.Labels[i]);
            }
            
        }
    }
}
