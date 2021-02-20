using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System.Globalization;
using System.Reflection;

namespace FakerTests
{
    [TestClass]
    public class ParserSGTests
    {
        [TestMethod]
        public void BasicTest()
        {
            RandomGenerator r = new();
            Console.WriteLine("NAME, DEFAULT CULTURE");
            for (int i = 0; i < 15; i++)
            {
                string s = r.Person.Name();
                Console.WriteLine(s);
            }
            Console.WriteLine("NAME, es-SP");
            for (int i = 0; i < 15; i++)
            {
                string s = r.Person.Name(PersonNameCulture.es_SP);
                Console.WriteLine(s);
            }
            Console.WriteLine("***************EN******************");
            for (int i = 0; i < 15; i++)
            {
                string s = r.Address.Country();
                Console.WriteLine(s);
            }
            Console.WriteLine("***************ES******************");
            RandomGenerator r2 = new RandomGenerator(new CultureInfo("es-SP"));
            for (int i = 0; i < 15; i++)
            {
                string s = r2.Address.City();
                Console.WriteLine(s);
            }
        }

        [TestMethod]
        public void CultureInfoExtensionTest()
        {
            CultureInfo info = new CultureInfo("en-US");

            string sgFriendlyName = info.SGFriendlyName();
            Console.WriteLine(sgFriendlyName);
            Assert.AreEqual("en_US", sgFriendlyName);

            string twoLetterName = info.TwoLetterISOLanguageName;
            Console.WriteLine(twoLetterName);
            Assert.AreEqual("en", twoLetterName);
            
        }

    }
}
