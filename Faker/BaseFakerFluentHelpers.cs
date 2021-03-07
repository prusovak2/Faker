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
            public ConditionFluent<TFirstMember> Otherwise()
            {
                return FakerInstance._otherwise<TFirstMember>();
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
        }
    }
}
