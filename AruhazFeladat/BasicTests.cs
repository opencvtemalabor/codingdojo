﻿using System;
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
            Assert.AreEqual(87, s.Eval("ELOSZIA"));
        }
    }
}
