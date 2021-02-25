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

        public IList<Titem> PickMultiple<Titem>(int howMany, params Titem[] pickFrom)
        {
            return PickMultiple(howMany, (IList<Titem>)pickFrom);
        }

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

        public IList<Titem> PickMultipleNoRepeat<Titem>(int howMany, params Titem[] pickFrom)
        {
            return PickMultipleNoRepeat(howMany, (IList<Titem>)pickFrom);
        }

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
