using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faker
{
    public partial class RandomGenerator
    {
        public class RandomString
        {
            /// <summary>
            /// reference to instance of Random generator that has reference to this instance of RandomString
            /// </summary>
            internal RandomGenerator RG { get; }
            public RandomString(RandomGenerator rg)
            {
                this.RG = rg;
            }

            /// <summary>
            /// generates a random string 255 characters long
            /// </summary>
            /// <returns></returns>
            public string String()
            {
                //parameterless overload necessary as default random function for string
                char[] chars = this.RG.GenericList<char>(255).ToArray();
                return new string(chars);
            }
            /// <summary>
            /// generates a random string, when precise is true, length is used as length of string<br/>
            /// otherwise a random int less or equal to length is generated and used as length of string
            /// </summary>
            /// <param name="length"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public string String(int length, bool precise = true)
            {
                char[] chars = this.RG.GenericList<char>(length, precise).ToArray();
                return new string(chars);
            }
            /// <summary>
            /// generates a random string 255 characters long consisting of lowercase letters
            /// </summary>
            /// <returns></returns>
            public string LowerCaseLetters()
            {
                char[] chars = this.RG.GenericList<char>(this.RG.Char.LowerCaseLetter, 255, true).ToArray();
                return new string(chars);
            }
            /// <summary>
            /// generates a random string consisting of lowercase letters<br/>
            /// when precise is true, length is used as length of string<br/>
            /// otherwise a random int less or equal to length is generated and used as length of string
            /// </summary>
            /// <param name="length"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public string LowerCaseLetters(int length, bool precise)
            {
                char[] chars = this.RG.GenericList<char>(this.RG.Char.LowerCaseLetter, length, precise).ToArray();
                return new string(chars);
            }
            /// <summary>
            /// generates a random string 255 characters long consisting of uppercase letters
            /// </summary>
            /// <returns></returns>
            public string UpperCaseLetters()
            {
                char[] chars = this.RG.GenericList<char>(this.RG.Char.UpperCaseLetter, 255, true).ToArray();
                return new string(chars);
            }
            /// <summary>
            /// generates a random string consisting of lowercase letters<br/>
            /// when precise is true, length is used as length of string<br/>
            /// otherwise a random int less or equal to length is generated and used as length of string
            /// </summary>
            /// <param name="length"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public string UpperCaseLetters(int length, bool precise)
            {
                char[] chars = this.RG.GenericList<char>(this.RG.Char.UpperCaseLetter, length, precise).ToArray();
                return new string(chars);
            }
            /// <summary>
            /// generates a random string 255 characters long consisting of Latin letters (lower+uppercase)
            /// </summary>
            /// <returns></returns>
            public string Letters()
            {
                char[] chars = this.RG.GenericList<char>(this.RG.Char.Letter, 255, true).ToArray();
                return new string(chars);
            }
            /// <summary>
            /// generates a random string consisting of Latin letters<br/>
            /// when precise is true, length is used as length of string<br/>
            /// otherwise a random int less or equal to length is generated and used as length of string
            /// </summary>
            /// <param name="length"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public string Letters(int length, bool precise)
            {
                char[] chars = this.RG.GenericList<char>(this.RG.Char.Letter, length, precise).ToArray();
                return new string(chars);
            }
            /// <summary>
            /// generates a random string 255 characters long consisting of alphanumeric characters
            /// </summary>
            /// <returns></returns>
            public string AlphaNumeric() 
            {
                char[] chars = this.RG.GenericList<char>(this.RG.Char.AlphaNumeric, 255, true).ToArray();
                return new string(chars);
            }
            /// <summary>
            /// generates a random string consisting of alphanumeric characters<br/>
            /// when precise is true, length is used as length of string<br/>
            /// otherwise a random int less or equal to length is generated and used as length of string
            /// </summary>
            /// <param name="length"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public string AlphaNumeric(int length, bool precise)
            {
                char[] chars = this.RG.GenericList<char>(this.RG.Char.AlphaNumeric, length, precise).ToArray();
                return new string(chars);
            }
            /// <summary>
            /// returns a string hexadecimal interpretation of random number from interval [lower,upper]
            /// </summary>
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public string HexadecimalString(ulong lower, ulong upper)
            {
                ulong randomLong = this.RG.Random.Ulong(lower, upper);
                string hexString = randomLong.ToString("X");
                return hexString;
            }
            /// <summary>
            /// returns a string representation of a hexadecimal number, that has numDigits digits (when precise is true)<br/>
            /// otherwise numDigits is used as upper bound for a random number of hexadecimal digits
            /// </summary>
            /// <param name="numDigits"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public string HexadecimalString(int numDigits, bool precise)
            {
                char[] chars = this.RG.GenericCollection<char>(this.RG.Char.HexadecimalDigit, numDigits, precise).ToArray();
                return new string(chars);
            }
        }
    }
}
