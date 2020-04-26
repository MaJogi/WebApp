using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Aids;
using WebApp.Domain.Request;

namespace WebApp.Tests.Domain.Request
{
    [TestClass]
    public class RequestFactoryTests : ClassTest<RequestFactory, object>
    {
        [TestMethod]
        public void CreateTest()
        {
            //var testObject = GetRandom.Object(typeof(IncomingShipmentData));
            WebApp.Domain.Request.Request testObject = RequestFactory.Create(
                GetRandom.String()
            );
            testObject.Data.Id = GetRandom.String();
            IsNullableProperty(() => testObject.Data.Id, x => testObject.Data.Id = x);

            Assert.AreEqual(testObject.GetType(), RequestFactory.Create(
                GetRandom.String()).GetType());
        }
    }
}
