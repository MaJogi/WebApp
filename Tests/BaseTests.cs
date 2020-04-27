using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Aids;

namespace WebApp.Tests
{
    public class BaseTests
    {
        private List<string> members { get; set; }
        private const string notTested = "<{0}> is not tested";
        private const string notSpecified = "Class is not specified";
        protected Type Type;

        [TestMethod]
        public void IsTested()
        {
            if (Type == null) Assert.Inconclusive(notSpecified);
            var m = GetClass.Members(Type, PublicBindingFlagsFor.DeclaredMembers);
            members = m.Select(e => e.Name).ToList();
            removeTested();

            if (members.Count == 0) return;
            Assert.Fail(notTested, members[0]);
        }

        private void removeTested()
        {
            var tests = GetType().GetMembers().Select(e => e.Name).ToList();

            for (var i = members.Count; i > 0; i--)
            {
                var m = members[i - 1] + "Test";
                var isTested = tests.Find(o => o == m);

                if (string.IsNullOrEmpty(isTested)) continue;
                members.RemoveAt(i - 1);
            }
        }
        protected static void testArePropertyValuesEqual(object obj1, object obj2)
        {
            List<string> exceptionList = new List<string> { "ExpiringOrHasExpired" };
            foreach (var property in obj1.GetType().GetProperties())
            {
                var name = property.Name;
                var p = obj2.GetType().GetProperty(name);
                if (!exceptionList.Contains(name))
                {
                    Assert.IsNotNull(p);
                    var expected = property.GetValue(obj1);
                    var actual = p.GetValue(obj2);
                    Assert.AreEqual(expected, actual);
                }
            }
        }
    }
}