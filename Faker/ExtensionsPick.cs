using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    public static class IListExtensions
    {
        static RandomGenerator RG = new();
        /// <summary>
        /// Randomly picks one item from the IList <br/>
        /// uses randomGenerator instance passed as the second argument
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="pickFrom"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static Titem PickRandom<Titem>(this IList<Titem> pickFrom, RandomGenerator random)
        {
            return random.Pick(pickFrom);
        }
        /// <summary>
        /// Randomly picks one item from the IList <br/>
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="pickFrom"></param>
        /// <returns></returns>
        public static Titem PickRandom<Titem>(this IList<Titem> pickFrom)
        {
            return RG.Pick(pickFrom);
        }

        public static IList<Titem> PickRandomMultiple<Titem>(this IList<Titem> pickFrom, int howMany)
        {
            return RG.PickMultiple(howMany, pickFrom);
        }

        public static IList<Titem> PickRandomMultiple<Titem>(this IList<Titem> pickFrom, int howMany, RandomGenerator random)
        {
            return random.PickMultiple(howMany, pickFrom);
        }

        public static IList<Titem> PickRandomMultipleNoRepeat<Titem>(this IList<Titem> pickFrom, int howMany)
        {
            return RG.PickMultipleNoRepeat(howMany, pickFrom);
        }

        public static IList<Titem> PickRandomMultipleNoRepeat<Titem>(this IList<Titem> pickFrom, int howMany, RandomGenerator random)
        {
            return random.PickMultipleNoRepeat(howMany, pickFrom);
        }

        public static void Shuffle<Titem>(this IList<Titem> toShuffle)
        {
            RG.Shuffle(toShuffle);
        }

        public static void Shuffle<Titem>(this IList<Titem> toShuffle, RandomGenerator random)
        {
            random.Shuffle(toShuffle);
        }
    }
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Randomly picks item from the ICollection <br/>
        /// LINEAR TIME COMPLEXITY! <br/>
        /// uses randomGenerator instance passed as the second argument
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="pickFrom"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static Titem PickRandom<Titem>(this ICollection<Titem> pickFrom, RandomGenerator random)
        {
            return random.Pick(pickFrom);
        }
        /// <summary>
        /// Randomly picks item from the ICollection <br/>
        /// LINEAR TIME COMPLEXITY! <br/>
        /// Uses newly created instance of the RandomGenerator
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="pickFrom"></param>
        /// <returns></returns>
        public static Titem PickRandom<Titem>(this ICollection<Titem> pickFrom)
        {
            RandomGenerator random = new RandomGenerator();
            return random.Pick(pickFrom);
        }
    }
}
