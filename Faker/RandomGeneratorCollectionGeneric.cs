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
                countToUse = this.Random.Int(1, count);
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
        public ICollection<TMember> GenericCollection<TMember>(Func<TMember> randomFunc, int count, bool precise = true)
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
        public ICollection<TMember> GenericCollection<TMember>(Func<TMember, TMember, TMember> randomFunc, int count, TMember lower, TMember upper, bool precise = true)
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
        /// returns collection of TMembers produced by Func<TMember?, TMember?, TMembe> randomFunc <br/>
        /// when precise is true, count is used as a size of returned collection <br/>
        /// otherwise a random number less or equal to count is generated and used as a count
        /// overload for floating point types
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="lower">lower bound passed to random function</param>
        /// <param name="upper">upper bound passed to random function</param>
        /// <param name="count">count of members in returned collection</param>
        /// <param name="precise"></param>
        /// <returns></returns>
        public ICollection<TMember> GenericCollection<TMember>(Func<TMember?, TMember?, TMember> randomFunc, int count, TMember? lower = null, TMember? upper=null, bool precise = true) where TMember: struct
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
        public ICollection<TMember> GenericCollection<TMember>(int count, bool precise = true)
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
        public ICollection<TMember>GenericCollection<TMember>(Func<int, bool, TMember> randomFunc,int count, bool precise, int countInnerCollection, bool preciseInnerCollection)
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
        public IList<TMember> GenericList<TMember>(int count, bool precise = false)
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
        public IList<TMember> GenericList<TMember>(Func<TMember> randomFunc, int count, bool precise = true)
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
        /// returns a list of TMembers produced by Func<TMember, TMember, TMember> randomFunc <br/>
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
        public IList<TMember> GenericList<TMember>(Func<TMember, TMember, TMember> randomFunc, int count, TMember lower, TMember upper, bool precise = true)
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
        /// returns a list of TMembers produced by Func<TMember?, TMember?, TMember?> randomFunc <br/>
        /// when precise is true, count is used as a count of returned list <br/>
        /// otherwise a random number less or equal to count is generated and used as a count
        /// overload for floating point types
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="lower">lower bound passed to random function</param>
        /// <param name="upper">upper bound passed to random function</param>
        /// <param name="count"></param>
        /// <param name="precise"></param>
        /// <returns></returns>
        public IList<TMember> GenericList<TMember>(Func<TMember?, TMember?, TMember> randomFunc, int count, TMember? lower=null, TMember? upper=null, bool precise = true) where TMember :struct
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
        public IList<TMember> GenericList<TMember>(Func<int, bool, TMember> randomFunc, int count, bool precise, int countInnerCollection, bool preciseInnerCollection)
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
        /// <summary>
        /// returns enumerable of TMembers produced by Func<TMember> randomFunc <br/>
        /// when precise is true, count is used as a size of returned collection <br/>
        /// otherwise a random number less or equal to count is generated and used as a count
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="count">count of members in returned enumerable</param>
        /// <param name="precise"></param>
        /// <returns></returns>
        public IEnumerable<TMember> GenericEnumerable<TMember>(Func<TMember> randomFunc, int count, bool precise = true)
        {
            int countToUse = this.CountToUse(count, precise);
            for (int i = 0; i < countToUse; i++)
            {
                TMember next = randomFunc();
                yield return next;
            }
        }
        /// <summary>
        /// returns an enumerable of TMembers produced by Func<TMember, TMember, TMembe> randomFunc <br/>
        /// when precise is true, count is used as a size of returned collection <br/>
        /// otherwise a random number less or equal to count is generated and used as a count
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="lower">lower bound passed to random function</param>
        /// <param name="upper">upper bound passed to random function</param>
        /// <param name="count">count of members in returned enumerable</param>
        /// <param name="precise"></param>
        /// <returns></returns>
        public IEnumerable<TMember> GenericEnumerable<TMember>(Func<TMember, TMember, TMember> randomFunc, int count, TMember lower, TMember upper, bool precise = true)
        {
            int countToUse = this.CountToUse(count, precise);
            for (int i = 0; i < countToUse; i++)
            {
                TMember next = randomFunc(lower, upper);
                yield return next;
            }
        }
        /// <summary>
        /// returns an enumerable of TMembers produced by Func<TMember?, TMember?, TMember> randomFunc <br/>
        /// when precise is true, count is used as a size of returned collection <br/>
        /// otherwise a random number less or equal to count is generated and used as a count
        /// overload for floating point types
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="lower">lower bound passed to random function</param>
        /// <param name="upper">upper bound passed to random function</param>
        /// <param name="count">count of members in returned enumerable</param>
        /// <param name="precise"></param>
        /// <returns></returns>
        public IEnumerable<TMember> GenericEnumerable<TMember>(Func<TMember?, TMember?, TMember> randomFunc, int count, TMember? lower = null, TMember? upper = null, bool precise = true) where TMember: struct
        {
            int countToUse = this.CountToUse(count, precise);
            for (int i = 0; i < countToUse; i++)
            {
                TMember next = randomFunc(lower, upper);
                yield return next;
            }
        }

        /// <summary>
        /// returns an enumerable of random members produced by a default random function for TMember type <br/>
        /// when precise is true, count is used as a size of returned collection <br/>
        /// otherwise a random number less or equal to count is generated and used as a count
        /// </summary>
        /// <exception cref="FakerException">Throws a FakerException, when there is no default random function for TMember type</exception>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="count"></param>
        /// <param name="precise"></param>
        /// <returns></returns>
        public IEnumerable<TMember> GenericEnumerable<TMember>(int count, bool precise = true)
        {
            Type type = typeof(TMember);
            Func<object> randomFunc = this.GetDefaultRandomFuncForType(type);
            if (randomFunc is null)
            {
                throw new FakerException($"{typeof(TMember)} type does not have a default random function.");
            }
            int countToUse = this.CountToUse(count, precise);
            for (int i = 0; i < countToUse; i++)
            {
                TMember next = (TMember)randomFunc();
                yield return next;
            }
        }
        /// <summary>
        /// returns enumerable of TMembers produced by Func<int, bool, TMember> randomFunc<br/>
        /// this overload is meant to be used to create random enumerable of random collections (for instance enumerable of random strings)<br/>
        /// count and precise affect outer list, countInnerCollection and preciseInnerCollection affect members of this list
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="countOuter">count of items in returned enumerable</param>
        /// <param name="preciseOuter">is count precise or upper bound for random count</param>
        /// <param name="countInnerCollection">count of items in inner collections</param>
        /// <param name="preciseInnerCollection">is countInnerCollection precise or upper bound for random count</param>
        /// <returns></returns>
        public IEnumerable<TMember> GenericEnumerable<TMember>(Func<int, bool, TMember> randomFunc, int countOuter, bool preciseOuter, int countInnerCollection, bool preciseInnerCollection)
        {
            int countToUse = this.CountToUse(countOuter, preciseOuter);
            for (int i = 0; i < countToUse; i++)
            {
                TMember next = randomFunc(countInnerCollection, preciseInnerCollection);
                yield return next;
            }
        }
        /// <summary>
        /// returns INFINITE enumerable of TMembers produced by Func<TMember> randomFunc<br/>
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <returns></returns>
        public IEnumerable<TMember> InfiniteGenericEnumerable<TMember>(Func<TMember> randomFunc)
        {
            while(true)
            {
                TMember next = randomFunc();
                yield return next;
            }
        }
        /// <summary>
        /// returns an INFINITE enumerable of TMembers produced by Func<TMember, TMember, TMember> randomFunc <br/>
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public IEnumerable<TMember> InfiniteGenericEnumerable<TMember>(Func<TMember, TMember, TMember> randomFunc, TMember lower, TMember upper)
        {
            while(true)
            {
                TMember next = randomFunc(lower, upper);
                yield return next;
            }
        }
        /// <summary>
        /// returns an INFINITE enumerable of TMembers produced by Func<TMember?, TMember?, TMember> randomFunc <br/>
        /// overload for floating point types
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public IEnumerable<TMember> InfiniteGenericEnumerable<TMember>(Func<TMember?, TMember?, TMember> randomFunc, TMember? lower=null, TMember? upper=null) where TMember: struct
        {
            while (true)
            {
                TMember next = randomFunc(lower, upper);
                yield return next;
            }
        }

        /// <summary>
        /// returns an INFINITE enumerable of random members produced by a default random function for TMember type <br/>
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <returns></returns>
        public IEnumerable<TMember> InfiniteGenericEnumerable<TMember>()
        {
            Type type = typeof(TMember);
            Func<object> randomFunc = this.GetDefaultRandomFuncForType(type);
            if (randomFunc is null)
            {
                throw new FakerException($"{typeof(TMember)} type does not have a default random function.");
            }
            while(true)
            {
                TMember next = (TMember)randomFunc();
                yield return next;
            }
        }
        /// <summary>
        /// returns enumerable of TMembers produced by Func<int, bool, TMember> randomFunc<br/>
        /// this overload is meant to be used to create random enumerable of random collections (for instance enumerable of random strings)<br/>
        /// count and precise affect outer list, countInnerCollection and preciseInnerCollection affect members of this list
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="randomFunc"></param>
        /// <param name="countInnerCollection">count of items in inner collections</param>
        /// <param name="preciseInnerCollection">is countInnerCollection precise or upper bound for random count</param>
        /// <returns></returns>
        public IEnumerable<TMember> InfiniteGenericEnumerable<TMember>(Func<int, bool, TMember> randomFunc, int countInnerCollection, bool preciseInnerCollection)
        {
            while(true)
            {
                TMember next = randomFunc(countInnerCollection, preciseInnerCollection);
                yield return next;
            }
        }
    }
}
