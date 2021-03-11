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
        //TODO: delete
        /*internal class Resolver<TMember>
        {
            public MemberInfo MemberToBeFilled { get; set; }

            public Predicate<TMember> Condition { get; set; }
            public TMember ConditionValue 
            { 
                get => _conditionValue;
                set
                {
                    if (!this.HasConditionValue)
                    {
                        this.HasConditionValue = true;
                        _conditionValue = value;
                    }
                }
            }
            private TMember _conditionValue;
            public bool HasConditionValue { get; private set; } = false;

            public bool UseRule { get; set; } = false;
            public bool UsedCondRule { get; set; } = false;
        }*/
    }
}
