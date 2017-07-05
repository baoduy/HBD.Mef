#region using

using HBD.Mef;
using HBD.Mef.Shell.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HBD.MefTests.Common
{
    [TestClass]
    public class ShellConfigManagerTests
    {
        private readonly IShellConfigManager _manager = new ShellConfigManager();

        [TestMethod]
        public void LoadModuleFromTest()
        {
            var module1 = _manager.Modules;
            var module2 = _manager.Modules;

            Assert.AreEqual(module1, module2);
            Assert.IsTrue(module1.Count > 1);
            Assert.AreEqual(module1[0].Name, "Demo_Module");
            Assert.AreEqual(module1[0].Description, "Demo Plugin");
            Assert.AreEqual(module1[0].AssemplyFiles[0], "HBD.Shell.Plugin.Demo.dll");
            Assert.IsTrue(module1[0].IsEnabled);
        }

        [TestMethod]
        public void SaveChangesTest()
        {
            var originalTitle = _manager.ShellConfig.Title;
            _manager.ShellConfig.Title = "123";
            _manager.Modules[0].IsEnabled = false;

            _manager.SaveChanges();

            Assert.IsTrue(_manager.Modules.ChangedItems.Count == 0);
            Assert.IsTrue(_manager.Modules[0].IsEnabled == false);

            var savedShell = JsonConfigHelper.ReadConfig<ShellConfig>("Shell.json");
            Assert.AreEqual(_manager.ShellConfig.Title, savedShell.Title);

            var savedModule = JsonConfigHelper.ReadConfig<ModuleConfig>("Modules\\HBD.App.Demo.Plugin\\Module_Demo.json");
            Assert.IsTrue(savedModule.IsEnabled == false);

            //Restore back to original values
            _manager.ShellConfig.Title = originalTitle;
            _manager.Modules[0].IsEnabled = true;
            _manager.SaveChanges();
        }

        [TestMethod]
        public void UndoChangesTest()
        {
            var originalTitle = _manager.ShellConfig.Title;
            _manager.ShellConfig.Title = "123";

            Assert.AreNotEqual(originalTitle, _manager.ShellConfig.Title);
            _manager.UndoChanges();

            Assert.AreEqual(originalTitle, _manager.ShellConfig.Title);

            var module = _manager.Modules;
            Assert.IsTrue(module.Count > 0);

            module[0].IsEnabled = false;
            Assert.IsTrue(_manager.Modules.ChangedItems.Count == 1);

            _manager.UndoChanges();
            Assert.IsTrue(_manager.Modules.ChangedItems.Count == 0);

            Assert.IsTrue(_manager.Modules[0].IsEnabled);
        }
    }
}