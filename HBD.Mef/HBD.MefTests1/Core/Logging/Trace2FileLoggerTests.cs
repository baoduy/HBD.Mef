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
        [TestMethod]
        public void Trace2FileLoggerTest()
        {
            using (var log = new Trace2FileLogger())
            {
                log.Info("AA");

                Assert.IsTrue(File.Exists(log.DefaultOutFileName));
            }
        }

        //[TestMethod()]
        //public void LogTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DisposeTest()
        {
            var log = new Trace2FileLogger();
            log.Dispose();
            log.Info("AA");
        }
    }
}