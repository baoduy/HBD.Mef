#region using

#region using

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

#endregion

namespace HBD.Mef.Mvc.Services.Tests
{
    [TestClass]
    public class CustomBinaryManagerTests
    {
        [TestMethod]
        public void LoadAssemblies_IncludedSubFolder_Test()
        {
            var items = new CustomBinaryManager().LoadAssemblies("TestBinaries").IncludedSubFolder();

            Assert.IsTrue(!items.Any(a => a.FullName.Contains("HBD.Framework")));
            Assert.IsTrue(items.OfType<object>().Count() == 2);
        }

        [TestMethod]
        public void LoadAssemblies_Test()
        {
            var items = new CustomBinaryManager().LoadAssemblies("TestBinaries");

            Assert.IsTrue(!items.Any(a => a.FullName.Contains("HBD.Framework")));
            Assert.IsTrue(items.OfType<object>().Count() <= 1);
        }

        [TestMethod]
        public void LoadAssemblies_PatNotExisted_Test()
        {
            var items = new CustomBinaryManager().LoadAssemblies("AAA");

            Assert.IsTrue(!items.Any());
            Assert.IsTrue(!items.OfType<object>().Any());
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void LoadAssemblies_ChangesOptionWhenListLoaded_Test()
        {
            var items = new CustomBinaryManager().LoadAssemblies("TestBinaries");

            Assert.IsTrue(!items.Any());

            items.IncludedAppDomainAssemblies();
        }

        [TestMethod]
        public void FindTypeTest()
        {
            var manager = new CustomBinaryManager();
            manager.LoadAssemblies("TestBinaries").IncludedAppDomainAssemblies();

            Assert.IsTrue(manager.Any(a=>a.Any(m=>m.FullName.Contains("WebActivatorEx"))));

            var foundType = manager.FindType("WebActivatorEx.ActivationManager");
            Assert.IsNotNull(foundType);

            var notFoundType = manager.FindType("CustomBinaryManagerTests");
            Assert.IsNull(notFoundType);
        }
    }
}