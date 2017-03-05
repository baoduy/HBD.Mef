#region

using HBD.Mef.Common;
using HBD.Mef.Shell.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HBD.MefTests.Common
{
    [TestClass]
    public class ShellConfigManagerTests
    {
        private readonly ShellConfigManager<ShellConfig, ModuleConfig> manager =
            new ShellConfigManager<ShellConfig, ModuleConfig>();

        [TestMethod]
        public void LoadModuleFromTest()
        {
            var module1 = manager.Modules;
            var module2 = manager.Modules;

            Assert.AreEqual(module1, module2);
            Assert.AreEqual(module1.Count, 1);
            Assert.AreEqual(module1[0].Name, "Demo_Module");
            Assert.AreEqual(module1[0].Description, "Demo Plugin");
            Assert.AreEqual(module1[0].AssemplyFiles[0], "HBD.Shell.Plugin.Demo.dll");
            Assert.IsTrue(module1[0].IsEnabled);
        }

        [TestMethod]
        public void SaveChangesTest()
        {
            var originalTitle = manager.ShellConfig.Title;
            manager.ShellConfig.Title = "123";
            manager.Modules[0].IsEnabled = false;

            manager.SaveChanges();

            Assert.IsTrue(manager.Modules.ChangedItems.Count == 0);
            Assert.IsTrue(manager.Modules[0].IsEnabled == false);

            var savedShell = JsonConfigHelper.ReadConfig<ShellConfig>("Shell.json");
            Assert.AreEqual(manager.ShellConfig.Title, savedShell.Title);

            var savedModule = JsonConfigHelper.ReadConfig<ModuleConfig>("Modules\\HBD.App.Demo.Plugin\\Module_Demo.json");
            Assert.IsTrue(savedModule.IsEnabled == false);

            //Restore back to original values
            manager.ShellConfig.Title = originalTitle;
            manager.Modules[0].IsEnabled = true;
            manager.SaveChanges();
        }

        [TestMethod]
        public void UndoChangesTest()
        {
            var originalTitle = manager.ShellConfig.Title;
            manager.ShellConfig.Title = "123";

            Assert.AreNotEqual(originalTitle, manager.ShellConfig.Title);
            manager.UndoChanges();

            Assert.AreEqual(originalTitle, manager.ShellConfig.Title);

            var module = manager.Modules;
            Assert.IsTrue(module.Count > 0);

            module[0].IsEnabled = false;
            Assert.IsTrue(manager.Modules.ChangedItems.Count == 1);

            manager.UndoChanges();
            Assert.IsTrue(manager.Modules.ChangedItems.Count == 0);

            Assert.IsTrue(manager.Modules[0].IsEnabled);
        }
    }
}