using HBD.Mef.Logging;
using HBD.Mef.MvcTests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Assert.IsTrue(System.IO.File.Exists("Logs\\Log_Log4NetLogger.log"));
        }
    }
}
