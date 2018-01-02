using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Logging.Test
{
    [TestClass]
    public class Log4NetTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            Directory.Delete("Logs", true);
        }

        [TestMethod]
        public void Log4Net_Default_File_LoggerTest()
        {
            var file = string.Empty;
            using (var log = new Log4NetLogger())
            {
                log.Info("AA");

                file = log.DefaultOutFileName;

                Assert.IsTrue(File.Exists(log.DefaultOutFileName));
            }

            Assert.IsTrue(File.ReadAllText(file).Contains("AA"));
        }

        [TestMethod]
        public void Log4NetLogger_Custom_File_Test()
        {
            var file = "Logs\\Log_Log4NetLogger_Log4NetLoggerTest.log";
            using (var log = new Log4NetLogger(file))
            {
                log.Info("BB");
            }

            Assert.IsTrue(File.ReadAllText(file).Contains("BB"));
        }

        [TestMethod]
        public void DisposeTest()
        {
            var file = "Logs\\Log_Log4NetLogger_DisposeTest.log";
            var log = new Log4NetLogger(file);
            log.Info("Duy");
            log.Dispose();
        }
    }
}