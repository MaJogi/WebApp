using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Aids;

namespace WebApp.Tests
{
    [TestClass]
    public abstract class BaseTest<TClass, TBaseClass>
    {
        protected TClass Obj;
        protected Type Type;

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

        protected static void IsNullableProperty<T>(Func<T> get, Action<T> set)
        {
            IsProperty(get, set);
            set(default);
            Assert.IsNull(get());
        }

        protected static void IsProperty<T>(Func<T> get, Action<T> set)
        {
            var d = (T)GetRandom.Value(typeof(T));
            Assert.AreNotEqual(d, get());
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
}
