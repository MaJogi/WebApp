using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApp.Tests
{
    [TestClass]
    public abstract class ClassTest<TClass, TBaseClass> : BaseClassTest<TClass, TBaseClass> where TClass : new()
    {

        [TestInitialize]
        public override void TestInitialize()
        {
            Obj = new TClass();
            Type = Obj.GetType();
        }

        [TestMethod]
        public void CanCreateTest()
        {
            Assert.IsNotNull(Obj);
        }


    }
}
