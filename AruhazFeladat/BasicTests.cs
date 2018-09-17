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
    }
}
