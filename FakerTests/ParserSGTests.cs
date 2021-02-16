using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System.Reflection;

namespace FakerTests
{
    [TestClass]
    public class ParserSGTests
    {
        [TestMethod]
        public void BasicTest()
        {
            string s = Data.GetFileText();
            Console.WriteLine(s);
        }
    }
}
