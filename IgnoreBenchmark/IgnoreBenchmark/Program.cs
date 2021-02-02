using System;
using Faker;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Exporters;

namespace IgnoreBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<IgnoreBenchmarks>();
        }
    }
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

    public class WithIgnoreFaker : BaseFaker<WithIgnore>
    {
        public WithIgnoreFaker(bool scan) :base(scan)
        {

        }
    }

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

    public class WithoutIgnoreFaker : BaseFaker<WithIgnore>
    {
        public WithoutIgnoreFaker(bool scan) : base(scan)
        {

        }
    }

    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class IgnoreBenchmarks
    {
        [Benchmark(Baseline =true)]
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
        }
    }
}
