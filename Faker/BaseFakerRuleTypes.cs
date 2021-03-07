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

            public bool UsedRule { get; private set; }

            public override void ResolveChainedRule(TClass instance, BaseFaker<TClass> faker)
            {
                throw new NotImplementedException();
                
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

            public Func<TFirstMember, bool> Condition { get; }
            public Func<object> RandomFunc { get; private set; } 
            public MemberInfo MemberInfo { get; private set; }

            public void AddFunction(Func<object> func)
            {
                this.RandomFunc = func;
            }
            public void AddMember(MemberInfo member)
            {
                this.MemberInfo = member;
            }
        }
    }  
}
