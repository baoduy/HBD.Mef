// <copyright file="ShellConfigManagerTest.cs" company="Duy Hoang">Copyright © Microsoft 2016</copyright>

using System;
using HBD.Mef.Shell.Configuration;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Shell.Configuration.Tests
{
    /// <summary>This class contains parameterized unit tests for ShellConfigManager</summary>
    [TestClassAttribute]
    [PexClass(typeof(ShellConfigManager))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ShellConfigManagerTest
    {
    }
}
