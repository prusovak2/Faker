using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Faker;

namespace FakerBenchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class DistribitionsBenchmarks
    {
        RandomGenerator rg = new();

        [Benchmark]
        public void Normal()
        {
            rg.Distribution.Normal();
        }

        [Benchmark]
        public void Exponential()
        {
            rg.Distribution.Exponential();
        }

        [Benchmark]
        public void Bernoulli()
        {
            rg.Distribution.Bernoulli(0.3);
        }

        [Benchmark]
        public void Geometric()
        {
            rg.Distribution.Geometric(0.3);
        }

        [Benchmark]
        public void Binomial()
        {
            rg.Distribution.Binomial(40, 0.3);
        }

        [Benchmark]
        public void BinomialNaive()
        {
            rg.Distribution.BinomialNaive(40, 0.3);
        }
    }
}
