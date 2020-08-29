using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Faker
{
    public partial class RandomGenerator
    {
        internal int CountToUse(int count, bool precise)
        {
            int countToUse;
            if (!precise)
            {
                countToUse = this.RandomInt(0, count);
            }
            else
            {
                countToUse = count;
            }
            return countToUse;
        }
        public ICollection<TMember> RandomCollection<TMember>(Func<TMember> randomFunc, int count, bool precise = true)
        {
            ICollection<TMember> result = new Collection<TMember>();

            int countToUse = this.CountToUse(count, precise);
            for (int i = 0; i < countToUse; i++)
            {
                TMember next = randomFunc();
                result.Add(next);
            }
            return result;
        }
        public ICollection<TMember> RandomCollection<TMember>(Func<TMember, TMember, TMember> randomFunc, TMember lower, TMember upper, int count, bool precise = true)
        {
            ICollection<TMember> result = new Collection<TMember>();
            int countToUse = this.CountToUse(count, precise);
            for (int i = 0; i < countToUse; i++)
            {
                TMember next = randomFunc(lower, upper);
                result.Add(next);
            }
            return result;
        }
        public ICollection<TMember> RandomCollection<TMember>(int count, bool precise = true)
        {
            Type type = typeof(TMember);
            Func<object> randomFunc = this.GetDefaultRandomFuncForType(type);
            if (randomFunc is null)
            {
                throw new FakerException($"{typeof(TMember)} type does not have a default random function.");
            }
            ICollection<TMember> result = new Collection<TMember>();
            int countToUse = this.CountToUse(count, precise);
            for (int i = 0; i < countToUse; i++)
            {
                TMember next = (TMember)randomFunc();
                result.Add(next);
            }
            return result;
        }
        public IList<TMember> RandomList<TMember>(int count)
        {
            Type type = typeof(TMember);
            Func<object> randomFunc = this.GetDefaultRandomFuncForType(type);
            if (randomFunc is null)
            {
                throw new FakerException($"{typeof(TMember)} type does not have a default random function.");
            }
            IList<TMember> result = new List<TMember>();
            for (int i = 0; i < count; i++)
            {
                TMember next = (TMember)randomFunc();
                result.Add(next);
            }
            return result;
        }
    }
}
