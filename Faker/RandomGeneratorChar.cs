using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Faker
{
    public partial class RandomGenerator
    {
        /// <summary>
        ///generates a random char from interval[lower, upper] <br/>
        /// when lower/upper bound is not specified, char.MinValue/char.MaxValue is used 
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public char RandomChar(char lower = char.MinValue, char upper = char.MaxValue)
        {
            ushort randomUshort = this.RandomUshort(lower, upper);
            return (char)randomUshort;
        }
        /// <summary>
        /// generates a random char representing a digit
        /// </summary>
        /// <returns></returns>
        public char RandomDigit()
        {
            int digit = this.RandomInt(48, 57); //ASCII for digits
            return (char)digit;
        }
        /// <summary>
        /// generates a random char representing a lowercase letter
        /// </summary>
        /// <returns></returns>
        public char RandomLowerCaseLetter()
        {
            int ord = this.RandomInt(97, 122); //ASCII for lowercase
            return (char)ord;
        }
        /// <summary>
        /// generates a random char representing a uppercase letter
        /// </summary>
        /// <returns></returns>
        public char RandomUpperCaseLetter()
        {
            int ord = this.RandomInt(65, 90); //ASCII for uppercase
            return (char)ord;
        }
        /// <summary>
        /// generates a random char representing a Latin letter
        /// </summary>
        /// <returns></returns>
        public char RandomLetter()
        {
            int ord = this.RandomInt(65, 116);
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
        public char RandomAlphanumericChar()
        {
            int ord = this.RandomInt(55, 116); //interval large enough to contain all alphanum., shifted to beginning of upper case letters
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
        public char RandomAsciiChar()
        {
            int ord = this.RandomInt(0, 127);
            return (char)ord;
        }
        /// <summary>
        /// generates a random char representing a vowel <br/>
        /// including y
        /// </summary>
        /// <returns></returns>
        public char RandomVowel()
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u','y' };
            int index = this.RandomInt(0, vowels.Length-1);
            return vowels[index];
        }
        /// <summary>
        /// generates a random char representing a consonant <br/>
        /// excluding y
        /// </summary>
        /// <returns></returns>
        public char RandomConsonant()
        {
            //TODO: is Y vowel or consonant?
            char[] consonants = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z' };
            int index = this.RandomInt(0, consonants.Length - 1);
            return consonants[index];
        }
        //TODO: white/ non white random char?
        public string RandomString()
        {
            char[] c = this.RandomList<char>(255).ToArray();
            return new string(c);
        }
        public string RandomString(int length, bool precise = true)
        {
            int lengthToUse = this.CountToUse(length, precise);
            char[] c = this.RandomList<char>(lengthToUse).ToArray();
            return new string(c);
        }
        //TODO: which overloads of RandomString with respect to RandomCollections
    }
}
