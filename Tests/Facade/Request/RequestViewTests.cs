using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Facade.Request;

namespace WebApp.Tests.Facade.Request
{
    [TestClass]
    public class RequestViewTests : SealedClassTests<RequestView, object> // Pärast view'de refaktoorimist tuleks siin objekt muuta view'ks
    {
        

        [TestMethod]
        public void IdTest()
        {
            IsNullableProperty(() => Obj.Id, x => Obj.Id = x);
        }

        [TestMethod]
        public void DescriptionTest()
        {
            IsNullableProperty(() => Obj.Description, x => Obj.Description = x);
        }

        [TestMethod]
        public void EntryDateTest()
        {
            IsNullableProperty(() => Obj.EntryDate, x => Obj.EntryDate = x);
        }

        [TestMethod]
        public void DeadLineTest()
        {
            IsNullableProperty(() => Obj.DeadLine, x => Obj.DeadLine = x);
        }


        [TestMethod]
        public void SolvedTest()
        {
            IsProperty(() => Obj.Solved, x => Obj.Solved = x);
        }


        [TestMethod]
        public void ExpiringOrHasExpiredTest()
        {
            IsProperty(() => Obj.ExpiringOrHasExpired, x => Obj.ExpiringOrHasExpired = x);
        }
    }
}
