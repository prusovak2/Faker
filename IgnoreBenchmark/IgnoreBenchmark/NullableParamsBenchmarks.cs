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
    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class NullableParamsCompoundBenchmark
    {
        public RandomGenerator random;

        [GlobalSetup]
        public void GlobalSetup()
        {
            random = new RandomGenerator();
        }

        public void DoubleNullableCompoundTest()
        {
            random.Random.DoubleNullableParams();
            random.Random.DoubleNullableParams(upper: -1);
            random.Random.DoubleNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void DoubleNullableCompound()
        {
            DoubleNullableCompoundTest();
        }

        public void DecimalNullableCompoundTest()
        {
            random.Random.DecimalNullableParams();
            random.Random.DecimalNullableParams(upper: -1);
            random.Random.DecimalNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void DecimalNullableCompound()
        {
            DecimalNullableCompoundTest();
        }

        public void IntNullableCompoundTest()
        {
            random.Random.IntNullableParams();
            random.Random.IntNullableParams(upper: -1);
            random.Random.IntNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void IntNullableCompound()
        {
            IntNullableCompoundTest();
        }

        public void LongNullableCompoundTest()
        {
            random.Random.LongNullableParams();
            random.Random.LongNullableParams(upper: -1);
            random.Random.LongNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void LongNullableCompound()
        {
            LongNullableCompoundTest();
        }

        public void DoubleCompoundTest()
        {
            random.Random.Double();
            random.Random.Double(upper: -1);
            random.Random.Double(-1000000, 1000000);
        }

        [Benchmark]
        public void DoubleCompound()
        {
            DoubleCompoundTest();
        }

        public void DecimalCompoundTest()
        {
            random.Random.Decimal();
            random.Random.Decimal(upper: -1);
            random.Random.Decimal(-1000000, 1000000);
        }

        [Benchmark]
        public void DecimalCompound()
        {
            DecimalCompoundTest();
        }

        public void IntCompoundTest()
        {
            random.Random.Int();
            random.Random.Int(upper: -1);
            random.Random.Int(-1000000, 1000000);
        }

        [Benchmark]
        public void IntCompound()
        {
            IntCompoundTest();
        }

        public void LongCompoundTest()
        {
            random.Random.Long();
            random.Random.Long(upper: -1);
            random.Random.Long(-1000000, 1000000);
        }

        [Benchmark]
        public void LongCompound()
        {
            LongCompoundTest();
        }
    }


    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class NullableParamsBasicBenchmarks
    {
        public RandomGenerator random;

        [GlobalSetup]
        public void GlobalSetup()
        {
            random = new RandomGenerator();
        }

        [Benchmark]
        public void DoubleNullableParamless()
        {
            random.Random.DoubleNullableParams();
        }

        [Benchmark]
        public void DoubleNullableOneParam()
        {
            random.Random.DoubleNullableParams(upper:-1) ;
        }

        [Benchmark]
        public void DoubleNullableTwoParams()
        {
            random.Random.DoubleNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void DecimalNullableParamless()
        {
            random.Random.DecimalNullableParams();
        }

        [Benchmark]
        public void DecimalNullableOneParam()
        {
            random.Random.DecimalNullableParams(upper: -1);
        }

        [Benchmark]
        public void DecimalNullableTwoParams()
        {
            random.Random.DecimalNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void IntNullableParamless()
        {
            random.Random.IntNullableParams();
        }

        [Benchmark]
        public void IntNullableOneParam()
        {
            random.Random.IntNullableParams(upper: -1);
        }

        [Benchmark]
        public void IntNullableTwoParams()
        {
            random.Random.IntNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void LongNullableParamless()
        {
            random.Random.LongNullableParams();
        }

        [Benchmark]
        public void LongNullableOneParam()
        {
            random.Random.LongNullableParams(upper: -1);
        }

        [Benchmark]
        public void LongNullableTwoParams()
        {
            random.Random.LongNullableParams(-1000000, 1000000);
        }

        [Benchmark]
        public void DoubleParamless()
        {
            random.Random.Double();
        }

        [Benchmark]
        public void DoubleOneParam()
        {
            random.Random.Double(upper: -1);
        }

        [Benchmark]
        public void DoubleTwoParams()
        {
            random.Random.Double(-1000000, 1000000);
        }

        [Benchmark]
        public void DecimalParamless()
        {
            random.Random.Decimal();
        }

        [Benchmark]
        public void DecimalOneParam()
        {
            random.Random.Decimal(upper: -1);
        }

        [Benchmark]
        public void DecimalTwoParams()
        {
            random.Random.Decimal(-1000000, 1000000);
        }

        [Benchmark]
        public void IntParamless()
        {
            random.Random.Int();
        }

        [Benchmark]
        public void IntOneParam()
        {
            random.Random.Int(upper: -1);
        }

        [Benchmark]
        public void IntTwoParams()
        {
            random.Random.Int(-1000000, 1000000);
        }

        [Benchmark]
        public void LongParamless()
        {
            random.Random.LongNullableParams();
        }

        [Benchmark]
        public void LongOneParam()
        {
            random.Random.Long(upper: -1);
        }

        [Benchmark]
        public void LongTwoParams()
        {
            random.Random.Long(-1000000, 1000000);
        }
    }


}
