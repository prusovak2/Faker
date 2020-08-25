using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public static class DoubleFloatDecimalExtensions
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
}
