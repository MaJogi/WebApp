using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Aids;

namespace WebApp.Tests
{
    [TestClass]
    public abstract class BaseClassTest<TClass, TBaseClass> : BaseTests
    {
        protected TClass Obj;
        protected Type Type;
        private List<string> members { get; set; }
        private const string notTested = "<{0}> is not tested";
        private const string notSpecified = "Class is not specified";

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Type = typeof(TClass);
        }

        [TestMethod]
        public virtual void IsInheritedTest()
        {
            Assert.AreEqual(typeof(TBaseClass), Type.BaseType);
        }

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

        protected static void IsNullableProperty<T>(Func<T> get, Action<T> set)
        {
            IsProperty(get, set);
            set(default);
            Assert.IsNull(get());
        }

        protected static void IsProperty<T>(Func<T> get, Action<T> set)
        {
            var d = (T)GetRandom.Value(typeof(T));
            if (typeof(T) != typeof(bool) )
            {
                Assert.AreNotEqual(d, get());
            }
            set(d);
            Assert.AreEqual(d, get());
        }

        protected static void IsReadOnlyProperty(object o, string name, object expected)
        {
            var property = o.GetType().GetProperty(name);
            Assert.IsNotNull(property);
            Assert.IsFalse(property.CanWrite);
            Assert.IsTrue(property.CanRead);
            var actual = property.GetValue(o);
            Assert.AreEqual(expected, actual);
        }
    }

    public class BaseTests
    {

    }
}