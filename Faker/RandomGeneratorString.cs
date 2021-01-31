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
            public string LowerCaseLetters(int length, bool precise=true)
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
            public string UpperCaseLetters(int length, bool precise=true)
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
            public string Letters(int length, bool precise= true)
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
            /// generates a random string consisting of alphanumeric characters <br/>
            /// when precise is true, length is used as length of string <br/>
            /// otherwise a random int less or equal to length is generated and used as length of string
            /// </summary>
            /// <param name="length"></param>
            /// <param name="precise"></param>
            /// <returns></returns>
            public string AlphaNumeric(int length, bool precise=true)
            {
                char[] chars = this.RG.GenericList<char>(this.RG.Char.AlphaNumeric, length, precise).ToArray();
                return new string(chars);
            }
            /// <summary>
            /// returns a string hexadecimal interpretation of random number from interval [lower,upper] <br/>
            /// format param determines whether it is lower or upper case and whether prefix is 0x, 0X or none
            /// </summary>
            /// <param name="lower">lower bound for a numeric value represented by s returned string</param>
            /// <param name="upper">upper bound for a numeric value represented by s returned string</param>
            /// <param name="format"> determines whether digits are lower or upper case and whether prefix is 0x, 0X or none</param>
            /// <returns></returns>
            public string HexadecimalString(ulong lower, ulong upper, HexadecimalFormat format = HexadecimalFormat.Upper0x)
            {
                ulong randomLong = this.RG.Random.Ulong(lower, upper);
                string prefixlessHexString;
                switch (format)
                {
                    case HexadecimalFormat.Upper0x:
                    case HexadecimalFormat.Upper0X:
                    case HexadecimalFormat.UpperPrefixless:
                        {
                            prefixlessHexString = randomLong.ToString("X");
                            break;
                        }
                    case HexadecimalFormat.Lower0x:
                    case HexadecimalFormat.Lower0X:
                    case HexadecimalFormat.LowerPrefixless:
                        {
                            prefixlessHexString = randomLong.ToString("x");
                            break;
                        }
                    default:
                        throw new NotImplementedException("Unexpected");

                }
                return AddPrexix(prefixlessHexString, format);

            }
            /// <summary>
            /// returns a string representation of a hexadecimal number, that has numDigits digits (when precise is true)<br/>
            /// otherwise numDigits is used as upper bound for a random number of hexadecimal digits <br/>
            /// format param determines whether it is lower or upper case and whether prefix is 0x, 0X or none
            /// </summary>
            /// <param name="numDigits">maximal number of hexadecimal digits in return string</param>
            /// <param name="precise">is numDigit used precisely or used as the upper bound for number of digits in returned string</param>
            /// <param name="format"> determines whether digits are lower or upper case and whether prefix is 0x, 0X or none</param>
            /// <returns></returns>
            public string HexadecimalString(int numDigits, bool precise = true, HexadecimalFormat format = HexadecimalFormat.Upper0x)
            {
                char[] prefixlessHexChars;
                switch (format)
                {
                    case HexadecimalFormat.Upper0x:
                    case HexadecimalFormat.Upper0X:
                    case HexadecimalFormat.UpperPrefixless:
                        {
                            prefixlessHexChars = this.RG.GenericCollection<char>(this.RG.Char.HexadecimalDigitUpper, numDigits, precise).ToArray();
                            break;
                        }
                    case HexadecimalFormat.Lower0x:
                    case HexadecimalFormat.Lower0X:
                    case HexadecimalFormat.LowerPrefixless:
                        {
                            prefixlessHexChars = this.RG.GenericCollection<char>(this.RG.Char.HexadecimalDigitLower, numDigits, precise).ToArray();
                            break;
                        }
                    default:
                        throw new NotImplementedException("Unexpected");

                }
                return AddPrexix( new string(prefixlessHexChars) , format);
            }
            /// <summary>
            /// adds 0x or 0X prefix or nothing in front of given string according to given HexadecimalFormat  
            /// </summary>
            /// <param name="prefixlessHexString"></param>
            /// <param name="format"></param>
            /// <returns></returns>
            internal string AddPrexix(string prefixlessHexString, HexadecimalFormat format)
            {
                switch (format)
                {
                    case HexadecimalFormat.UpperPrefixless:
                    case HexadecimalFormat.LowerPrefixless:
                        {
                            return prefixlessHexString;
                        }
                    case HexadecimalFormat.Upper0x:
                    case HexadecimalFormat.Lower0x:
                        {
                            return "0x" + prefixlessHexString;
                        }
                    case HexadecimalFormat.Upper0X:
                    case HexadecimalFormat.Lower0X:
                        {
                            return "0X" + prefixlessHexString;
                        }
                    default:
                        throw new NotImplementedException("Unexpected");
                }
            }
            public enum HexadecimalFormat
            {
                Lower0x,
                Upper0x,
                Lower0X,
                Upper0X,
                LowerPrefixless,
                UpperPrefixless
            }

        }
    }
}
