using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    //Set Rule Helpers AutoFaker
    public partial class AutoFaker<TClass> : BaseFaker<TClass>
    {
        public class MemberAutoFluent<TFirstMember, TCurMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal MemberAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }

            public RuleAutoFluent<TFirstMember> SetRule(Func<RandomGenerator, TCurMember> setter)
            {
                return FakerInstance._setRule<TFirstMember, TCurMember>(setter);
            }

            public RuleAutoFluent<TFirstMember> Ignore()
            {
                return FakerInstance._chainedIgnore<TFirstMember>();
            }

            public static explicit operator LastMemberAutoFluent<TFirstMember, TCurMember>(MemberAutoFluent<TFirstMember, TCurMember> original)
            {
                return new LastMemberAutoFluent<TFirstMember, TCurMember>(original.FakerInstance);
            }
        }

        public class RuleAutoFluent<TFirstMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal RuleAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            public ConditionAutoFluent<TFirstMember> When(Func<TFirstMember, bool> condition)
            {
                return FakerInstance._when(condition);
            }
            public LastConditionAutoFluent<TFirstMember> Otherwise()
            {
                return (LastConditionAutoFluent<TFirstMember>)FakerInstance._otherwise<TFirstMember>();
            }
            //Type of a member to be filled changes with another call to For
            public MemberAutoFluent<TFirstMember, TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
            {
                MemberInfo memberInfo = BaseFaker<TClass>.GetMemberFromExpression(selector);
                return FakerInstance._for<TFirstMember, TAnotherMember>(memberInfo);
            }

            public static explicit operator FirstRuleAutoFluent<TFirstMember>(RuleAutoFluent<TFirstMember> original)
            {
                return new FirstRuleAutoFluent<TFirstMember>(original.FakerInstance);
            }

            public static explicit operator LastConditionAutoFluent<TFirstMember>(RuleAutoFluent<TFirstMember> original)
            {
                return new LastConditionAutoFluent<TFirstMember>(original.FakerInstance);
            }
        }

        

        public class ConditionAutoFluent<TFirtsMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal ConditionAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }

            //Type of a member to be filled changes with another call to For
            public MemberAutoFluent<TFirtsMember, TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
            {
                MemberInfo memberInfo = BaseFaker<TClass>.GetMemberFromExpression(selector);
                return FakerInstance._for<TFirtsMember, TAnotherMember>(memberInfo);
            }

            public static explicit operator LastConditionAutoFluent<TFirtsMember>(ConditionAutoFluent<TFirtsMember> original)
            {
                return new LastConditionAutoFluent<TFirtsMember>(original.FakerInstance);
            }
        }
        public class LastConditionAutoFluent<TFirtsMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal LastConditionAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }

            //Type of a member to be filled changes with another call to For
            public LastMemberAutoFluent<TFirtsMember, TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
            {
                MemberInfo memberInfo = BaseFaker<TClass>.GetMemberFromExpression(selector);
                return (LastMemberAutoFluent<TFirtsMember, TAnotherMember>)FakerInstance._for<TFirtsMember, TAnotherMember>(memberInfo);
            }
        }

        public class LastMemberAutoFluent<TFirstMember, TCurMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal LastMemberAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            public LastConditionAutoFluent<TFirstMember> SetRule(Func<RandomGenerator, TCurMember> setter)
            {
                return (LastConditionAutoFluent<TFirstMember>)FakerInstance._setRule<TFirstMember, TCurMember>(setter);
            }
            public LastConditionAutoFluent<TFirstMember> Ignore()
            {
                return (LastConditionAutoFluent<TFirstMember>)FakerInstance._chainedIgnore<TFirstMember>();
            }
        }
        public class FirstMemberAutoFluent<TFirstMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal FirstMemberAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }

            public FirstRuleAutoFluent<TFirstMember> SetRule(Func<RandomGenerator, TFirstMember> setter)
            {
                return (FirstRuleAutoFluent<TFirstMember>)FakerInstance._firtsSetRule<TFirstMember>(setter);
            }
        }

        public class FirstRuleAutoFluent<TFirstMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal FirstRuleAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            public ConditionAutoFluent<TFirstMember> When(Func<TFirstMember, bool> condition)
            {
                return FakerInstance._when(condition);
            }
        }
    }
}
