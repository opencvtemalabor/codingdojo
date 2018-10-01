using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AruhazFeladat
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void Instantiation()
        {
            var s = new Supermarket();
            Assert.AreEqual(0, s.Eval(""));
        }

        [TestMethod]
        public void BaseValues()
        {
            var s = new Supermarket();
            Assert.AreEqual(95, s.Eval("HELOSZIA"));
        }

        [TestMethod]
        public void IgnoreLowerCase()
        {
            var s = new Supermarket();
            Assert.AreEqual(87, s.Eval("hELOSZIA"));
        }

        [TestMethod]
        public void PayForTwoGetThree()
        {
            var s = new Supermarket();
            Assert.AreEqual( 86, s.Eval("HELLOBELLOO"));
        }

        [TestMethod]
        public void PayForTwoGetThreeOnSpecificItems()
        {
            var s = new Supermarket();
            Assert.AreNotEqual(2, s.Eval("AAA"));
        }

        [TestMethod]
        public void SpecialBundle()
        {
            var s = new Supermarket();
            Assert.IsTrue(s.Eval("ABC") < s.Eval("AB") + s.Eval("C"));
        }
    }
}
