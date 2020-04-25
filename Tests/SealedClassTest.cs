using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApp.Tests
{
    [TestClass]
    public abstract class SealedClassTest<TClass, TBaseClass> : ClassTest<TClass, TBaseClass> where TClass : new()
    {
        [TestMethod]
        public void IsSealed()
        {
            Assert.IsTrue(Type.IsSealed);
        }
    }
}
