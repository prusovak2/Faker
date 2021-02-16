using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Exporters;
using Faker;

namespace FakerBenchmark
{


    //public class WithoutIgnoreIgnoreFaker : IgnoreFaker<WithoutIgnore> { }

    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class IgnoreBenchmarks
    {
        /*[Benchmark(Baseline =true)]
        public void WithIgnoreScan()
        {
            new WithIgnoreFaker(true);
        }

        [Benchmark]
        public void WithoutIgnoreScan()
        {
            new WithoutIgnoreFaker(true);
        }

        [Benchmark]
        public void WithoutIgnoreNOScan()
        {
            new WithoutIgnoreFaker(false);
        }*/
       /* [Benchmark]
        public void WithIgnoreBaseFaker()
        {
            // no scanning is carried out, class contains FakerIgnore attributes
            new WithIgnoreBaseFaker();
        }

        [Benchmark]
        public void WithIgnoreIgnoreFaker()
        {
            //scanning is carried out, class contains FakerIgnore attributes
            new WithIgnoreIgnoreFaker();
        }

        [Benchmark]
        public void WithoutIgnoreBaseFaker()
        {
            // no scanning is carried out, class doesn't contain FakerIgnore attributes
            new WithoutIgnoreBaseFaker();
        }

        [Benchmark]
        public void WithoutIgnoreIgnoreFaker()
        {
            //scanning is carried out, class doesn't contain FakerIgnore attributes
            new WithoutIgnoreIgnoreFaker();
        }*/
    }
}
