﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApp.Tests.Domain
{
    [TestClass]
    public class IsDomainTested : AssemblyTests
    {
        private const string assembly = "WebApp.Domain";

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
        public void IsTested() { isAllTested(base.Namespace("Domain")); }
    }
}
