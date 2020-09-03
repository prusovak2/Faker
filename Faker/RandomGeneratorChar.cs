using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Faker
{
    public partial class RandomGenerator
    {
        public class RandomChar
        {
            /// <summary>
            /// reference to instance of Random generator that has reference to this instance of RandomChar
            /// </summary>
            internal RandomGenerator RG { get; }
            public RandomChar(RandomGenerator rg)
            {
                this.RG = rg;
            }
            /// <summary>
            ///generates a random char from interval[lower, upper] <br/>
            /// when lower/upper bound is not specified, char.MinValue/char.MaxValue is used 
            /// <param name="lower"></param>
            /// <param name="upper"></param>
            /// <returns></returns>
            public char Char(char lower = char.MinValue, char upper = char.MaxValue)
            {
                ushort randomUshort = this.RG.Random.Ushort(lower, upper);
                return (char)randomUshort;
            }
            /// <summary>
            /// generates a random char representing a digit
            /// </summary>
            /// <returns></returns>
            public char Digit()
            {
                int digit = this.RG.Random.Int(48, 57); //ASCII for digits
                return (char)digit;
            }
            /// <summary>
            /// generates a random char representing a lowercase letter
            /// </summary>
            /// <returns></returns>
            public char LowerCaseLetter()
            {
                int ord = this.RG.Random.Int(97, 122); //ASCII for lowercase
                return (char)ord;
            }
            /// <summary>
            /// generates a random char representing a uppercase letter
            /// </summary>
            /// <returns></returns>
            public char UpperCaseLetter()
            {
                int ord = this.RG.Random.Int(65, 90); //ASCII for uppercase
                return (char)ord;
            }
            /// <summary>
            /// generates a random char representing a Latin letter
            /// </summary>
            /// <returns></returns>
            public char Letter()
            {
                int ord = this.RG.Random.Int(65, 116);
                if (ord > 90) //shift lowercase to ASCII range for lowercase
                {
                    ord += 6;
                }
                return (char)ord;
            }
            /// <summary>
            /// generates a random letter or digit
            /// </summary>
            /// <returns></returns>
            public char AlphaNumeric()
            {
                int ord = this.RG.Random.Int(55, 116); //interval large enough to contain all alphanum., shifted to beginning of upper case letters
                if (ord > 90) //shift lowercase to ASCII range for lowercase
                {
                    ord += 6;
                }
                else if (ord < 65) //shift digit to ASCII range for digit
                {
                    ord -= 7;
                }
                return (char)ord;
            }
            /// <summary>
            /// generates a random char from ASCII range
            /// </summary>
            /// <returns></returns>
            public char Ascii()
            {
                int ord = this.RG.Random.Int(0, 127);
                return (char)ord;
            }
            /// <summary>
            /// generates a random char representing a vowel <br/>
            /// including y
            /// </summary>
            /// <returns></returns>
            public char Vowel()
            {
                char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
                int index = this.RG.Random.Int(0, vowels.Length - 1);
                return vowels[index];
            }
            /// <summary>
            /// generates a random char representing a consonant <br/>
            /// excluding y
            /// </summary>
            /// <returns></returns>
            public char Consonant()
            {
                //TODO: is Y vowel or consonant?
                char[] consonants = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z' };
                int index = this.RG.Random.Int(0, consonants.Length - 1);
                return consonants[index];
            }
            /// <summary>
            /// returns a random char representing a hexadecimal digit 
            /// </summary>
            /// <returns></returns>
            public char HexadecimalDigit()
            {
                char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
                int index = this.RG.Random.Int(0, hexDigits.Length - 1);
                return hexDigits[index];
            }
            /// <summary>
            /// returns a random ASCII whitespace char <br/>
            /// as ASCII whitespaces are regarded space(32), horizontal tab(9), line feed(10), vertical tab(11), form feed(12), carriage return(13)
            /// </summary>
            /// <returns></returns>
            public char AsciiWhitespace()
            {
                //https://stackoverflow.com/questions/18169006/all-the-whitespace-characters-is-it-language-independent list of ASCII whitespaces
                int[] whiteSpaces = { 32, 9, 10, 11, 12, 13 };
                int index = this.RG.Random.Int(0, whiteSpaces.Length - 1);
                return (char)whiteSpaces[index];
            }

        }
       
    }
}
