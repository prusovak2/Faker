using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    public partial class RandomGenerator
    {
        /// <summary>
        /// Randomly picks one item from the IList
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="pickFrom"></param>
        /// <returns></returns>
        public Titem Pick<Titem>(IList<Titem> pickFrom)
        {
            if(pickFrom is null)
            {
                throw new ArgumentNullException("List to pick from mustn't be null");
            }
            int randomIndex = this.Random.Int(0, pickFrom.Count - 1);
            return pickFrom[randomIndex];
        }
        /// <summary>
        /// Randomly picks one of items passed as arguments
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="pickFrom"></param>
        /// <returns></returns>
        public Titem Pick<Titem>(params Titem[] pickFrom)
        {
            return Pick((IList<Titem>)pickFrom);
        }
        /// <summary>
        /// Randomly picks item from the ICollection <br/>
        /// LINEAR TIME COMPLEXITY!
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="pickFrom"></param>
        /// <returns></returns>
        public Titem Pick<Titem>(ICollection<Titem> pickFrom)
        {
            if (pickFrom is null)
            {
                throw new ArgumentNullException("Collection to pick from mustn't be null");
            }
            int randomIndex = this.Random.Int(0, pickFrom.Count - 1);
            int counter = 0;
            foreach (Titem item in pickFrom)
            {
                if (counter == randomIndex)
                {
                    return item;
                }
                counter++;
            }
            throw new NotImplementedException("Unexpected");
        }

        /// <summary>
        /// Returns a list of given number of randomly selected items from the pickFrom list <br/>
        /// items may be picked multiple times <br/>
        /// does not copy items, if Titem is reference type, returned list contains references to the same items as contained in the source list 
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="howMany">count of items in returned list</param>
        /// <param name="pickFrom">list to pick from</param>
        /// <returns></returns>
        public IList<Titem> PickMultiple<Titem>(int howMany, IList<Titem> pickFrom)
        {
            if(howMany < 0)
            {
                throw new ArgumentException("Argument howMany mustn't be negative.");
            }
            if (pickFrom is null)
            {
                throw new ArgumentNullException("List to pick from mustn't be null");
            }
            Titem[] picked = new Titem[howMany];
            for (int i = 0; i < howMany; i++)
            {
                int randomIndex = this.Random.Int(0, pickFrom.Count - 1);
                picked[i] = pickFrom[randomIndex];
            }
            return picked;
        }
        /// <summary>
        /// Randomly picks several of items passed as arguments, item might be picked multiple times
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="howMany"></param>
        /// <param name="pickFrom"></param>
        /// <returns></returns>
        public IList<Titem> PickMultiple<Titem>(int howMany, params Titem[] pickFrom)
        {
            return PickMultiple(howMany, (IList<Titem>)pickFrom);
        }
        /// <summary>
        /// Returns a list of given number of randomly selected items from the pickFrom list <br/>
        /// each item can be picked only one, if items repeat themselves in the source array, they may be repeated in the result as well <br/>
        /// does not copy items, if Titem is reference type, returned list contains references to the same items as contained in the source list 
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="howMany"></param>
        /// <param name="pickFrom"></param>
        /// <returns></returns>
        public IList<Titem> PickMultipleNoRepeat<Titem>(int howMany, IList<Titem> pickFrom)
        {
            if (howMany < 0)
            {
                throw new ArgumentException("Argument howMany mustn't be negative.");
            }
            if (pickFrom is null)
            {
                throw new ArgumentNullException("List to pick from mustn't be null");
            }
            if(howMany >= pickFrom.Count)
            {
                throw new ArgumentException("Argument howMany must be less than or equal to the number of the elements in the list to pick from.");
            }

            Titem[] picked = new Titem[howMany];
            int[] indices = new int[pickFrom.Count];
            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = i;
            }

            for (int i = 0; i < howMany; i++)
            {
                // pick only from the part of indices array that had not yet been reordered to contain resulting indices  
                int randomIndex = this.Random.Int(i, pickFrom.Count-1);
                //swap indices[i] and indices[randomIndex] 
                int tmp = indices[i];
                indices[i] = indices[randomIndex];
                indices[randomIndex] = tmp;
                // member from the beginning are swapped with selected members so that they can be selected latter on 
                picked[i] = pickFrom[indices[i]];
            }
            return picked;
        }
        /// <summary>
        /// Randomly picks several of items passed as arguments, each item might be picked only once
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="howMany"></param>
        /// <param name="pickFrom"></param>
        /// <returns></returns>
        public IList<Titem> PickMultipleNoRepeat<Titem>(int howMany, params Titem[] pickFrom)
        {
            return PickMultipleNoRepeat(howMany, (IList<Titem>)pickFrom);
        }
        /// <summary>
        /// Shuffles (modifies) a given list
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="toShuffle"></param>
        public void Shuffle<Titem>(IList<Titem> toShuffle)
        {
            for (int i = 0; i < toShuffle.Count; i++)
            {
                int randomIndex = this.Random.Int(i, toShuffle.Count - 1);
                Titem tmp = toShuffle[i];
                toShuffle[i] = toShuffle[randomIndex];
                toShuffle[randomIndex] = tmp;
            }
        }
    }
}
