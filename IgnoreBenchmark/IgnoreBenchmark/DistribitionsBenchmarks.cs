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
        public void NormalZiggurat()
        {
            rg.Distribution.Normal();
        }

        [Benchmark]
        public void ExponentialZiggurat()
        {
            rg.Distribution.Exponential();
        }

        [Benchmark]
        public void ExponentialStrightforward()
        {
            rg.Distribution.Exponential(5);
        }
    }
}
