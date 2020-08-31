using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections;

namespace Faker
{
    public partial class RandomGenerator
    {
        /// <summary>
        /// returns count, when precise is true or random int less or equal to count when precise is false 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="precise">should be used precisely the count?</param>
        /// <returns></returns>
        internal int CountToUse(int count, bool precise)
        {
            int countToUse;
            if (!precise)
            {
                countToUse = this.Int(1, count);
            }
            else
            {
                countToUse = count;
            }
            return countToUse;
        }
        /// <summary>
        /// returns collection of TMembers produced by Func<TMember> randomFunc <br/>
        /// when precise is true, count is used as a size of returned collection <br/>
        /// otherwise a random number less or equal to count is generated and used as a count
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="count">count of members in returned collection</param>
        /// <param name="precise"></param>
        /// <returns></returns>
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
        /// <summary>
        /// returns collection of TMembers produced by Func<TMember, TMember, TMembe> randomFunc <br/>
        /// when precise is true, count is used as a size of returned collection <br/>
        /// otherwise a random number less or equal to count is generated and used as a count
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="lower">lower bound passed to random function</param>
        /// <param name="upper">upper bound passed to random function</param>
        /// <param name="count">count of members in returned collection</param>
        /// <param name="precise"></param>
        /// <returns></returns>
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
        /// <summary>
        /// returns collection of random members produced by a default random function for TMember type <br/>
        /// when precise is true, count is used as a size of returned collection <br/>
        /// otherwise a random number less or equal to count is generated and used as a count
        /// </summary>
        /// <exception cref="FakerException">Throws a FakerException, when there is no default random function for TMember type</exception>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="count"></param>
        /// <param name="precise"></param>
        /// <returns></returns>
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
        /// <summary>
        /// returns ICollection of TMembers produced by Func<int, bool, TMember> randomFunc<br/>
        /// this overload is meant to be used to create random collection of random collections (for instance list of random strings)<br/>
        /// count and precise affect outer collection, countInnerCollection and preciseInnerCollection affect member collections of this collection
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="count">count of returned collection</param>
        /// <param name="precise">is count precise or upper bound for random count</param>
        /// <param name="countInnerCollection">length of inner collections</param>
        /// <param name="preciseInnerCollection">is countInnerCollection precise or upper bound for random count</param>
        /// <returns></returns>
        public ICollection<TMember>RandomCollection<TMember>(Func<int, bool, TMember> randomFunc,int count, bool precise, int countInnerCollection, bool preciseInnerCollection)
        {
            ICollection<TMember> result = new Collection<TMember>();
            int countToUse = this.CountToUse(count, precise);
            for (int i = 0; i < countToUse; i++)
            {
                TMember next = (TMember)randomFunc(countInnerCollection, preciseInnerCollection);
                result.Add(next);
            }
            return result;
        }
        /// <summary>
        ///  returns a list of random members produced by a default random function for TMember type <br/>
        /// when precise is true, count is used as a count of returned list <br/>
        /// otherwise a random number less or equal to count is generated and used as a count
        /// </summary>
        /// <exception cref="FakerException">Throws a FakerException, when there is no default random function for TMember type</exception>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="count"></param>
        /// <param name="precise"></param>
        /// <returns></returns>
        public IList<TMember> RandomList<TMember>(int count, bool precise = false)
        {
            Type type = typeof(TMember);
            Func<object> randomFunc = this.GetDefaultRandomFuncForType(type);
            if (randomFunc is null)
            {
                throw new FakerException($"{typeof(TMember)} type does not have a default random function.");
            }
            IList<TMember> result = new List<TMember>();
            int countToUse = this.CountToUse(count, precise);
            for (int i = 0; i < countToUse; i++)
            {
                TMember next = (TMember)randomFunc();
                result.Add(next);
            }
            return result;
        }

        /// <summary>
        /// returns a list of TMembers produced by Func<TMember> randomFunc <br/>
        /// when precise is true, count is used as a count of returned list <br/>
        /// otherwise a random number less or equal to count is generated and used as a count
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="count"></param>
        /// <param name="precise"></param>
        /// <returns></returns>
        public IList<TMember> RandomList<TMember>(Func<TMember> randomFunc, int count, bool precise = true)
        {
            IList<TMember> result = new List<TMember>();

            int countToUse = this.CountToUse(count, precise);
            for (int i = 0; i < countToUse; i++)
            {
                TMember next = randomFunc();
                result.Add(next);
            }
            return result;
        }

        /// <summary>
        /// returns a list of TMembers produced by Func<TMember, TMember, TMembe> randomFunc <br/>
        /// when precise is true, count is used as a count of returned list <br/>
        /// otherwise a random number less or equal to count is generated and used as a count
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="lower">lower bound passed to random function</param>
        /// <param name="upper">upper bound passed to random function</param>
        /// <param name="count"></param>
        /// <param name="precise"></param>
        /// <returns></returns>
        public IList<TMember> RandomList<TMember>(Func<TMember, TMember, TMember> randomFunc, TMember lower, TMember upper, int count, bool precise = true)
        {
            IList<TMember> result = new List<TMember>();
            int countToUse = this.CountToUse(count, precise);
            for (int i = 0; i < countToUse; i++)
            {
                TMember next = randomFunc(lower, upper);
                result.Add(next);
            }
            return result;
        }
        /// <summary>
        /// returns IList of TMembers produced by Func<int, bool, TMember> randomFunc<br/>
        /// this overload is meant to be used to create random list of random collections (for instance list of random strings)<br/>
        /// count and precise affect outer list, countInnerCollection and preciseInnerCollection affect members of this list
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="count">count of returned list</param>
        /// <param name="precise">is count precise or upper bound for random count</param>
        /// <param name="countInnerCollection">length of inner collections</param>
        /// <param name="preciseInnerCollection">is countInnerCollection precise or upper bound for random count</param>
        /// <returns></returns>
        public IList<TMember> RandomList<TMember>(Func<int, bool, TMember> randomFunc, int count, bool precise, int countInnerCollection, bool preciseInnerCollection)
        {
            IList<TMember> result = new List<TMember>();
            int countToUse = this.CountToUse(count, precise);
            for (int i = 0; i < countToUse; i++)
            {
                TMember next = (TMember)randomFunc(countInnerCollection, preciseInnerCollection);
                result.Add(next);
            }
            return result;
        }
        //TODO: Random enumerable?
        /*  public IEnumerable<TMember> RandomEnumerable<TMember>(Func<TMember> randomFunc)
          {
              RandomEnumerableClass < TMember > Enumerable = new RandomEnumerableClass<TMember>(randomFunc);
              return Enumerable;
          }
          private class RandomEnumerableClass<TMemeber> : IEnumerable<TMemeber>
          {
              internal Func<TMemeber> randomFunc;
              public RandomEnumerableClass(Func<TMemeber> func)
              {
                  this.randomFunc = func;
              }
              public IEnumerator<TMemeber> GetEnumerator()
              {
                  while (true)
                  {
                      yield return this.randomFunc();
                  }
              }

              IEnumerator IEnumerable.GetEnumerator()
              {
                  throw new NotImplementedException();
              }
          }*/
    }
}
