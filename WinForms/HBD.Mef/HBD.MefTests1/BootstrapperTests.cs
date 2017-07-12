using HBD.Mef.Logging;
using HBD.Mef.Modularity;
using HBD.Mef.Shell.Services;
using HBD.MefTests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace HBD.MefTests
{
    [TestClass]
    public class BootstrapperTests
    {
        [TestMethod]
        public void TestAutoExport_Plugin_Type()
        {
            var p1 = TestBootstrapper.Default.Container.GetExportedValue<StartUp1>();
            Assert.IsNotNull(p1);
        }

        [TestMethod]
        public void TestAutoExport_Plugin_Name()
        {
            var p1 = TestBootstrapper.Default.Container.GetExportedValues<IPlugin>();

            Assert.IsTrue(p1.Count() >= 3);
        }

        [TestMethod]
        public void TestAutoExport_From_DepenLib()
        {
            var p = TestBootstrapper.Default.Container.GetExportedValue<IShellMenuService>();
            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void Test_PluginManager()
        {
            var p = TestBootstrapper.Default.Container.GetExportedValue<IPluginManager>() as PluginManager;

            Assert.IsNotNull(p);
            Assert.IsTrue(p.Plugins.Count >= 3);
            Assert.IsTrue(p.Plugins.Any(a => a.ModuleName == "StartupTestModule"));
            Assert.IsTrue(p.ExportedModules.Count >= 3);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Test_PluginManager_RunAgain()
        {
            var p = TestBootstrapper.Default.Container.GetExportedValue<IPluginManager>() as PluginManager;
            p.Run();
        }

        [TestMethod]
        public void Able_To_Get_Logger()
        {
            var a = TestBootstrapper.Default.Container.GetExportedValue<ILogger>();
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Able_To_Get_AggregateCatalog()
        {
            var a = TestBootstrapper.Default.Container.GetExportedValue<AggregateCatalog>();
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Able_To_Get_CompositionContainer()
        {
            var a = TestBootstrapper.Default.Container.GetExportedValue<CompositionContainer>();
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Able_To_Get_ICompositionService()
        {
            var a = TestBootstrapper.Default.Container.GetExportedValue<ICompositionService>();
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Able_To_Get_IServiceLocator()
        {
            var a = TestBootstrapper.Default.Container.GetExportedValue<Microsoft.Practices.ServiceLocation.IServiceLocator>();
            Assert.IsNotNull(a);
        }
    }
}
