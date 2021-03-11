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
        /// <summary>
        /// Stores chained rule and evaluates it when it is to be used
        /// </summary>
        /// <typeparam name="TFirstMember"></typeparam>
        internal class ChainedRuleResolver<TFirstMember> : ChainedRule
        {
            /// <summary>
            /// Create new resolver with given leading member
            /// </summary>
            /// <param name="member"></param>
            public ChainedRuleResolver(MemberInfo member)
            {
                this.AddFirstRule(member);
            }
            /// <summary>
            /// Leading member of this rule chain
            /// </summary>
            public MemberInfo FirstMember { get; private set; }
            /// <summary>
            /// The first and the only unconditional rule of this rule chain
            /// </summary>
            public Func<object> FirstFunc { get; private set; }
            /// <summary>
            /// Random value generated when the leading unconditional rule was evaluated <br/>
            /// All condition in this chained rule are evaluated with respect to this value
            /// </summary>
            public TFirstMember ConditionValue { get; set; } //generation state
            /// <summary>
            /// List of conditional rules
            /// </summary>
            public List<ConditionPack<TFirstMember>> ChainedRuleParts { get; } = new List<ConditionPack<TFirstMember>>();
            /// <summary>
            /// Has any of .When conditions satisfied? <br/>
            /// Used to evaluate .Otherwise condition
            /// </summary>
            public bool UsedRule { get; private set; } = false; // generation state

            public override void ResolveChainedRule(TClass instance, BaseFaker<TClass> faker)
            {
                // first leading rule - unconditional, provides value to evaluate conditions with
                this.ConditionValue = faker.UseRule<TFirstMember>(instance, FirstMember, FirstFunc); // set the value that will be considered in following conditions and use rule
                // evaluate conditional rules
                for (int i = 0; i < this.ChainedRuleParts.Count; i++)
                {
                    ConditionPack<TFirstMember> curCondChain = this.ChainedRuleParts[i];
                    if (curCondChain.Condition(this.ConditionValue))
                    {
                        this.UsedRule = true; //some rule in this chain was used, otherwise branch is not gonna be carried out 
                        for (int j = 0; j < curCondChain.Rules.Count; j++)
                        {
                            RulePack curRule = curCondChain.Rules[j];
                            if (!curRule.Ignore) // function less RulePack corresponding to .Ignore call 
                            {
                                faker.UseRule(instance, curRule.MemberInfo, curRule.RandomFunc);
                            }
                        }
                    }
                }
                this.ClearState(); //reset state so that this Resolver can be reused by another ._populate call 
            }
            /// <summary>
            /// resets state so that this Resolver can be reused by another ._populate call 
            /// </summary>
            private void ClearState()
            {
                this.ConditionValue = default;
                this.UsedRule = false;
            }
            /// <summary>
            /// Sets the FirstMember (unconditional) of this Resolver <br/>
            /// Called from BaseFaker.For
            /// </summary>
            /// <param name="member"></param>
            private void AddFirstRule(MemberInfo member)
            {
                this.FirstMember = member;
            }
            /// <summary>
            /// Sets the FirstFunc (unconditional) of this Resolver <br/>
            /// Called from ._firtsSetRule
            /// </summary>
            /// <param name="func"></param>
            public void AddFirstFunc(Func<object> func)
            {
                this.FirstFunc = func;
            }
            /// <summary>
            /// Adds new condition to this Resolver <br/>
            /// Called from .When
            /// </summary>
            /// <param name="condition"></param>
            public void AddNewCondPackWithCondition(Func<TFirstMember, bool> condition)
            {
                ConditionPack<TFirstMember> rulePack = new ConditionPack<TFirstMember>(condition);
                this.ChainedRuleParts.Add(rulePack);
            }
            /// <summary>
            /// Adds new Otherwise condition to this Resolver <br/>
            /// Called from .Otherwise
            /// </summary>
            public void AddNewCondPackWithOtherwiseCondition()
            {
                ConditionPack<TFirstMember> rulePack = new ConditionPack<TFirstMember>( _ => !this.UsedRule);
                this.ChainedRuleParts.Add(rulePack);
            }
            /// <summary>
            /// Adds a member part of a new conditional rule to the last ConditionPack of this Resolver <br/>
            /// Called from ConditionFluent._for
            /// </summary>
            /// <param name="member"></param>
            public void AddMemberToLastRulePack(MemberInfo member)
            {
                this.ChainedRuleParts[ChainedRuleParts.Count - 1].AddMember(member);
            }
            /// <summary>
            /// Adds a function part of a new conditional rule to the last RulePack of the last ConditionPack of this Resolver <br/>
            /// Called from SetRule
            /// </summary>
            /// <param name="func"></param>
            public void AddFunctionToLastRulePack(Func<object> func)
            {
                this.ChainedRuleParts[ChainedRuleParts.Count - 1].AddFunction(func);
            }
            /// <summary>
            /// /// Sets the last RulePack of the last ConditionPack of this Resolver Ignored <br/>
            /// Called from AutoFaker._autoIgnore
            /// </summary>
            public void SetLastRulePackIgnored()
            {
                this.ChainedRuleParts[ChainedRuleParts.Count - 1].SetIgnored();
            }
            /// <summary>
            /// Returns the last member added to this Resolver <br/>
            /// That means it returns the member from the last RulePack of the Last ConditionPack of this Resolver
            /// </summary>
            /// <returns></returns>
            public MemberInfo GetLastMember()
            {
                return this.ChainedRuleParts[ChainedRuleParts.Count - 1].GetLastMember();
            }
        }
        /// <summary>
        /// Represent one condition and series of Rules to be applied if and only if the condition is satisfied
        /// </summary>
        /// <typeparam name="TFirstMember"></typeparam>
        internal class ConditionPack<TFirstMember>
        {
            public Func<TFirstMember, bool> Condition { get; }

            public List<RulePack> Rules { get; } = new();
            /// <summary>
            /// new RulePack with a given condition
            /// </summary>
            /// <param name="condition"></param>
            public ConditionPack(Func<TFirstMember, bool> condition)
            {
                this.Condition = condition;
            }
            /// <summary>
            /// Adds new Rule with given member
            /// </summary>
            /// <param name="member"></param>
            public void AddMember(MemberInfo member)
            {
                this.Rules.Add(new RulePack(member));
            }
            /// <summary>
            /// Adds a function part to the last Rule
            /// </summary>
            /// <param name="func"></param>
            public void AddFunction(Func<object> func)
            {
                this.Rules[Rules.Count - 1].AddFunction(func);
            }
            /// <summary>
            /// Sets the last Rule as Ignored
            /// </summary>
            public void SetIgnored()
            {
                this.Rules[Rules.Count - 1].SetIgnored();
            }
            /// <summary>
            /// Returns the last member added to this ConditionPack
            /// </summary>
            /// <returns></returns>
            public MemberInfo GetLastMember()
            {
                return this.Rules[Rules.Count - 1].MemberInfo;
            }

        }
        /// <summary>
        /// Represents a pair of a member and a function to be used to fill the member with a pseudo-random contend
        /// </summary>
        internal class RulePack
        {
            public MemberInfo MemberInfo { get; private set; }
            public Func<object> RandomFunc { get; private set; }
            /// <summary>
            /// does this RulePack represent .Ignore call
            /// </summary>
            public bool Ignore { get; private set; } = false;
            /// <summary>
            /// new RulePack with a member parts set
            /// </summary>
            /// <param name="member"></param>
            public RulePack(MemberInfo member)
            {
                this.MemberInfo = member;
            }
            /// <summary>
            /// Adds function part to this RulePack
            /// </summary>
            /// <param name="func"></param>
            public void AddFunction(Func<object> func)
            {
                this.RandomFunc = func;
            }
            /// <summary>
            /// Sets this RulePack Ignored
            /// </summary>
            public void SetIgnored()
            {
                this.Ignore = true;
            }
        }
    }  
}
