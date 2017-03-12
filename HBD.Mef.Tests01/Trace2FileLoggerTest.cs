using Prism.Logging;
// <copyright file="Trace2FileLoggerTest.cs" company="Duy Hoang">Copyright © Microsoft 2016</copyright>

using System;
using HBD.Mef.Logging;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Logging.Tests
{
    /// <summary>This class contains parameterized unit tests for Trace2FileLogger</summary>
    [TestClassAttribute]
    [PexClass(typeof(Trace2FileLogger))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class Trace2FileLoggerTest
    {

        /// <summary>Test stub for .ctor(String)</summary>
        [PexMethod(MaxRunsWithoutNewTests = 200)]
        [PexAllowedException(typeof(ArgumentException))]
        public Trace2FileLogger ConstructorTest(string outputFile)
        {
            Trace2FileLogger target = new Trace2FileLogger(outputFile);
            return target;
            // TODO: add assertions to method Trace2FileLoggerTest.ConstructorTest(String)
        }

        /// <summary>Test stub for Log(String, Category, Priority)</summary>
        [PexMethod]
        public void LogTest(
            [PexAssumeUnderTest]Trace2FileLogger target,
            string message,
            Category category,
            Priority priority
        )
        {
            target.Log(message, category, priority);
            // TODO: add assertions to method Trace2FileLoggerTest.LogTest(Trace2FileLogger, String, Category, Priority)
        }
    }
}
