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

            for (int i = 0; i < 15; i++)
            {
                string s = r.Person.Name();
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
        }

    }
}
