using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
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
    public class WithIgnoreAutoFaker : AutoFaker<WithIgnore> { }
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
    public class WithoutIgnoreAutoFaker : AutoFaker<WithoutIgnore> { }
    
    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class IgnoreWithoutIgnoreFakerBenchmark
    {
        public void BaseWith()
        {
            WithIgnoreBaseFaker faker = new();
            WithIgnore x = faker.Generate();
            x = faker.Generate();
        }
        public void BaseWithout()
        {
            WithoutIgnoreBaseFaker faker = new();
            WithoutIgnore x = faker.Generate();
            x = faker.Generate();
        }
        public void BaseWithRuleFor()
        {
            WithIgnoreBaseFaker faker = new();
            WithIgnore x = faker.Generate();
            faker.For(a => a.Byte).SetRule( _ => 42);
            x = faker.Generate();
        }
        public void AutoWith()
        {
            WithIgnoreAutoFaker faker = new();
            WithIgnore x = faker.Generate();
            x = faker.Generate();
        }
        public void AutoWithout()
        {
            WithoutIgnoreAutoFaker faker = new();
            WithoutIgnore x = faker.Generate();
            x = faker.Generate();
        }
        public void AutoWithRuleFor()
        {
            WithIgnoreAutoFaker faker = new();
            WithIgnore x = faker.Generate();
            faker.For(a => a.Byte).SetRule( _ => 42);
            x = faker.Generate();
        }
        [Benchmark]
        public void BaseFakerAttributes()
        {
            BaseWith();
        }
        [Benchmark]
        public void BaseFakerNoAttributes()
        {
            BaseWithout();
        }
        [Benchmark]
        public void BaseFakerAttributesRuleFor()
        {
            BaseWithRuleFor();
        }
        [Benchmark]
        public void AutoFakerAttributes()
        {
            AutoWith();
        }
        [Benchmark]
        public void AutoFakerNoAttributes()
        {
            AutoWithout();
        }
        [Benchmark]
        public void AutoFakerAttributesRuleFor()
        {
            AutoWithRuleFor();
        }
    }
}
