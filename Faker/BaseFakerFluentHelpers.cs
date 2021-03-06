using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    public partial class BaseFaker<TClass> : IFaker where TClass : class
    {

//Set Faker helpers
        public class RefMemberFluent<TInnerClass> where TInnerClass : class
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal RefMemberFluent(BaseFaker<TClass>  faker)
            {
                this.FakerInstance = faker;
            }

            public void Faker(BaseFaker<TInnerClass> faker)
            {
                this.FakerInstance._faker(faker);
            }
        }

//Unconditional Rule Helpers
        public class UncontionalRuleMemberFluent<TMember>
        {
            private BaseFaker<TClass> FakerInstance { get; }

            internal UncontionalRuleMemberFluent(BaseFaker<TClass> faker)
            {
                this.FakerInstance = faker;
            }

            public void Rule(Func<RandomGenerator, TMember> setter)
            {
                this.FakerInstance._uncoditionalRule<TMember>(setter);
            }
        }
    }
}
