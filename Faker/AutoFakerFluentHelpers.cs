using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    //Conditional Rule Helpers AutoFaker
    public partial class AutoFaker<TClass> : BaseFaker<TClass>
    {
        public class ConditionalMemberAutoFluent<TFirstMember, TCurMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal ConditionalMemberAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }

            public ConditionalRuleAutoFluent<TFirstMember> SetRule(Func<RandomGenerator, TCurMember> setter)
            {
                return FakerInstance._setRule<TFirstMember, TCurMember>(setter);
            }

            public ConditionalRuleAutoFluent<TFirstMember> Ignore()
            {
                return FakerInstance._autoIgnore<TFirstMember>();
            }

            public static explicit operator LastConditionalMemberAutoFluent<TFirstMember, TCurMember>(ConditionalMemberAutoFluent<TFirstMember, TCurMember> original)
            {
                return new LastConditionalMemberAutoFluent<TFirstMember, TCurMember>(original.FakerInstance);
            }
        }

        public class ConditionalRuleAutoFluent<TFirstMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal ConditionalRuleAutoFluent(AutoFaker<TClass> faker)
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
        }

        public class ConditionAutoFluent<TFirtsMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal ConditionAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }

            //Type of a member to be filled changes with another call to For
            public ConditionalMemberAutoFluent<TFirtsMember, TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
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
            public LastConditionalMemberAutoFluent<TFirtsMember, TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
            {
                MemberInfo memberInfo = BaseFaker<TClass>.GetMemberFromExpression(selector);
                return (LastConditionalMemberAutoFluent<TFirtsMember, TAnotherMember>)FakerInstance._for<TFirtsMember, TAnotherMember>(memberInfo);
            }
        }

        public class LastConditionalMemberAutoFluent<TFirstMember, TCurMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal LastConditionalMemberAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            public void SetRule(Func<RandomGenerator, TCurMember> setter)
            {
                FakerInstance._setRule<TFirstMember, TCurMember>(setter);
            }
            public void Ignore()
            {
                FakerInstance._autoIgnore<TFirstMember>();
            }
        }
    }
}
