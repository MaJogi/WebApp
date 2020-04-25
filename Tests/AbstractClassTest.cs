using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApp.Tests
{
    [TestClass]
    public abstract class AbstractClassTest<TClass, TBaseClass> : BaseTest<TClass, TBaseClass>
    {
        [TestMethod]
        public void IsAbstract()
        {
            Assert.IsTrue(Type.IsAbstract);
        }
    }
}
