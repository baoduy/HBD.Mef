using HBD.Mef;
using HBD.Mef.Common;
using HBD.Mef.Core.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.MefTests.Common
{
    [TestClass]
    public class ShellConfigManagerTests
    {
        [TestMethod]
        public void LoadModuleFromTest()
        {
            var module1 = ShellConfigManager.Modules;
            var module2 = ShellConfigManager.Modules;

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
            var originalTitle = ShellConfigManager.ShellConfig.Title;
            ShellConfigManager.ShellConfig.Title = "123";
            ShellConfigManager.Modules[0].IsEnabled = false;

            ShellConfigManager.SaveChanges();

            Assert.IsTrue(ShellConfigManager.Modules.ChangedItems.Count == 0);
            Assert.IsTrue(ShellConfigManager.Modules[0].IsEnabled == false);

            var savedShell = JsonConfigHelper.ReadConfig<ShellConfig>("Shell.json");
            Assert.AreEqual(ShellConfigManager.ShellConfig.Title, savedShell.Title);

            var savedModule = JsonConfigHelper.ReadConfig<ModuleConfig>("Modules\\HBD.App.Demo.Plugin\\Module_Demo.json");
            Assert.IsTrue(savedModule.IsEnabled == false);

            //Restore back to original values
            ShellConfigManager.ShellConfig.Title = originalTitle;
            ShellConfigManager.Modules[0].IsEnabled = true;
            ShellConfigManager.SaveChanges();
        }

        [TestMethod]
        public void UndoChangesTest()
        {
            var originalTitle = ShellConfigManager.ShellConfig.Title;
            ShellConfigManager.ShellConfig.Title = "123";

            Assert.AreNotEqual(originalTitle, ShellConfigManager.ShellConfig.Title);
            ShellConfigManager.UndoChanges();

            Assert.AreEqual(originalTitle, ShellConfigManager.ShellConfig.Title);

            var module = ShellConfigManager.Modules;
            Assert.IsTrue(module.Count > 0);

            module[0].IsEnabled = false;
            Assert.IsTrue(ShellConfigManager.Modules.ChangedItems.Count == 1);

            ShellConfigManager.UndoChanges();
            Assert.IsTrue(ShellConfigManager.Modules.ChangedItems.Count == 0);

            Assert.IsTrue(ShellConfigManager.Modules[0].IsEnabled);
        }
    }
}