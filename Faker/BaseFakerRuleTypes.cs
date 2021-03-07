using System;
using System.Collections.Generic;
using System.Linq;
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

        internal class ChainedRule : RuleType
        {
            public List<FluentAction> FluentActions { get; } = new List<FluentAction>();
            //TODO: rest of implementation
        }
    }
}
