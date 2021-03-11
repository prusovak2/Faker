using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    public partial class BaseFaker<TClass> : IInnerFaker where TClass : class
    {
        internal abstract class ChainedRule 
        {
            public abstract void ResolveChainedRule(TClass instance, BaseFaker<TClass> faker);
        }

        internal class ChainedRuleResolver<TFirstMember> : ChainedRule
        {
            public ChainedRuleResolver(MemberInfo member)
            {
                this.AddFirstRule(member);
            }
            public MemberInfo FirstMember { get; private set; }

            public Func<object> FirstFunc { get; private set; }

            public TFirstMember ConditionValue { get; set; } //generation state

            public List<ConditionPack<TFirstMember>> ChainedRuleParts { get; } = new List<ConditionPack<TFirstMember>>();

            public bool UsedRule { get; private set; } = false; // generation state

            public override void ResolveChainedRule(TClass instance, BaseFaker<TClass> faker)
            {
                //first leading rule - unconditional, provides value to evaluate conditions with
                //ConditionPack<TFirstMember> leadingRulePack = this.ChainedRuleParts[0];
                //set value that will be considered in following conditions and use rule
                this.ConditionValue = faker.UseRule<TFirstMember>(instance, FirstMember, FirstFunc);
                /*for (int i = 1; i < this.ChainedRuleParts.Count; i++)
                {
                    ConditionPack<TFirstMember> curRule = this.ChainedRuleParts[i];
                    if ((!curRule.Ignore) && curRule.Condition(this.ConditionValue))  //evaluate condition
                    {
                        faker.UseRule(instance, curRule.MemberInfo, curRule.RandomFunc);
                        this.UsedRule = true; //some rule in this chain was used, otherwise branch is not gonna be carried out 
                    }
                    //else ignore this rule
                }*/
                for (int i = 0; i < this.ChainedRuleParts.Count; i++)
                {
                    ConditionPack<TFirstMember> curCondChain = this.ChainedRuleParts[i];
                    if (curCondChain.Condition(this.ConditionValue))
                    {
                        this.UsedRule = true; //some rule in this chain was used, otherwise branch is not gonna be carried out 
                        for (int j = 0; j < curCondChain.Rules.Count; j++)
                        {
                            RulePack curRule = curCondChain.Rules[j];
                            if (!curRule.Ignore)
                            {
                                faker.UseRule(instance, curRule.MemberInfo, curRule.RandomFunc);
                            }
                        }
                    }
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
            private void AddFirstRule(MemberInfo member)
            {
                //ConditionPack<TFirstMember> rulePack = new ConditionPack<TFirstMember>(_ => true, member);
                //this.ChainedRuleParts.Add(rulePack);
                this.FirstMember = member;
            }
            public void AddFirstFunc(Func<object> func)
            {
                this.FirstFunc = func;
            }
            /// <summary>
            /// Called from .When
            /// </summary>
            /// <param name="condition"></param>
            public void AddNewCondPackWithCondition(Func<TFirstMember, bool> condition)
            {
                ConditionPack<TFirstMember> rulePack = new ConditionPack<TFirstMember>(condition);
                this.ChainedRuleParts.Add(rulePack);
            }
            /// <summary>
            /// Called from .Otherwise
            /// </summary>
            public void AddNewCondPackWithOtherwiseCondition()
            {
                ConditionPack<TFirstMember> rulePack = new ConditionPack<TFirstMember>( _ => !this.UsedRule);
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
                return this.ChainedRuleParts[ChainedRuleParts.Count - 1].GetLastMember();
            }
        }
        internal class ConditionPack<TFirstMember>
        {
            public Func<TFirstMember, bool> Condition { get; }

            public List<RulePack> Rules { get; } = new();
            //public MemberInfo MemberInfo { get; private set; }
            //public Func<object> RandomFunc { get; private set; }
            //public bool Ignore { get; private set; } = false;

            public ConditionPack(Func<TFirstMember, bool> condition)
            {
                this.Condition = condition;
            }

            public ConditionPack(Func<TFirstMember, bool> condition, MemberInfo memberInfo)
            {
                this.Condition = condition;
                this.Rules.Add(new RulePack(memberInfo));
                //this.MemberInfo = memberInfo;
                //this.RandomFunc = null;
            }

            public void AddMember(MemberInfo member)
            {
                this.Rules.Add(new RulePack(member));
            }

            public void AddFunction(Func<object> func)
            {
                this.Rules[Rules.Count - 1].AddFunction(func);
            }

            public void SetIgnored()
            {
                this.Rules[Rules.Count - 1].SetIgnored();
            }

            public MemberInfo GetLastMember()
            {
                return this.Rules[Rules.Count - 1].MemberInfo;
            }

        }

        internal class RulePack
        {
            public MemberInfo MemberInfo { get; private set; }
            public Func<object> RandomFunc { get; private set; }
            public bool Ignore { get; private set; } = false;

            public RulePack(MemberInfo member)
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
