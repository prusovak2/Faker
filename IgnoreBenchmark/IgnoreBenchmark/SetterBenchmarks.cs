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
    public class SetterBenchmarks
    {
        private LotOfMembers lotOfMembers;
        private LotOfMembersFaker lotOfMembersFaker;
        private OneMember oneMember;
        private OneMemberFaker oneMemberFaker;
        private TwoMember twoMember;
        private TwoMemberFaker twoMemberFaker;
        private NestedClass nestedClass;
        private NestedClassFaker nestedClassFaker;

        [GlobalSetup]
        public void GlobalSetup()
        {
            Console.WriteLine("setup started");
            lotOfMembers = new();
            lotOfMembersFaker = new();
            oneMember = new();
            oneMemberFaker = new();
            twoMember = new();
            twoMemberFaker = new();
            nestedClass = new();
            nestedClassFaker = new();
            Console.WriteLine("setup done");
        }

        [Benchmark]
        public void LotOfMemebers()
        {
            lotOfMembers = lotOfMembersFaker.Generate();
        }
        [Benchmark]
        public void OneMember()
        {
            oneMember = oneMemberFaker.Populate(oneMember);
        }
        [Benchmark]
        public void TwoMembers()
        {
            twoMember = twoMemberFaker.Populate(twoMember);
        }
        [Benchmark]
        public void Nested()
        {
            nestedClass = nestedClassFaker.Populate(nestedClass);
        }
    }

    public class LotOfMembers
    {
        public int Int;
        public byte Byte;
        public short Short { get; set; }
        public DateTime DateTime { get; set; }
        public double Double;
        public Guid Guid;
        public string String { get; set; } = "IGNORED";
        public int IgnoredInt = 42;

    }
    public class OneMember
    {
        public int Int { get; set; }
    }

    public class TwoMember
    {
        public int Int { get; set; }
        public byte Byte;
    }
    public class InnerClass
    {
        public int InnerInt { get; set; }
    }

    public class NestedClass
    {
        public InnerClass Inner { get; set; }
        public byte OuterByte;
    }
    public class LotOfMembersFaker : BaseFaker<LotOfMembers>
    {
        public LotOfMembersFaker()
        {
            RuleFor(x => x.Int, rg => rg.Random.Int(upper: 42));
            RuleFor(x => x.Byte, rg => rg.Random.Byte());
            RuleFor(x => x.Short, rg => rg.Random.Short());
            RuleFor(x => x.DateTime, rg => rg.Random.DateTime());
            RuleFor(x => x.Double, rg => rg.Random.Double());
            RuleFor(x => x.Guid, rg => rg.Random.Guid());
            RuleFor(x => x.String, rg => rg.Random.String());
            RuleFor(x => x.IgnoredInt, rg => rg.Random.Int());
        }
    }    

    public class OneMemberFaker : BaseFaker<OneMember>
    {
        public OneMemberFaker()
        {
            RuleFor(x => x.Int, rg => rg.Random.Int(upper: 42));
        }
    }
    public class TwoMemberFaker : BaseFaker<TwoMember>
    {
        public TwoMemberFaker()
        {
            RuleFor(x => x.Int, rg => rg.Random.Int(upper:42));
            RuleFor(x => x.Byte, rg => rg.Random.Byte());
        }
    }

    public class InnerClassFaker : BaseFaker<InnerClass>
    {
        public InnerClassFaker()
        {
            RuleFor(x => x.InnerInt, rg => rg.Random.Int(upper: 42));
        }
    } 
    public class NestedClassFaker : BaseFaker<NestedClass>
    {
        public NestedClassFaker()
        {
            SetFaker(x => x.Inner, new InnerClassFaker());
            RuleFor(x => x.OuterByte, rg => rg.Random.Byte());
        }
    }

}
