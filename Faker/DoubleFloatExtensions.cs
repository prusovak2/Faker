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
    }
}
