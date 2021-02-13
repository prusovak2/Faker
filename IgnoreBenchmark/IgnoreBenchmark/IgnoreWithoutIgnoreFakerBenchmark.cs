using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Faker;


namespace FakerBenchmark
{
    

    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class IgnoreWithoutIgnoreFakerBenchmark
    {
        public static void WithoutIgnoreAttributes()
        {
            WithoutIgnoreBaseFaker faker = new WithoutIgnoreBaseFaker();
            WithoutIgnore wi = faker.Generate();
            wi = faker.Generate();
        }
        public static void WithoutIgnoreAttributesRulefor()
        {
            WithoutIgnoreBaseFaker faker = new WithoutIgnoreBaseFaker();
            WithoutIgnore wi = faker.Generate();
            faker.RuleFor(x => x.Int, _ => 42);
            wi = faker.Generate();
        }


        public static void WithIgnoreAttributes()
        {
            WithIgnoreBaseFaker faker = new WithIgnoreBaseFaker();
            WithIgnore wi = faker.Generate();
            wi = faker.Generate();
        }

        [Benchmark]
        public void ScanningBaseFakerNoATTRs()
        {
            WithoutIgnoreAttributes();
        }
        [Benchmark]
        public void ScanningBaseFakerATTRs()
        {
            WithIgnoreAttributes();
        }
        [Benchmark]
        public void RuleForBetweenGenerate()
        {
            WithoutIgnoreAttributesRulefor();
        }
    }
}
