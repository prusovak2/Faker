using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public static class DoubleDecimaFloatExtensions
    {
        /// <summary>
        /// compare two double values for equality with respect to relative margin of difference between two values (0.00001 * lowerNumber)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool EpsilonEquals(this double a, double b)
        {
            double epsilon;
            // Define the tolerance for variation in their values based on lower number
            if (a <= b)
            {
                epsilon = Math.Abs(a * 0.00001);
            }
            else
            {
                epsilon = Math.Abs(b * 0.00001);
            }
            // Compare the values
            if (Math.Abs(a - b) <= epsilon)
            {
                return true;
            }
            else
            {
                return false;
            }    
        }
        /// <summary>
        /// compare two float values for equality with respect to relative margin of difference between two values (0.00001 * lowerNumber)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool EpsilonEquals(this float a, float b)
        {
            float epsilon;
            // Define the tolerance for variation in their values based on lower number
            if (a <= b)
            {
                epsilon = Math.Abs(a * 0.00001f);
            }
            else
            {
                epsilon = Math.Abs(b * 0.00001f);
            }
            if (Math.Abs(a - b) <= epsilon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// compare two decimal values for equality with respect to relative margin of difference between two values (0.00001 * lowerNumber)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool EpsilonEquals(this decimal a, decimal b)
        {
            //check, whether the length of an interval does fit into decimal
            //if interval between values is that large, values obviously are not equal
            if (a.RangeTooLarge(b))
            {
                return false;
            }

            decimal epsilon;
            // Define the tolerance for variation in their values based on lower number
            if (a <= b)
            {
                epsilon = Math.Abs(a * 0.00001m);
            }
            else
            {
                epsilon = Math.Abs(b * 0.00001m);
            }

            //this would throw System.OverflowException for too large interval
            if (Math.Abs(a - b) <= epsilon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        ///  compare two decimal values for equality with respect to relative margin of difference between two values (0.00001 * lowerNumber)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="RangeTooLarge">true, if interval [a,b] is to large to fit its length into a decimal variable</param>
        /// <returns></returns>
        public static bool EpsilonEquals(this decimal a, decimal b, out bool RangeTooLarge)
        {
            //check, whether the length of an interval does fit into decimal
            //if interval between values is that large, values obviously are not equal
            if (a.RangeTooLarge(b))
            {
                RangeTooLarge = true;
                return false;
            }
            RangeTooLarge = false;
            decimal epsilon;
            // Define the tolerance for variation in their values based on lower number
            if (a <= b)
            {
                epsilon = Math.Abs(a * 0.00001m);
            }
            else
            {
                epsilon = Math.Abs(b * 0.00001m);
            }
            //this would throw System.OverflowException for too large interval
            if (Math.Abs(a - b) <= epsilon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// determines, whether the length of the interval [a,b] is to high to fit into a decimal variable <br/>
        /// sufficient but not necessary condition <br/>
        /// may return true even when the interval is short enough, but never returns false for too long interval
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        internal static bool RangeTooLarge(this decimal a, decimal b)
        {
            decimal upperBound = (decimal.MaxValue / 2) ;
            decimal lowerBound = (upperBound-1) * -1;
            //length of [lowerBound,upperBound] is decimal.MaxValue
            //decimal test = Math.Abs(upperBound - lowerBound); //
            //is the interval too long?
            if ((a < lowerBound && b >= 0) || (b < lowerBound && a >= 0) || (a > upperBound && b <= 0) || (b > upperBound && a <= 0))
            {
                return true;
            }
            return false;
        }
    }
    internal static class TypeExtensions
    {
        internal static Dictionary<Type, object> SampleInstances { get; }

        static TypeExtensions()
        {
            TypeExtensions.SampleInstances = new Dictionary<Type, object>();
            SampleInstances.Add(typeof(byte), new byte());
            SampleInstances.Add(typeof(sbyte), new sbyte());
            SampleInstances.Add(typeof(short), new short());
            SampleInstances.Add(typeof(ushort), new ushort());
            SampleInstances.Add(typeof(int), new int());
            SampleInstances.Add(typeof(uint), new uint());
            SampleInstances.Add(typeof(long), new long());
            SampleInstances.Add(typeof(ulong), new ulong());
            SampleInstances.Add(typeof(float), new float());
            SampleInstances.Add(typeof(double), new double());
            SampleInstances.Add(typeof(decimal), new decimal());
            SampleInstances.Add(typeof(char), new char());
            SampleInstances.Add(typeof(bool), new bool());
            SampleInstances.Add(typeof(string), "string");
            SampleInstances.Add(typeof(DateTime), new DateTime());
            SampleInstances.Add(typeof(Guid), new Guid());

        }
        /// <summary>
        /// provides a sample instance of type to be used as param. of RandomGenerator.GetDefaultRandomFuncForType(object o)
        /// </summary>
        internal static object GetSampleInstance(this Type type)
        {
            if (!SampleInstances.ContainsKey(type))
            {
                return null;
            }
            return SampleInstances[type];
        }
    }

    public static class IListExtensions
    {
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
        /// Uses newly created instance of the RandomGenerator
        /// </summary>
        /// <typeparam name="Titem"></typeparam>
        /// <param name="pickFrom"></param>
        /// <returns></returns>
        public static Titem PickRandom<Titem>(this IList<Titem> pickFrom)
        {
            RandomGenerator random = new RandomGenerator();
            return random.Pick(pickFrom);
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
