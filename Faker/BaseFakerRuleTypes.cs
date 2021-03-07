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

            public TFirstMember ConditionValue
            {
                get => _conditionValue;
                set
                {
                    if (!this._hasConditionValue)
                    {
                        this._hasConditionValue = true;
                        _conditionValue = value;
                    }
                }
            }
            private TFirstMember _conditionValue;
            private bool _hasConditionValue = false;

            public List<RulePack<TFirstMember>> ChainedRuleParts { get; } = new List<RulePack<TFirstMember>>();

            public bool UsedRule { get; private set; } = false;

            public override void ResolveChainedRule(TClass instance, BaseFaker<TClass> faker)
            {
                throw new NotImplementedException();
                
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
            
        }
        internal struct RulePack<TFirstMember>
        {
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

            public Func<TFirstMember, bool> Condition { get; }
            public MemberInfo MemberInfo { get; private set; }
            public Func<object> RandomFunc { get; private set; }

            public void AddMember(MemberInfo member)
            {
                this.MemberInfo = member;
            }

            public void AddFunction(Func<object> func)
            {
                this.RandomFunc = func;
            }
           
        }
    }  
}
