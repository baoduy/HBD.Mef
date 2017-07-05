using HBD.Mef.Logging;
using HBD.Mef.Modularity;
using HBD.Mef.Shell.Services;
using HBD.MefTests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace HBD.MefTests
{
    [TestClass]
    public class BootstrapperTests
    {
        static readonly TestBootstrapper b = new TestBootstrapper();

        static BootstrapperTests()
        {
            b.Run();
        }

        [TestMethod]
        public void TestAutoExport_Plugin_Type()
        {
            var p1 = b.Container.GetExportedValue<StartUp1>();
            Assert.IsNotNull(p1);
        }

        [TestMethod]
        public void TestAutoExport_Plugin_Name()
        {
            var p1 = b.Container.GetExportedValues<IPlugin>();

            Assert.AreEqual(3, p1.Count());
        }

        [TestMethod]
        public void TestAutoExport_From_DepenLib()
        {
            var p = b.Container.GetExportedValue<IShellMenuService>();
            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void Test_PluginManager()
        {
            var p = b.Container.GetExportedValue<IPluginManager>() as PluginManager;

            Assert.IsNotNull(p);
            Assert.AreEqual(3, p.Plugins.Count);
            Assert.IsTrue(p.Plugins.Any(a=>a.ModuleName== "StartupTestModule"));
            Assert.AreEqual(3, p.ExportedModules.Count);
        }

        [TestMethod]
        public void Able_To_Get_Logger()
        {
            var a = b.Container.GetExportedValue<ILogger>();
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Able_To_Get_AggregateCatalog()
        {
            var a = b.Container.GetExportedValue<AggregateCatalog>();
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Able_To_Get_CompositionContainer()
        {
            var a = b.Container.GetExportedValue<CompositionContainer>();
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Able_To_Get_ICompositionService()
        {
            var a = b.Container.GetExportedValue<ICompositionService>();
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Able_To_Get_IServiceLocator()
        {
            var a = b.Container.GetExportedValue<Microsoft.Practices.ServiceLocation.IServiceLocator>();
            Assert.IsNotNull(a);
        }
    }
}
