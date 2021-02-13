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
    public class WithIgnore
    {
        public int Int;
        public byte Byte;
        [FakerIgnore]
        public short Short { get; set; } = 73;
        public DateTime DateTime { get; set; }
        [FakerIgnore]
        public double Double = 42.73;
        public Guid Guid;
        [FakerIgnore]
        public string IgnoredString { get; set; } = "IGNORED";
        [FakerIgnore]
        public int IgnoredInt = 42;
    }

    public class WithIgnoreBaseFaker : BaseFaker<WithIgnore> { }

    //public class WithIgnoreIgnoreFaker : IgnoreFaker<WithIgnore> { }


    public class WithoutIgnore
    {
        public int Int;
        public byte Byte;
        public short Short { get; set; } = 73;
        public DateTime DateTime { get; set; }
        public double Double = 42.73;
        public Guid Guid;
        public string IgnoredString { get; set; } = "IGNORED";
        public int IgnoredInt = 42;
    }

    public class WithoutIgnoreBaseFaker : BaseFaker<WithoutIgnore> { }

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
