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
        internal abstract class FluentAction
        {
            public abstract void Resolve<TMember>(Resolver<TMember> resover);
        }
        internal class FluentFor : FluentAction
        {
            internal MemberInfo memberToBeFilled { get; }
            public sealed override void Resolve<TMember>(Resolver<TMember> resover)
            {
                throw new NotImplementedException();
            }
        }
        internal class FluentSetRule : FluentAction
        {
            public sealed override void Resolve<TMember>(Resolver<TMember> resover)
            {
                throw new NotImplementedException();
            }
        }
        internal class FluentWhen : FluentAction
        {
            public sealed override void Resolve<TMember>(Resolver<TMember> resover)
            {
                throw new NotImplementedException();
            }
        }
        internal class FluentOtherwise : FluentAction
        {
            public sealed override void Resolve<TMember>(Resolver<TMember> resover)
            {
                throw new NotImplementedException();
            }
        }
    }
}
