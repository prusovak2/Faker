using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Faker
{
    public partial class BaseFaker<TClass> : IInnerFaker where TClass : class
    {

//Set Faker helpers
        public class RefMemberFluent<TInnerClass> where TInnerClass : class
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal RefMemberFluent(BaseFaker<TClass>  faker)
            {
                this.FakerInstance = faker;
            }

            public void As(BaseFaker<TInnerClass> faker)
            {
                this.FakerInstance._faker(faker);
            }
        }

//Set Rule Helpers
        public class MemberFluent<TFirstMember, TCurMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal MemberFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }

            public RuleFluent<TFirstMember> SetRule(Func<RandomGenerator, TCurMember> setter)
            {
                return FakerInstance._setRule<TFirstMember, TCurMember>(setter);
            }

            public static explicit operator LastMemberFluent<TFirstMember, TCurMember>(MemberFluent<TFirstMember, TCurMember> original)
            {
                return new LastMemberFluent<TFirstMember, TCurMember>(original.FakerInstance);
            }
        }

        public class RuleFluent<TFirstMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal RuleFluent(BaseFaker<TClass> faker)
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
            //Type of a member to be filled changes with another call to For
            public MemberFluent<TFirstMember, TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
            {
                MemberInfo memberInfo = BaseFaker<TClass>.GetMemberFromExpression(selector);
                return FakerInstance._for<TFirstMember, TAnotherMember>(memberInfo);
            }
            public static explicit operator LastConditionFluent<TFirstMember>(RuleFluent<TFirstMember> original)
            {
                return new LastConditionFluent<TFirstMember>(original.FakerInstance);
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
            public MemberFluent<TFirtsMember,TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
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
            public LastMemberFluent<TFirtsMember, TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
            {
                MemberInfo memberInfo = BaseFaker<TClass>.GetMemberFromExpression(selector);
                return (LastMemberFluent<TFirtsMember, TAnotherMember>)FakerInstance._for<TFirtsMember, TAnotherMember>(memberInfo);
            }
        }

        public class LastMemberFluent<TFirstMember, TCurMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal LastMemberFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            public LastConditionFluent<TFirstMember> SetRule(Func<RandomGenerator, TCurMember> setter)
            {
                return (LastConditionFluent<TFirstMember>)FakerInstance._setRule<TFirstMember, TCurMember>(setter);
            }
        }

        public class FirstMemberFluent<TFirstMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal FirstMemberFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }

            public FirstRuleFluent<TFirstMember> SetRule(Func<RandomGenerator, TFirstMember> setter)
            {
                return (FirstRuleFluent<TFirstMember>)FakerInstance._firtsSetRule<TFirstMember>(setter);
            }
        }

        public class FirstRuleFluent<TFirstMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal FirstRuleFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            public ConditionFluent<TFirstMember> When(Func<TFirstMember, bool> condition)
            {
                return FakerInstance._when(condition);
            }
        }
    }

 
}
