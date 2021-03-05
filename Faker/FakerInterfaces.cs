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
        public class RefMemberFluent<TInnerClass> where TInnerClass : class
        {
            private BaseFaker<TClass> BaseFakerInstance { get; }

            internal RefMemberFluent(BaseFaker<TClass>  faker)
            {
                this.BaseFakerInstance = faker;
            }

            public void Faker(BaseFaker<TInnerClass> faker)
            {
                this.BaseFakerInstance._faker(faker);
            }
        }
    }
}
