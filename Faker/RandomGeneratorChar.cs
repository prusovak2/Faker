using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Faker
{
    public partial class RandomGenerator
    {
        public char RandomChar(char lower = char.MinValue, char upper = char.MaxValue)
        {
            ushort randomUshort = this.RandomUshort(lower, upper);
            return (char)randomUshort;
        }
        public char RandomDigit()
        {
            int digit = this.RandomInt(48, 57); //ascii for digits
            return (char)digit;
        }
        public char RandomLowerCaseLetter()
        {
            int ord = this.RandomInt(97, 122); //ascii for lower case
            return (char)ord;
        }
        public char RandomUpperCaseLetter()
        {
            int ord = this.RandomInt(65, 90); //ascii for upper case
            return (char)ord;
        }
        public char RandomLetter()
        {
            int ord = this.RandomInt(65, 116);
            if (ord > 90) //shift lowercase to ascii range for lowercase
            {
                ord += 6;
            }
            return (char)ord;
        }
        public char RandomAlphanumericChar()
        {
            int ord = this.RandomInt(55, 116); //interval large enough to contain all alphanum., shifted to beginning of upper case letters
            if (ord > 90) //shift lowercase to ascii range for lowercase
            {
                ord += 6;
            }
            else if (ord < 65) //shift digit to ascii range for digit
            {
                ord -= 7;
            }
            return (char)ord;
        }
        public char RandomAsciiChar()
        {
            int ord = this.RandomInt(0, 127);
            return (char)ord;
        }
            
        public char RandomVowel()
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u','y' };
            int index = this.RandomInt(0, vowels.Length-1);
            return vowels[index];
        }
        public char RandomConsonant()
        {
            //TODO: is Y vowel or consonant?
            char[] consonants = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z' };
            int index = this.RandomInt(0, consonants.Length - 1);
            return consonants[index];
        }
        public string RandomString()
        {
            throw new NotImplementedException();
        }
    }
}
