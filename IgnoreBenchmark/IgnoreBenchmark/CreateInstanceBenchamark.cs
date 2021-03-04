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

    public class EmptyClass 
    {
        public EmptyClass() { }
        public EmptyClass(int val) { }

    }

    public class EmptyClassFaker : BaseFaker<EmptyClass> { }

    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class CreateInstanceBenchamark
    {
        private EmptyClass emptyClass { get; set; }

        private EmptyClassFaker faker { get; set; }
        [GlobalSetup]
        public void GlobalSetup()
        {
            emptyClass = new();
            faker = new();
        }

        [Benchmark]
        public void Populate()
        {
            emptyClass = new();
            emptyClass = faker.Populate(emptyClass);
        }
        [Benchmark]
        public void GenerateParamless()
        {
            emptyClass = faker.Generate();
        }
        [Benchmark]
        public void GenerateParam()
        {
            emptyClass = faker.Generate(42);
        }
    }
}
