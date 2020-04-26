using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Data.Request;

namespace WebApp.Tests.Domain.Request
{
    [TestClass]
    public class RequestTests : SealedClassTest<WebApp.Domain.Request.Request, object>
    {
        protected object ObjL;

        [TestInitialize]
        public override void TestInitialize()
        {
            Obj= new WebApp.Domain.Request.Request();
            Type = typeof(WebApp.Domain.Request.Request);
        }

        public override void IsInheritedTest()
        {
            Assert.AreEqual(typeof(WebApp.Domain.Request.Request), Type.BaseType);
        }

        [TestMethod]
        public void CanCreateTest()
        {
            Assert.IsNotNull(Obj);
        }
    }
}
