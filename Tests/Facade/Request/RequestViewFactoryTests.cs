using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Aids;
using WebApp.Data.Request;
using WebApp.Facade.Request;

namespace WebApp.Tests.Facade.Request
{
    [TestClass]
    public class RequestViewFactoryTests : BaseTests
    {
        [TestInitialize]
        public virtual void TestInitialize()
        {
            Type = typeof(RequestViewFactory);
        }

        [TestMethod]
        public void CreateTest()
        {
            var view = GetRandom.Object<RequestView>();
            var data = RequestViewFactory.Create(view).Data;

            testArePropertyValuesEqual(view, data);

        }

        [TestMethod]
        public void CreateObjectTest()
        {
            var view = GetRandom.Object<RequestView>();
            var data = RequestViewFactory.Create(view).Data;

            testArePropertyValuesEqual(view, data);

        }

        [TestMethod]
        public void CreateViewTest()
        {
            var data = GetRandom.Object<RequestData>();
            var view = RequestViewFactory.Create(new WebApp.Domain.Request.Request(data));

            testArePropertyValuesEqual(data, view);



        }
    }
}
