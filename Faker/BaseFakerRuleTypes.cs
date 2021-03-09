using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    public partial class BaseFaker<TClass> : IFaker where TClass : class
    {
        internal abstract class RuleType { }

        internal class SimpleRule : RuleType
        {
            public SimpleRule(Func<object> randFunc)
            {
                this.Rule = randFunc;
            }
            public Func<object> Rule { get; }
        }

        internal abstract class ChainedRule : RuleType
        {
            public abstract void ResolveChainedRule(TClass instance, BaseFaker<TClass> faker);
        }

        internal class ChainedRuleResolver<TFirstMember> : ChainedRule
        {
            public ChainedRuleResolver(MemberInfo member)
            {
                this.AddFirstPack(member);
            }

            public TFirstMember ConditionValue { get; set; } //generation state

            public List<RulePack<TFirstMember>> ChainedRuleParts { get; } = new List<RulePack<TFirstMember>>();

            public bool UsedRule { get; private set; } = false; // generation state

            public override void ResolveChainedRule(TClass instance, BaseFaker<TClass> faker)
            {
                //first leading rule - unconditional, provides value to evaluate conditions with
                RulePack<TFirstMember> leadingRulePack = this.ChainedRuleParts[0];
                //set value that will be considered in following conditions and use rule
                this.ConditionValue = faker.UseRule<TFirstMember>(instance, leadingRulePack.MemberInfo, leadingRulePack.RandomFunc);
                for (int i = 1; i < this.ChainedRuleParts.Count; i++)
                {
                    RulePack<TFirstMember> curRule = this.ChainedRuleParts[i];
                    if ((!curRule.Ignore) && curRule.Condition(this.ConditionValue))  //evaluate condition
                    {
                        faker.UseRule(instance, curRule.MemberInfo, curRule.RandomFunc);
                        this.UsedRule = true; //some rule in this chain was used, otherwise branch is not gonna be carried out 
                    }
                    //else ignore this rule
                }
                this.ClearState(); //reset state so that this Resolver can be reused by another ._populate call 
            }
            private void ClearState()
            {
                this.ConditionValue = default;
                this.UsedRule = false;
            }

            /// <summary>
            /// Called from BaseFaker.For
            /// </summary>
            /// <param name="member"></param>
            private void AddFirstPack(MemberInfo member)
            {
                RulePack<TFirstMember> rulePack = new RulePack<TFirstMember>(_ => true, member);
                this.ChainedRuleParts.Add(rulePack);
            }
            /// <summary>
            /// Called from .When
            /// </summary>
            /// <param name="condition"></param>
            public void AddNewRulePackWithCondition(Func<TFirstMember, bool> condition)
            {
                RulePack<TFirstMember> rulePack = new RulePack<TFirstMember>(condition);
                this.ChainedRuleParts.Add(rulePack);
            }
            /// <summary>
            /// Called from .Otherwise
            /// </summary>
            public void AddNewRulePackWithOtherwiseCondition()
            {
                RulePack<TFirstMember> rulePack = new RulePack<TFirstMember>( _ => !this.UsedRule);
                this.ChainedRuleParts.Add(rulePack);
            }
            /// <summary>
            /// Called from ConditionFluent._for
            /// </summary>
            /// <param name="member"></param>
            public void AddMemberToLastRulePack(MemberInfo member)
            {
                this.ChainedRuleParts[ChainedRuleParts.Count - 1].AddMember(member);
            }
            /// <summary>
            /// Called from SetRule
            /// </summary>
            /// <param name="func"></param>
            public void AddFunctionToLastRulePack(Func<object> func)
            {
                this.ChainedRuleParts[ChainedRuleParts.Count - 1].AddFunction(func);
            }
            /// <summary>
            /// Called from AutoFaker._autoIgnore
            /// </summary>
            public void SetLastRulePackIgnored()
            {
                this.ChainedRuleParts[ChainedRuleParts.Count - 1].SetIgnored();
            }
            public MemberInfo GetLastMember()
            {
                return this.ChainedRuleParts[ChainedRuleParts.Count - 1].MemberInfo;
            }
        }
        internal class RulePack<TFirstMember>
        {
            public Func<TFirstMember, bool> Condition { get; }
            public MemberInfo MemberInfo { get; private set; }
            public Func<object> RandomFunc { get; private set; }
            public bool Ignore { get; private set; } = false;

            public RulePack(Func<TFirstMember, bool> condition)
            {
                this.Condition = condition;
                RandomFunc = null;
                MemberInfo = null;
            }

            public RulePack(Func<TFirstMember, bool> condition, MemberInfo memberInfo)
            {
                this.Condition = condition;
                this.MemberInfo = memberInfo;
                this.RandomFunc = null;
            }

            public void AddMember(MemberInfo member)
            {
                this.MemberInfo = member;
            }

            public void AddFunction(Func<object> func)
            {
                this.RandomFunc = func;
            }
            
            public void SetIgnored()
            {
                this.Ignore = true;
            }
        }
    }  
}
