using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;


namespace FakerTests
{
    [TestClass]
    public class RandomGeneratorDoubleTests
    {
        [TestMethod]
        public void DoubleEpsiloEqualTest()
        {
            // Initialize two doubles with apparently identical values
            double double1 = 0.3333333;
            double double2 = 1d / 3d;
            bool b = double1.EpsilonEquals(double2);
            Assert.IsTrue(b);

            double1 = .33333;
            double2 = 1d / 4d;
            b = double1.EpsilonEquals(double2);
            Assert.IsFalse(b);

            double1 = .33333;
            double2 = 99999999999999990000000.1;
            b = double1.EpsilonEquals(double2);
            Assert.IsFalse(b);
        }
    }
}
