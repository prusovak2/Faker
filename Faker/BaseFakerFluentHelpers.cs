using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Faker
{
    public partial class BaseFaker<TClass> : IFaker where TClass : class
    {

//Set Faker helpers
        public class RefMemberFluent<TInnerClass> where TInnerClass : class
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal RefMemberFluent(BaseFaker<TClass>  faker)
            {
                this.FakerInstance = faker;
            }

            public void Faker(BaseFaker<TInnerClass> faker)
            {
                this.FakerInstance._faker(faker);
            }
        }

//Unconditional Rule Helpers
        public class UnconditionalMemberFluent<TMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal UnconditionalMemberFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }

            public void Rule(Func<RandomGenerator, TMember> setter)
            {
                this.FakerInstance._uncoditionalRule<TMember>(setter);
            }
        }
//Conditional Rule Helpers
        public class ConditionalMemberFluent<TFirstMember, TCurMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal ConditionalMemberFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }

            public ConditionalRuleFluent<TFirstMember> SetRule(Func<RandomGenerator, TCurMember> setter)
            {
                return FakerInstance._setRule<TFirstMember, TCurMember>(setter);
            }

            public static explicit operator LastConditionalMemberFluent<TFirstMember, TCurMember>(ConditionalMemberFluent<TFirstMember, TCurMember> original)
            {
                return new LastConditionalMemberFluent<TFirstMember, TCurMember>(original.FakerInstance);
            }
        }

        public class ConditionalRuleFluent<TFirstMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal ConditionalRuleFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            public ConditionFluent<TFirstMember> When(Func<TFirstMember, bool> condition)
            {
                return FakerInstance._when(condition);
            }
            public LastConditionFluent<TFirstMember> Otherwise()
            {
                return (LastConditionFluent<TFirstMember>)FakerInstance._otherwise<TFirstMember>();
            }
        }

        public class ConditionFluent<TFirtsMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal ConditionFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            
            //Type of a member to be filled changes with another call to For
            public ConditionalMemberFluent<TFirtsMember,TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
            {
                MemberInfo memberInfo = BaseFaker<TClass>.GetMemberFromExpression(selector);
                return FakerInstance._for<TFirtsMember, TAnotherMember>(memberInfo);
            }

            public static explicit operator LastConditionFluent<TFirtsMember>(ConditionFluent<TFirtsMember> original)
            {
                return new LastConditionFluent<TFirtsMember>(original.FakerInstance);
            }
        }
        public class LastConditionFluent<TFirtsMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal LastConditionFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }

            //Type of a member to be filled changes with another call to For
            public LastConditionalMemberFluent<TFirtsMember, TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
            {
                MemberInfo memberInfo = BaseFaker<TClass>.GetMemberFromExpression(selector);
                return (LastConditionalMemberFluent<TFirtsMember, TAnotherMember>)FakerInstance._for<TFirtsMember, TAnotherMember>(memberInfo);
            }
        }

        public class LastConditionalMemberFluent<TFirstMember, TCurMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal LastConditionalMemberFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            public void SetRule(Func<RandomGenerator, TCurMember> setter)
            {
                FakerInstance._setRule<TFirstMember, TCurMember>(setter);
            }
        }
    }

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
