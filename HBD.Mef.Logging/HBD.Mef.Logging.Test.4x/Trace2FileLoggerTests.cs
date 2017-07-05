#region using

using System;
using System.IO;
using HBD.Mef.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HBD.MefTests.Core.Logging
{
    [TestClass]
    public class Trace2FileLoggerTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            HBD.Framework.IO.DirectoryEx.DeleteDirectories("Logs");
        }

        [TestMethod]
        public void Trace2File_Default_File_LoggerTest()
        {
            using (var log = new Trace2FileLogger())
            {
                log.Info("AA");

                Assert.IsTrue(File.Exists(log.DefaultOutFileName));
            }
        }

        [TestMethod]
        public void Trace2FileLogger_Custom_File_Test()
        {
            var file = "Logs\\Log_Trace2FileLogger_Trace2FileLoggerTest.log";
            using (var log = new Trace2FileLogger(file))
            {
                log.Info("AA");

                Assert.IsTrue(File.Exists(file));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DisposeTest()
        {
            var log = new Trace2FileLogger("Logs\\Log_Trace2FileLogger_DisposeTest.log");
            log.Dispose();
            log.Info("AA");
        }
    }
}