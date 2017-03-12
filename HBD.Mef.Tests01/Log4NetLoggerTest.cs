using Prism.Logging;
// <copyright file="Log4NetLoggerTest.cs" company="Duy Hoang">Copyright © Microsoft 2016</copyright>

using System;
using HBD.Mef.Logging;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Logging.Tests
{
    /// <summary>This class contains parameterized unit tests for Log4NetLogger</summary>
    [TestClassAttribute]
    [PexClass(typeof(Log4NetLogger))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class Log4NetLoggerTest
    {

        /// <summary>Test stub for .ctor(String)</summary>
        [PexMethod]
        public Log4NetLogger ConstructorTest(string outputFile)
        {
            Log4NetLogger target = new Log4NetLogger(outputFile);
            return target;
            // TODO: add assertions to method Log4NetLoggerTest.ConstructorTest(String)
        }

        /// <summary>Test stub for Log(String, Category, Priority)</summary>
        [PexMethod]
        public void LogTest(
            [PexAssumeUnderTest]Log4NetLogger target,
            string message,
            Category category,
            Priority priority
        )
        {
            target.Log(message, category, priority);
            // TODO: add assertions to method Log4NetLoggerTest.LogTest(Log4NetLogger, String, Category, Priority)
        }
    }
}
