using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Logging;
// <copyright file="LoggingExtensionsTest.InfoTest.g.cs" company="Duy Hoang">Copyright © Microsoft 2016</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace HBD.Mef.Logging.Tests
{
    public partial class LoggingExtensionsTest
    {

[TestMethodAttribute]
[PexGeneratedBy(typeof(LoggingExtensionsTest))]
public void InfoTest353()
{
    this.InfoTest((ILoggerFacade)null, (string)null);
}

[TestMethodAttribute]
[PexGeneratedBy(typeof(LoggingExtensionsTest))]
public void InfoTest228()
{
    DebugLogger s0 = new DebugLogger();
    this.InfoTest((ILoggerFacade)s0, (string)null);
}

[TestMethodAttribute]
[PexGeneratedBy(typeof(LoggingExtensionsTest))]
public void InfoTest931()
{
    EmptyLogger s0 = new EmptyLogger();
    this.InfoTest((ILoggerFacade)s0, (string)null);
}
    }
}
