using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Aids;
using WebApp.Data.Request;
using WebApp.Domain.Request;
using WebApp.Infra;

namespace WebApp.Tests.Infra
{
    [TestClass]
    public class SortedRepositoryTests : AbstractClassTests<SortedRepository<Request, RequestData>, BaseRepository<Request, RequestData>>
    {
        private class TestClass : SortedRepository<Request, RequestData>
        {
            public TestClass(DbContext context, DbSet<RequestData> set) : base(context, set) { }

            protected override async Task<RequestData> getData(string id)
            {
                await Task.CompletedTask; // Märgitakse task lõpetatuks (completed).
                return new RequestData(); // See on esialgne, sest id'd hetkel ei kasutata.
            }

            protected internal override Request toDomainObject(RequestData d) => new Request(d);
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
           
            var options = new DbContextOptionsBuilder<RequestDbContext>().UseInMemoryDatabase("TestDb").Options;
            var context = new RequestDbContext(options); // See jäetakse Microsoft'i südametunnistusele, et kui ta töötab testandmebaasiga mälus, siis ta töötab ka reaalse andmebaasiga.
            Obj = new TestClass(context, context.Requests);
        }

        [TestMethod]
        public void SortOrderTest()
        {
            IsNullableProperty(() => Obj.SortOrder, x => Obj.SortOrder = x);
        }
        [TestMethod]
        public void DescendingStringTest()
        {

            var propertyName = GetMember.Name<TestClass>(x => x.DescendingString);
            IsReadOnlyProperty(Obj, propertyName, "_desc");
        }
        [TestMethod]
        public void setSortingTest()
        {
            void test(IQueryable<RequestData> d, string sortOrder)
            {
                Obj.SortOrder = sortOrder + Obj.DescendingString;
                var set = Obj.addSorting(d);
                Assert.IsNotNull(set);
                Assert.AreNotEqual(d, set);
                Assert.IsTrue(set.Expression.ToString()
                    .Contains($"WebApp.Data.Request.RequestData]).OrderByDescending(Param_0 => Convert(Param_0.{sortOrder}, Object))"));
                Obj.SortOrder = sortOrder;
                set = Obj.addSorting(d);
                Assert.IsNotNull(set);
                Assert.AreNotEqual(d, set);
                Assert.IsTrue(set.Expression.ToString().Contains($"WebApp.Data.Request.RequestData]).OrderBy(Param_0 => Convert(Param_0.{sortOrder}, Object))"));
            }

            Assert.IsNull(Obj.addSorting(null));

            IQueryable<RequestData> dataSet = Obj.dbSet;
            Obj.SortOrder = null;
            Assert.AreEqual(dataSet, Obj.addSorting(dataSet)); 


            test(dataSet, GetMember.Name<RequestData>(x => x.Id));
            test(dataSet, GetMember.Name<RequestData>(x => x.DeadLine));
            test(dataSet, GetMember.Name<RequestData>(x => x.Description));
            test(dataSet, GetMember.Name<RequestData>(x => x.EntryDate));
            test(dataSet, GetMember.Name<RequestData>(x => x.Solved));
        }
        [TestMethod]
        public void createExpressionTest()
        {
            string s;
            testCreateExpression(GetMember.Name<RequestData>(x => x.Id));
            testCreateExpression(GetMember.Name<RequestData>(x => x.DeadLine));
            testCreateExpression(GetMember.Name<RequestData>(x => x.Description));
            testCreateExpression(GetMember.Name<RequestData>(x => x.EntryDate));
            testCreateExpression(GetMember.Name<RequestData>(x => x.Solved));

            testCreateExpression(s = GetMember.Name<RequestData>(x => x.Id), s + Obj.DescendingString);
            testCreateExpression(s = GetMember.Name<RequestData>(x => x.DeadLine), s + Obj.DescendingString);
            testCreateExpression(s = GetMember.Name<RequestData>(x => x.Description), s + Obj.DescendingString);
            testCreateExpression(s = GetMember.Name<RequestData>(x => x.EntryDate), s + Obj.DescendingString);
            testCreateExpression(s = GetMember.Name<RequestData>(x => x.Solved), s + Obj.DescendingString);


            testNullExpression(GetRandom.String());
            testNullExpression(string.Empty);
            testNullExpression(null);


        }

        private void testNullExpression(string name)
        {
            Obj.SortOrder = name;
            var lambda = Obj.createExpression();
            Assert.IsNull(lambda);
        }

        private void testCreateExpression(string expected, string name = null)
        {
            name ??= expected; // Kui name osutub nulliks, siis asenda see expected'iga
            Obj.SortOrder = name;
            var lambda = Obj.createExpression();
            Assert.IsNotNull(lambda);
            Assert.IsInstanceOfType(lambda, typeof(Expression<Func<RequestData, object>>));
            Assert.IsTrue(lambda.ToString().Contains(expected));
        }

