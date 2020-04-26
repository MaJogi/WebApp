using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Aids;
using WebApp.Data.Request;
using WebApp.Domain.Common;

namespace WebApp.Tests.Domain.Common
{
    [TestClass]
    public class EntityTests : AbstractClassTest<Entity<RequestData>, object>
    {
        private class testClass : Entity<RequestData>
        {
            public testClass(RequestData d = null) : base(d) { }

        }
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Obj = new testClass();
        }

        [TestMethod]
        public void DataTest()
        {
            var d = GetRandom.Object<RequestData>();
            Assert.AreNotSame(d, Obj.Data);
            Obj = new testClass(d);
            Assert.AreSame(d, Obj.Data);
        }

        [TestMethod]
        public void DataIsNullTest()
        {
            var d = GetRandom.Object<RequestData>();
            Assert.IsNotNull(Obj.Data);
            Obj.Data = d;
            Assert.AreSame(d, Obj.Data);
        }

        [TestMethod]
        public void CanSetNullDataTest()
        {
            Assert.IsNotNull(Obj.Data);
            Obj.Data = null;
            Assert.IsNull(Obj.Data);
        }

    }
}
