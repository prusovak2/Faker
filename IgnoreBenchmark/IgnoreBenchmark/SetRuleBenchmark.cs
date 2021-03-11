using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Exporters;
using Faker;


namespace FakerBenchmark
{
    public class LotOfMembersSetRuleFoFaker : BaseFaker<LotOfMembers>
    {
        public LotOfMembersSetRuleFoFaker()
        {
            SetRuleFor(x => x.Int).As(rg => rg.Random.Int(upper: 42));
            SetRuleFor(x => x.Byte).As(rg => rg.Random.Byte());
            SetRuleFor(x => x.Short).As(rg => rg.Random.Short());
            SetRuleFor(x => x.DateTime).As(rg => rg.Random.DateTime());
            SetRuleFor(x => x.Double).As(rg => rg.Random.Double());
            SetRuleFor(x => x.Guid).As(rg => rg.Random.Guid());
            SetRuleFor(x => x.String).As(rg => rg.Random.String());
            SetRuleFor(x => x.IgnoredInt).As(rg => rg.Random.Int());
        }
    }

    public class OneMemberSetRuleForFaker : BaseFaker<OneMember>
    {
        public OneMemberSetRuleForFaker()
        {
            SetRuleFor(x => x.Int).As(rg => rg.Random.Int(upper: 42));
        }
    }

    public class LotOfMembersForSetRuleFaker : BaseFaker<LotOfMembers>
    {
        public LotOfMembersForSetRuleFaker()
        {
            For(x => x.Int).SetRule(rg => rg.Random.Int(upper: 42));
            For(x => x.Byte).SetRule(rg => rg.Random.Byte());
            For(x => x.Short).SetRule(rg => rg.Random.Short());
            For(x => x.DateTime).SetRule(rg => rg.Random.DateTime());
            For(x => x.Double).SetRule(rg => rg.Random.Double());
            For(x => x.Guid).SetRule(rg => rg.Random.Guid());
            For(x => x.String).SetRule(rg => rg.Random.String());
            For(x => x.IgnoredInt).SetRule(rg => rg.Random.Int());
        }
    }

    public class OneMemberForSetRuleFaker : BaseFaker<OneMember>
    {
        public OneMemberForSetRuleFaker()
        {
            For(x => x.Int).SetRule(rg => rg.Random.Int(upper: 42));
        }
    }

    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class OneMemberCtorBenchmark
    {
        [Benchmark]
        public void RuleForCtorOneMember()
        {
            OneMemberRuleForFaker f = new();
        }
        
        [Benchmark]
        public void SetRuleForCtorOneMember()
        {
            OneMemberSetRuleForFaker f = new();
        }

        [Benchmark]
        public void ForSetRuleCtorOneMember()
        {
            OneMemberForSetRuleFaker f = new();
        }
    }

    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class LotOfMembersCtorBenchmark
    {
        [Benchmark]
        public void RuleForCtorLotOfMembers()
        {
            LotOfMembersRuleForFaker f = new();
        }

        [Benchmark]
        public void SetRuleForCtorLotOfMembers()
        {
            LotOfMembersSetRuleFoFaker f = new();
        }

        [Benchmark]
        public void ForSetRuleCtorLotOfMembers()
        {
            LotOfMembersForSetRuleFaker f = new();
        }
    }

    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class OneMemberGenerateBenchmark
    {
        private OneMemberRuleForFaker RuleForFaker;
        private OneMemberSetRuleForFaker SetRuleForFaker;
        private OneMemberForSetRuleFaker ForSetRuleFaker;

        [GlobalSetup]
        public void GlobalSetup()
        {
            RuleForFaker = new();
            SetRuleForFaker = new();
            ForSetRuleFaker = new();
        }

        [Benchmark]
        public void RuleForGenerateOneMember()
        {
            OneMember om = RuleForFaker.Generate();
        }

        [Benchmark]
        public void SetRuleForGenerateOneMember()
        {
            OneMember om = SetRuleForFaker.Generate();
        }

        [Benchmark]
        public void ForSetRuleGenerateOneMember()
        {
            OneMember om = ForSetRuleFaker.Generate();
        }
    }


    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class LotOfMembersGenerateBenchmark
    {
        private LotOfMembersRuleForFaker RuleForFaker;
        private LotOfMembersSetRuleFoFaker SetRuleForFaker;
        private LotOfMembersForSetRuleFaker ForSetRuleFaker;

        [GlobalSetup]
        public void GlobalSetup()
        {
            RuleForFaker = new();
            SetRuleForFaker = new();
            ForSetRuleFaker = new();
        }

        [Benchmark]
        public void RuleForGenerateLotOfMembers()
        {
            LotOfMembers om = RuleForFaker.Generate();
        }

        [Benchmark]
        public void SetRuleForGenerateLotOfMembers()
        {
            LotOfMembers om = SetRuleForFaker.Generate();
        }

        [Benchmark]
        public void ForSetRuleGenerateLotOfMembers()
        {
            LotOfMembers om = ForSetRuleFaker.Generate();
        }
    }
}
