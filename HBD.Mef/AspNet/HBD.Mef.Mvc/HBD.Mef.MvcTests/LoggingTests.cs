#region

using System.IO;
using HBD.Mef.Logging;
using HBD.Mef.MvcTests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HBD.Mef.MvcTests
{
    [TestClass]
    public class LoggingTests
    {
        private static readonly TestBootatrapper Bootatrapper = new TestBootatrapper();

        [TestMethod]
        public void Logger_Test()
        {
            Bootatrapper.Logger.Info("Hoang Bao Duy");

            Assert.IsTrue(File.Exists("Logs\\Log_Log4NetLogger.log"));
        }
    }
}