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
        public void SaveChangesTest()
        {
            var originalTitle = _manager.ShellConfig.Title;
            _manager.ShellConfig.Title = "123";

            _manager.SaveChanges();

            var savedShell = JsonConfigHelper.ReadConfig<ShellConfig>("Shell.json");
            Assert.AreEqual(_manager.ShellConfig.Title, savedShell.Title);

            //Restore back to original values
            _manager.ShellConfig.Title = originalTitle;
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

            _manager.UndoChanges();
        }
    }
}