        [TestMethod]
        public void lambdaExpressionTest()
        {
            var name = GetMember.Name<RequestData>(x => x.Id); // Saadakse kätte property nimi.
            var property = typeof(RequestData).GetProperty(name); // saadakse kätte klass "PropertyInfo"
            var lambda = Obj.lambdaExpression(property);
            Assert.IsNotNull(lambda);
            Assert.IsInstanceOfType(lambda, typeof(Expression<Func<RequestData, object>>));
            Assert.IsTrue(lambda.ToString().Contains(name));
        }

        [TestMethod]
        public void findPropertyTest() // Todo: Tuleb ära testida ka kõik parent klassi property'd
        {
            string s;

            void test(PropertyInfo expected, string sortOrder)
            {
                Obj.SortOrder = sortOrder;
                Assert.AreEqual(expected, Obj.findProperty());
            }

            test(null, GetRandom.String());
            test(null, null);
            test(null, string.Empty);
            test(typeof(RequestData).GetProperty(s = GetMember.Name<RequestData>(x => x.Id)), s);
            test(typeof(RequestData).GetProperty(s = GetMember.Name<RequestData>(x => x.DeadLine)), s);
            test(typeof(RequestData).GetProperty(s = GetMember.Name<RequestData>(x => x.Description)), s);
            test(typeof(RequestData).GetProperty(s = GetMember.Name<RequestData>(x => x.EntryDate)), s);
            test(typeof(RequestData).GetProperty(s = GetMember.Name<RequestData>(x => x.Solved)), s);

            // Also descending ones
            test(typeof(RequestData).GetProperty(s = GetMember.Name<RequestData>(x => x.Id)),
                s + Obj.DescendingString);
            test(typeof(RequestData).GetProperty(s = GetMember.Name<RequestData>(x => x.DeadLine)),
                s + Obj.DescendingString);
            test(typeof(RequestData).GetProperty(s = GetMember.Name<RequestData>(x => x.Description)),
                s + Obj.DescendingString);
            test(typeof(RequestData).GetProperty(s = GetMember.Name<RequestData>(x => x.EntryDate)),
                s + Obj.DescendingString);
            test(typeof(RequestData).GetProperty(s = GetMember.Name<RequestData>(x => x.Solved)),
                s + Obj.DescendingString);
        }

        [TestMethod]
        public void getNameTest()
        {
            string s;

            void test(string expected, string sortOrder)
            {
                Obj.SortOrder = sortOrder;
                Assert.AreEqual(expected, Obj.getName());
            }
            test(s = GetRandom.String(), s);
            test(s = GetRandom.String(), s + Obj.DescendingString);
            test(s = string.Empty, string.Empty);
            test(s = string.Empty, null);
        }
        [TestMethod]
        public void setOrderByTest()
        {
            void test(IQueryable<RequestData> d, Expression<Func<RequestData, object>> e, string expected)
            {
                Obj.SortOrder = GetRandom.String() + Obj.DescendingString;
                var set = Obj.addOrderBy(d, e);
                Assert.IsNotNull(set);
                Assert.AreNotEqual(d, set);
                Assert.IsTrue(set.Expression.ToString()
                    .Contains($"WebApp.Data.Request.RequestData]).OrderByDescending({expected})"));
                Obj.SortOrder = GetRandom.String();
                set = Obj.addOrderBy(d, e);
                Assert.IsNotNull(set);
                Assert.AreNotEqual(d, set);
                Assert.IsTrue(set.Expression.ToString().Contains($"WebApp.Data.Request.RequestData]).OrderBy({expected})"));
            }

            Assert.IsNull(Obj.addOrderBy(null, null));
            IQueryable<RequestData> dataSet = Obj.dbSet;
            Assert.AreEqual(dataSet, Obj.addOrderBy(dataSet, null));


            test(dataSet, x => x.Id, "x => x.Id");

            test(dataSet, x => x.DeadLine, "x => Convert(x.Deadline, Object)");
            test(dataSet, x => x.EntryDate, "x => Convert(x.EntryDate, Object)");
            test(dataSet, x => x.Solved, "x => Convert(x.Solved, Object)");
            test(dataSet, x => x.Description, "x => x.Description");
            
        }
        [TestMethod]
        public void isDecendingTest()
        {
            void test(string sortOrder, bool expected)
            {
                Obj.SortOrder = sortOrder;
                Assert.AreEqual(expected, Obj.isDecending());
            }

            test(GetRandom.String(), false);
            test(GetRandom.String() + Obj.DescendingString, true);
            test(string.Empty, false);
            test(null, false);

        }

    }
}
