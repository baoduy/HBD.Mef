using HBD.Framework.IO;
using HBD.Mef.Common;
using HBD.Mef.Core.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace HBD.Framework.Shell.Tests
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
            System.IO.Directory.CreateDirectory("TestData\\");
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
    }
}