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
            s.AddAllItemsToPayForTwo();    
            Assert.AreEqual( 86, s.Eval("HELLOBELLOO"));
            Trace.WriteLine("{0}", s.Eval("HELLOBELLOO").ToString());
        }

        [TestMethod]
        public void PayForTwoGetThreeOnSpecificItems()
        {
            s.AddAllItemsToPayForTwo();
            s.RemoveFromPayForTwo('A');
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
            s.AddAllItemsToPayForTwo();
            s.RegisterDiscount(new BundleDiscount("ABC", 1));
            Assert.AreEqual(8, s.Eval("BBBAC"));
            Assert.AreEqual(9, s.Eval("CCCAB"));
        }

        [TestMethod]
        public void LoyaltyPoints() {
            Assert.AreEqual(9, s.GetLoyaltyPoints("HELOSZIA"));
        }

        [TestMethod]
        public void LoyaltyPointsWhenPayForTwoDiscount()
        {
            PayForTwoDiscount payForTwoDiscount = new PayForTwoDiscount();
            s.RegisterDiscount(payForTwoDiscount);
            s.AddAllItemsToPayForTwo();
            Assert.AreEqual(7, s.GetLoyaltyPoints("ZZZ"));
        }

        [TestMethod]
        public void ForEveryTenLoyaltyPointsGetValueMinusOne()
        {
            s.AddAllItemsToPayForTwo();
            Assert.AreEqual(78, s.Eval("ZZZZ"));
            Assert.AreEqual(77, s.FinalPrice("ZZZZ"));
        }

        [TestMethod]
        public void AlmostExpired()
        {
            s.RegisterDiscount(new AlmostExpiredDiscount("HE"));
            Assert.AreEqual(46, s.Eval("HELLO"));
        }
    }

}
