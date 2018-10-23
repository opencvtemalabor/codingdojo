using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AruhazFeladat
{
    [TestClass]
    public class BasicTests
    {
        Supermarket s = new Supermarket();

       [TestMethod]
        public void Instantiation()
        {
            Assert.AreEqual(0, s.Eval(""));
        }

        [TestMethod]
        public void BaseValues()
        {
            Assert.AreEqual(95, s.Eval("HELOSZIA"));
        }

        [TestMethod]
        public void IgnoreLowerCase()
        {
            Assert.AreEqual(87, s.Eval("hELOSZIA"));
        }

        [TestMethod]
        public void PayForTwoGetThree()
        {
            PayForTwoDiscount payForTwoDiscount = new PayForTwoDiscount();
            s.RegisterDiscount(payForTwoDiscount);
            payForTwoDiscount.AddAllItemsToPayForTwo();    
            Assert.AreEqual( 86, s.Eval("HELLOBELLOO"));
            Trace.WriteLine("{0}", s.Eval("HELLOBELLOO").ToString());
        }

        [TestMethod]
        public void PayForTwoGetThreeOnSpecificItems()
        {
            PayForTwoDiscount payForTwoDiscount = new PayForTwoDiscount();
            s.RegisterDiscount(payForTwoDiscount);
            payForTwoDiscount.AddAllItemsToPayForTwo();
            payForTwoDiscount.RemoveFromPayForTwo('A');
            Assert.AreNotEqual(2, s.Eval("AAA"));
        }

        [TestMethod]
        public void SpecialBundle()
        {
            s.RegisterDiscount(new BundleDiscount("ABC", 1));
            Assert.IsTrue(s.Eval("ABC") < s.Eval("AB") + s.Eval("C"));
        }

        [TestMethod]
        public void PreferPayForTwoGetThree()
        {
            PayForTwoDiscount payForTwoDiscount = new PayForTwoDiscount();
            s.RegisterDiscount(payForTwoDiscount);
            payForTwoDiscount.AddAllItemsToPayForTwo();
            s.RegisterDiscount(new BundleDiscount("ABC", 1));
            Assert.AreEqual(8, s.Eval("BBBAC"));
            Assert.AreEqual(9, s.Eval("CCCAB"));
        }
    }
}
