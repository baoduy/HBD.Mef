#region

using System.IO;
using System.Linq;
using HBD.Framework.IO;
using HBD.Mef.Shell.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HBD.Mef.Common.Tests
{
    [TestClass]
    public class JsonConfigHelperTests
    {
        [TestMethod]
        public void ReadConfigTest()
        {
            var shell1 = JsonConfigHelper.ReadConfig<ShellConfig>("Shell.json");
            var shell2 = JsonConfigHelper.ReadConfig<ShellConfig>("Shell.json");

            Assert.AreEqual(shell1, shell2);
            Assert.AreEqual(shell1.Title, "HBD Window Forms Shell");
            Assert.AreEqual(shell1.Logo, "Resources\\Shell.ico");
            Assert.AreEqual(shell1.Environment, "DEV");
            Assert.AreEqual(shell2.ModulePath, "./Modules");
        }

        [TestMethod]
        public void SaveConfigTest()
        {
            Directory.CreateDirectory("TestData\\");
            DirectoryEx.DeleteFiles("TestData\\", "Shell.Testing*.*");

            var shell = new ShellConfig
            {
                Title = "Hoang Bao Duy",
                Logo = "HBD"
            };
            //Save the object to Json file.

            const string fileName = "TestData\\Shell.Testing.json";
            JsonConfigHelper.SaveConfig(shell, fileName);

            var savedShell = JsonConfigHelper.ReadConfig<ShellConfig>(fileName);

            Assert.AreEqual(shell.Title, savedShell.Title);
            Assert.AreEqual(shell.Logo, savedShell.Logo);

            //Save File again. the old file should be backed up.
            JsonConfigHelper.SaveConfig(shell, fileName);

            Assert.IsTrue(File.Exists(fileName));
            Assert.IsTrue(Directory.GetFiles(Path.GetDirectoryName(fileName), "Shell.Testing_*.back").Any());
        }

        [TestMethod]
        public void ReadConfig_WithObj_Test()
        {
            var shell = new ShellConfig();
            JsonConfigHelper.ReadConfig(shell, "Shell.json");

            Assert.AreEqual(shell.Title, "HBD Window Forms Shell");
            Assert.AreEqual(shell.Logo, "Resources\\Shell.ico");
            Assert.AreEqual(shell.Environment, "DEV");
            Assert.AreEqual(shell.ModulePath, "./Modules");
        }
    }
}