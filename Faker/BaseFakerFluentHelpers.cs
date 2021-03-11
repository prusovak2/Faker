using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Faker
{
    public partial class BaseFaker<TClass> : IInnerFaker where TClass : class
    {

        /// <summary>
        /// SetFaker fluent helper
        /// </summary>
        public class RefMemberFluent<TInnerClass> where TInnerClass : class
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal RefMemberFluent(BaseFaker<TClass>  faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Set inner faker for selected member <br/>
            /// .Generate and .Populate calls recursively .Generate or .Populate on all inner fakers of given Faker 
            /// </summary>
            /// <param name="faker">inner faker</param>
            public void As(BaseFaker<TInnerClass> faker)
            {
                this.FakerInstance._faker(faker);
            }
        }

        /// <summary>
        /// SetRule fluent helper
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in the rule chain</typeparam>
        /// <typeparam name="TCurMember">Type of current member</typeparam>
        public class MemberFluent<TFirstMember, TCurMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal MemberFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Sets a Rule for a selected member <br/>
            /// Rule is used to generate a random contend of this member
            /// </summary>
            /// <param name="setter">function to be used to fill a member</param>
            /// <returns>fluent syntax helper</returns>
            public RuleFluent<TFirstMember> SetRule(Func<RandomGenerator, TCurMember> setter)
            {
                return FakerInstance._setRule<TFirstMember, TCurMember>(setter);
            }
            
            public static explicit operator LastMemberFluent<TFirstMember, TCurMember>(MemberFluent<TFirstMember, TCurMember> original)
            {
                return new LastMemberFluent<TFirstMember, TCurMember>(original.FakerInstance);
            }
        }
        /// <summary>
        /// SetRule fluent helper
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in the rule chain</typeparam>
        public class RuleFluent<TFirstMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal RuleFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Sets an condition, following rules are applied only if the condition is satisfied <br/>
            /// Random value generated while applying the first rule of this rule chain is used while evaluating the condition 
            /// </summary>
            /// <param name="condition"></param>
            /// <returns>fluent syntax helper</returns>
            public ConditionFluent<TFirstMember> When(Func<TFirstMember, bool> condition)
            {
                return FakerInstance._when(condition);
            }
            /// <summary>
            /// Sets an Otherwise condition <br/>
            /// It is satisfied if and only if any of .When conditions preceding this .Otherwise has not been satisfied
            /// </summary>
            /// <returns>fluent syntax helper</returns>
            public LastConditionFluent<TFirstMember> Otherwise()
            {
                return (LastConditionFluent<TFirstMember>)FakerInstance._otherwise<TFirstMember>();
            }
            /// <summary>
            /// Selects a member of TClass to have a conditional rule set for it
            /// </summary>
            /// <typeparam name="TAnotherMember">Type of member</typeparam>
            /// <param name="selector">lambda returning a member</param>
            /// <returns>Fluent syntax helper</returns>
            public MemberFluent<TFirstMember, TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
            {
                //Type of a member to be filled changes with another call to For
                MemberInfo memberInfo = BaseFaker<TClass>.GetMemberFromExpression(selector);
                return FakerInstance._for<TFirstMember, TAnotherMember>(memberInfo);
            }
            public static explicit operator LastConditionFluent<TFirstMember>(RuleFluent<TFirstMember> original)
            {
                return new LastConditionFluent<TFirstMember>(original.FakerInstance);
            }
        }
        /// <summary>
        /// SetRule fluent helper
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in the rule chain</typeparam>
        public class ConditionFluent<TFirstMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal ConditionFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Selects a member of TClass to have a conditional rule set for it
            /// </summary>
            /// <typeparam name="TAnotherMember">Type of member</typeparam>
            /// <param name="selector">lambda returning a member</param>
            /// <returns>Fluent syntax helper</returns>
            public MemberFluent<TFirstMember,TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
            {
                //Type of a member to be filled changes with another call to For
                MemberInfo memberInfo = BaseFaker<TClass>.GetMemberFromExpression(selector);
                return FakerInstance._for<TFirstMember, TAnotherMember>(memberInfo);
            }

            public static explicit operator LastConditionFluent<TFirstMember>(ConditionFluent<TFirstMember> original)
            {
                return new LastConditionFluent<TFirstMember>(original.FakerInstance);
            }
        }
        /// <summary>
        /// SetRule fluent helper
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in the rule chain</typeparam>
        public class LastConditionFluent<TFirstMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal LastConditionFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Selects a member of TClass to have a conditional rule set for it
            /// </summary>
            /// <typeparam name="TAnotherMember">Type of member</typeparam>
            /// <param name="selector">lambda returning a member</param>
            /// <returns>Fluent syntax helper</returns>
            public LastMemberFluent<TFirstMember, TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
            {
                //Type of a member to be filled changes with another call to For
                MemberInfo memberInfo = BaseFaker<TClass>.GetMemberFromExpression(selector);
                return (LastMemberFluent<TFirstMember, TAnotherMember>)FakerInstance._for<TFirstMember, TAnotherMember>(memberInfo);
            }
        }
        /// <summary>
        /// SetRule fluent helper
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in the rule chain</typeparam>
        /// <typeparam name="TCurMember">Type of current member</typeparam>
        public class LastMemberFluent<TFirstMember, TCurMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal LastMemberFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Sets a conditional rule for a member
            /// </summary>
            /// <param name="setter">function to be used to fill a member</param>
            /// <returns>fluent syntax helper</returns>
            /// <returns></returns>
            public LastConditionFluent<TFirstMember> SetRule(Func<RandomGenerator, TCurMember> setter)
            {
                return (LastConditionFluent<TFirstMember>)FakerInstance._setRule<TFirstMember, TCurMember>(setter);
            }
        }
        /// <summary>
        /// SetRule fluent helper
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in the rule chain</typeparam>
        public class FirstMemberFluent<TFirstMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal FirstMemberFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Sets unconditional rule for a member 
            /// </summary>
            /// <param name="setter"></param>
            /// <returns>fluent syntax helper</returns>
            public FirstRuleFluent<TFirstMember> SetRule(Func<RandomGenerator, TFirstMember> setter)
            {
                return (FirstRuleFluent<TFirstMember>)FakerInstance._firtsSetRule<TFirstMember>(setter);
            }
        }
        /// <summary>
        /// SetRule fluent helper
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in the rule chain</typeparam>
        public class FirstRuleFluent<TFirstMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal FirstRuleFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Sets an condition, following rules are applied only if the condition is satisfied <br/>
            /// Random value generated while applying the first rule of this rule chain is used while evaluating the condition 
            /// </summary>
            /// <param name="condition"></param>
            /// <returns>fluent syntax helper</returns>
            public ConditionFluent<TFirstMember> When(Func<TFirstMember, bool> condition)
            {
                return FakerInstance._when(condition);
            }
        }
    }
}
