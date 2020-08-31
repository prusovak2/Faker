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
        public char Char(char lower = char.MinValue, char upper = char.MaxValue)
        {
            ushort randomUshort = this.Ushort(lower, upper);
            return (char)randomUshort;
        }
        /// <summary>
        /// generates a random char representing a digit
        /// </summary>
        /// <returns></returns>
        public char Digit()
        {
            int digit = this.Int(48, 57); //ASCII for digits
            return (char)digit;
        }
        /// <summary>
        /// generates a random char representing a lowercase letter
        /// </summary>
        /// <returns></returns>
        public char LowerCaseLetter()
        {
            int ord = this.Int(97, 122); //ASCII for lowercase
            return (char)ord;
        }
        /// <summary>
        /// generates a random char representing a uppercase letter
        /// </summary>
        /// <returns></returns>
        public char UpperCaseLetter()
        {
            int ord = this.Int(65, 90); //ASCII for uppercase
            return (char)ord;
        }
        /// <summary>
        /// generates a random char representing a Latin letter
        /// </summary>
        /// <returns></returns>
        public char Letter()
        {
            int ord = this.Int(65, 116);
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
            int ord = this.Int(55, 116); //interval large enough to contain all alphanum., shifted to beginning of upper case letters
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
            int ord = this.Int(0, 127);
            return (char)ord;
        }
        /// <summary>
        /// generates a random char representing a vowel <br/>
        /// including y
        /// </summary>
        /// <returns></returns>
        public char Vowel()
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u','y' };
            int index = this.Int(0, vowels.Length-1);
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
            int index = this.Int(0, consonants.Length - 1);
            return consonants[index];
        }
        /// <summary>
        /// returns a random char representing a hexadecimal digit 
        /// </summary>
        /// <returns></returns>
        public char HexadecimalDigit()
        {
            char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            int index = this.Int(0, hexDigits.Length - 1);
            return hexDigits[index];
        }
        //TODO: white/ non white random char?

        /// <summary>
        /// generates a random string 255 characters long
        /// </summary>
        /// <returns></returns>
        public string String()
        {
            //parameterless overload necessary as default random function for string
            char[] chars = this.RandomList<char>(255).ToArray();
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
            char[] chars = this.RandomList<char>(length, precise).ToArray();
            return new string(chars);
        }
        /// <summary>
        /// generates a random string 255 characters long consisting of lowercase letters
        /// </summary>
        /// <returns></returns>
        public string LowerCaseString()
        {
            char[] chars = this.RandomList<char>(this.LowerCaseLetter, 255, true).ToArray();
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
        public string LowerCaseString(int length, bool precise)
        {
            char[] chars = this.RandomList<char>(this.LowerCaseLetter,length, precise).ToArray();
            return new string(chars);
        }
        /// <summary>
        /// generates a random string 255 characters long consisting of uppercase letters
        /// </summary>
        /// <returns></returns>
        public string UpperCaseLetters()
        {
            char[] chars = this.RandomList<char>(this.UpperCaseLetter, 255, true).ToArray();
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
            char[] chars = this.RandomList<char>(this.UpperCaseLetter, length, precise).ToArray();
            return new string(chars);
        }
        /// <summary>
        /// generates a random string 255 characters long consisting of Latin letters (lower+uppercase)
        /// </summary>
        /// <returns></returns>
        public string Letters()
        {
            char[] chars = this.RandomList<char>(this.Letter, 255, true).ToArray();
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
            char[] chars = this.RandomList<char>(this.Letter, length, precise).ToArray();
            return new string(chars);
        }
        /// <summary>
        /// generates a random string 255 characters long consisting of alphanumeric characters
        /// </summary>
        /// <returns></returns>
        public string AlphaNumericString() //TODO: rename to AlphaNumeric
        {
            char[] chars = this.RandomList<char>(this.AlphaNumeric, 255, true).ToArray();
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
        public string AlphaNumericString(int length, bool precise)
        {
            char[] chars = this.RandomList<char>(this.AlphaNumeric, length, precise).ToArray();
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
            ulong randomLong = this.Ulong(lower, upper);
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
            char[] chars = this.RandomCollection<char>(this.HexadecimalDigit, numDigits, precise).ToArray();
            return new string(chars);
        }

    }
}
