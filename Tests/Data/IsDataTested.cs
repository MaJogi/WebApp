using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApp.Tests.Data
{
    [TestClass]
    public class IsDataTested : AssemblyTests
    {

        private const string assembly = "WebApp.Data";

        protected override string Namespace(string name) { return $"{assembly}.{name}"; }

        [TestMethod] 
        public void IsCommonTested() { isAllTested(assembly, Namespace("Common")); }

        [TestMethod]
        public void IsRequestTested()
        {
            isAllTested(assembly, Namespace("Request"));
        }

        //[TestMethod] 
        //public void IsMoneyTested() { isAllTested(assembly, Namespace("Money")); }



        [TestMethod] 
        public void IsTested() { isAllTested(base.Namespace("Data")); }

    }
}
