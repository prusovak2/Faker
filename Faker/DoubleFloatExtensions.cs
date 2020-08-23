using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public static class DoubleFloatExtensions
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
            //Console.WriteLine(epsilon);
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
        public static bool EpsilonEquals(this decimal a, decimal b)
        {
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

            //TODO: this will throw System.OverflowException for too large interval - can it be solved in some better way than by try-catch block?
            if (Math.Abs(a - b) <= epsilon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool EpsilonEquals(this decimal a, decimal b, out bool RangeTooLarge)
        {
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
            //TODO: this will throw System.OverflowException for too large interval - can it be solved in some better way than by try-catch block?
            if (Math.Abs(a - b) <= epsilon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static bool RangeTooLarge(this decimal a, decimal b)
        {
            decimal upperBound = (decimal.MaxValue / 2) ;
            decimal lowerBound = (upperBound-1) * -1;
            decimal test = Math.Abs(upperBound - lowerBound);
            if ((a < lowerBound && b >= 0) || (b < lowerBound && a >= 0) || (a > upperBound && b <= 0) || (b > upperBound && a <= 0))
            {
                return true;
            }
            return false;
        }
    }
}
