using Prism.Logging;
using System;
using HBD.Mef.Logging;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Logging.Tests
{
    /// <summary>This class contains parameterized unit tests for LoggingExtensions</summary>
    [TestClassAttribute]
    [PexClass(typeof(LoggingExtensions))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class LoggingExtensionsTest
    {

        /// <summary>Test stub for CreateExceptionString(Exception, String)</summary>
        [PexMethod(MaxRunsWithoutNewTests = 200)]
        internal string CreateExceptionStringTest(Exception @this, string indent)
        {
            string result = LoggingExtensions.CreateExceptionString(@this, indent);
            return result;
            // TODO: add assertions to method LoggingExtensionsTest.CreateExceptionStringTest(Exception, String)
        }

        /// <summary>Test stub for Log(ILoggerFacade, Exception)</summary>
        [PexMethod]
        public void LogTest(ILoggerFacade @this, Exception exception)
        {
            LoggingExtensions.Log(@this, exception);
            // TODO: add assertions to method LoggingExtensionsTest.LogTest(ILoggerFacade, Exception)
        }

        /// <summary>Test stub for Debug(ILoggerFacade, String)</summary>
        [PexMethod]
        public void DebugTest(ILoggerFacade @this, string message)
        {
            LoggingExtensions.Debug(@this, message);
            // TODO: add assertions to method LoggingExtensionsTest.DebugTest(ILoggerFacade, String)
        }

        /// <summary>Test stub for Info(ILoggerFacade, String)</summary>
        [PexMethod]
        public void InfoTest(ILoggerFacade @this, string message)
        {
            LoggingExtensions.Info(@this, message);
            // TODO: add assertions to method LoggingExtensionsTest.InfoTest(ILoggerFacade, String)
        }

        /// <summary>Test stub for Warn(ILoggerFacade, String)</summary>
        [PexMethod]
        public void WarnTest(ILoggerFacade @this, string message)
        {
            LoggingExtensions.Warn(@this, message);
            // TODO: add assertions to method LoggingExtensionsTest.WarnTest(ILoggerFacade, String)
        }

        /// <summary>Test stub for Exception(ILoggerFacade, String)</summary>
        [PexMethod]
        public void ExceptionTest(ILoggerFacade @this, string message)
        {
            LoggingExtensions.Exception(@this, message);
            // TODO: add assertions to method LoggingExtensionsTest.ExceptionTest(ILoggerFacade, String)
        }

        /// <summary>Test stub for Exception(ILoggerFacade, Exception)</summary>
        [PexMethod]
        public void ExceptionTest(ILoggerFacade @this, Exception exception)
        {
            LoggingExtensions.Exception(@this, exception);
            // TODO: add assertions to method LoggingExtensionsTest.ExceptionTest(ILoggerFacade, Exception)
        }
    }
}
