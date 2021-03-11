using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    public partial class AutoFaker<TClass> : BaseFaker<TClass>
    {
        /// <summary>
        /// SetRule fluent AutoFaker helper
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in the rule chain</typeparam>
        /// <typeparam name="TCurMember">Type of current member</typeparam>
        public class MemberAutoFluent<TFirstMember, TCurMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal MemberAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Sets a Rule for the selected member <br/>
            /// Rule is used to generate a random contend of this member
            /// </summary>
            /// <param name="setter">function to be used to fill a member</param>
            /// <returns>fluent syntax helper</returns>
            public RuleAutoFluent<TFirstMember> SetRule(Func<RandomGenerator, TCurMember> setter)
            {
                return FakerInstance._setRule<TFirstMember, TCurMember>(setter);
            }
            /// <summary>
            /// Sets the selected member ignored <br/>
            /// Member is not to be filled by default random function
            /// </summary>
            /// <returns>fluent syntax helper</returns>
            public RuleAutoFluent<TFirstMember> Ignore()
            {
                return FakerInstance._chainedIgnore<TFirstMember>();
            }

            public static explicit operator LastMemberAutoFluent<TFirstMember, TCurMember>(MemberAutoFluent<TFirstMember, TCurMember> original)
            {
                return new LastMemberAutoFluent<TFirstMember, TCurMember>(original.FakerInstance);
            }
        }
        /// <summary>
        /// SetRule fluent AutoFaker helper
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in the rule chain</typeparam>
        public class RuleAutoFluent<TFirstMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal RuleAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Sets an condition, following rules are applied only if the condition is satisfied <br/>
            /// Random value generated while applying the first rule of this rule chain is used while evaluating the condition 
            /// </summary>
            /// <param name="condition"></param>
            /// <returns>fluent syntax helper</returns>
            public ConditionAutoFluent<TFirstMember> When(Func<TFirstMember, bool> condition)
            {
                return FakerInstance._when(condition);
            }
            /// <summary>
            /// Sets an Otherwise condition <br/>
            /// It is satisfied if and only if any of .When conditions preceding this .Otherwise has not been satisfied
            /// </summary>
            /// <returns>fluent syntax helper</returns>
            public LastConditionAutoFluent<TFirstMember> Otherwise()
            {
                return (LastConditionAutoFluent<TFirstMember>)FakerInstance._otherwise<TFirstMember>();
            }
            /// <summary>
            /// Selects a member of TClass to have a conditional rule set for it
            /// </summary>
            /// <typeparam name="TAnotherMember">Type of member</typeparam>
            /// <param name="selector">lambda returning a member</param>
            /// <returns>Fluent syntax helper</returns>
            public MemberAutoFluent<TFirstMember, TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
            {
                //Type of a member to be filled changes with another call to For
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


        /// <summary>
        /// SetRule fluent AutoFaker helper
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in the rule chain</typeparam>
        public class ConditionAutoFluent<TFirtsMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal ConditionAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Selects a member of TClass to have a conditional rule set for it
            /// </summary>
            /// <typeparam name="TAnotherMember">Type of member</typeparam>
            /// <param name="selector">lambda returning a member</param>
            /// <returns>Fluent syntax helper</returns>
            public MemberAutoFluent<TFirtsMember, TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
            {
                //Type of a member to be filled changes with another call to For
                MemberInfo memberInfo = BaseFaker<TClass>.GetMemberFromExpression(selector);
                return FakerInstance._for<TFirtsMember, TAnotherMember>(memberInfo);
            }

            public static explicit operator LastConditionAutoFluent<TFirtsMember>(ConditionAutoFluent<TFirtsMember> original)
            {
                return new LastConditionAutoFluent<TFirtsMember>(original.FakerInstance);
            }
        }
        /// <summary>
        /// SetRule fluent AutoFaker helper
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in the rule chain</typeparam>
        public class LastConditionAutoFluent<TFirtsMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal LastConditionAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Selects a member of TClass to have a conditional rule set for it
            /// </summary>
            /// <typeparam name="TAnotherMember">Type of member</typeparam>
            /// <param name="selector">lambda returning a member</param>
            /// <returns>Fluent syntax helper</returns>
            public LastMemberAutoFluent<TFirtsMember, TAnotherMember> For<TAnotherMember>(Expression<Func<TClass, TAnotherMember>> selector)
            {
                //Type of a member to be filled changes with another call to For
                MemberInfo memberInfo = BaseFaker<TClass>.GetMemberFromExpression(selector);
                return (LastMemberAutoFluent<TFirtsMember, TAnotherMember>)FakerInstance._for<TFirtsMember, TAnotherMember>(memberInfo);
            }
        }
        /// <summary>
        /// SetRule fluent AutoFaker helper
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in the rule chain</typeparam>
        /// <typeparam name="TCurMember">Type of current member</typeparam>
        public class LastMemberAutoFluent<TFirstMember, TCurMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal LastMemberAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Sets a Rule for the selected member <br/>
            /// Rule is used to generate a random contend of this member
            /// </summary>
            /// <param name="setter">function to be used to fill a member</param>
            /// <returns>fluent syntax helper</returns>
            public LastConditionAutoFluent<TFirstMember> SetRule(Func<RandomGenerator, TCurMember> setter)
            {
                return (LastConditionAutoFluent<TFirstMember>)FakerInstance._setRule<TFirstMember, TCurMember>(setter);
            }
            /// <summary>
            /// Sets the selected member ignored <br/>
            /// Member is not to be filled by default random function
            /// </summary>
            /// <returns>fluent syntax helper</returns>
            public LastConditionAutoFluent<TFirstMember> Ignore()
            {
                return (LastConditionAutoFluent<TFirstMember>)FakerInstance._chainedIgnore<TFirstMember>();
            }
        }
        /// <summary>
        /// SetRule fluent AutoFaker helper
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in the rule chain</typeparam>
        public class FirstMemberAutoFluent<TFirstMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal FirstMemberAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Sets unconditional rule for a member 
            /// </summary>
            /// <param name="setter"></param>
            /// <returns>fluent syntax helper</returns>
            public FirstRuleAutoFluent<TFirstMember> SetRule(Func<RandomGenerator, TFirstMember> setter)
            {
                return (FirstRuleAutoFluent<TFirstMember>)FakerInstance._firtsSetRule<TFirstMember>(setter);
            }
        }
        /// <summary>
        /// SetRule fluent AutoFaker helper
        /// </summary>
        /// <typeparam name="TFirstMember">Type of the first member in the rule chain</typeparam>
        public class FirstRuleAutoFluent<TFirstMember>
        {
            private AutoFaker<TClass> FakerInstance { get; }

            internal FirstRuleAutoFluent(AutoFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }
            /// <summary>
            /// Sets an condition, following rules are applied only if the condition is satisfied <br/>
            /// Random value generated while applying the first rule of this rule chain is used while evaluating the condition 
            /// </summary>
            /// <param name="condition"></param>
            /// <returns>fluent syntax helper</returns>
            public ConditionAutoFluent<TFirstMember> When(Func<TFirstMember, bool> condition)
            {
                return FakerInstance._when(condition);
            }
        }
    }
}
