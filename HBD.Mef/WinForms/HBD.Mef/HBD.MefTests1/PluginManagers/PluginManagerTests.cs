using FluentAssertions;
using HBD.Mef;
using HBD.Mef.Modularity;
using HBD.MefTests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace HBD.MefTests.PluginManagers
{
    [TestClass]
    public class PluginManagerTests
    {
        [TestMethod]
        public void The_SystemModule_Wont_Be_Validated()
        {
            using (var b = new TestBootstrapper())
            {
                b.Run();

                var p = b.Container.GetExportedValue<IPluginManager>() as PluginManager;

                var t = p.ModuleActivationValidators.OfType<TestActivationValidator>().First();

                Assert.IsTrue(t.Count > 0);
            }
        }

        [TestMethod]
        public void Exported_And_Activated_Plugins_ShouldBe_TheSame()
        {
            var p = TestBootstrapper.Default.Container.GetExportedValue<IPluginManager>() as PluginManager;

            var activatedPlugins = p.Plugins.Count(a => a.State == PluginState.Initialized);
            var exportedPlugins = TestBootstrapper.Default.Container.GetExports<IPlugin>().Count();

            activatedPlugins.Should().Be(exportedPlugins);
        }

        [TestMethod]
        public void Get_ModuleInfo_Tests()
        {
            var p = TestBootstrapper.Default.Container.GetExportedValue<IPluginManager>() as PluginManager;
            var moduleInfos = p.GetModuleInfos().ToList();

            moduleInfos.Count.Should().Be(p.Plugins.Count);
        }
    }
}
