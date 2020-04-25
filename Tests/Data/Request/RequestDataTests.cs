using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Data.Common;
using WebApp.Data.Request;

namespace WebApp.Tests.Data.Request
{
    [TestClass]
    public class RequestDataTests : ClassTest<RequestData, UniqueEntityData>
    {
        private class TestClass : RequestData { }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            Obj = new TestClass();
        }

        [TestMethod]
        public void DescriptionTest()
        {
            IsProperty(() => Obj.Description, x => Obj.Description = x);
        }

        [TestMethod]
        public void EntryDateTest()
        {
            IsProperty(() => Obj.EntryDate, x=> Obj.EntryDate = x);
        }

        [TestMethod]
        public void DeadlineTest()
        {
            IsProperty(() => Obj.Deadline, x => Obj.Deadline = x);
        }

        [TestMethod]
        public void SolvedTest()
        {
            IsProperty(() => Obj.Solved, x => Obj.Solved = x);
        }


    }
}
