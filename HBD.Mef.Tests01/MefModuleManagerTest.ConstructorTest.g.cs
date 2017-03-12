using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Logging;
using Prism.Modularity;
using HBD.Mef.Modularity;
using Microsoft.Pex.Framework.Generated;
// <copyright file="MefModuleManagerTest.ConstructorTest.g.cs" company="Duy Hoang">Copyright © Microsoft 2016</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace HBD.Mef.Modularity.Tests
{
    public partial class MefModuleManagerTest
    {

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(MefModuleManagerTest))]
        [PexRaisedException(typeof(ArgumentNullException))]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorTestThrowsArgumentNullException81()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                MefModuleManager mefModuleManager;
                mefModuleManager = this.ConstructorTest
                                       ((IModuleInitializer)null, (IModuleCatalog)null, (ILoggerFacade)null);
                disposables.Add((IDisposable)mefModuleManager);
                disposables.Dispose();
            }
        }

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(MefModuleManagerTest))]
        [ExpectedExceptionAttribute(typeof(ArgumentNullException))]
        public void ConstructorTestThrowsArgumentNullException8101()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                MefModuleManager mefModuleManager;
                mefModuleManager = this.ConstructorTest
                                       ((IModuleInitializer)null, (IModuleCatalog)null, (ILoggerFacade)null);
                disposables.Add((IDisposable)mefModuleManager);
                disposables.Dispose();
            }
        }

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(MefModuleManagerTest))]
        [ExpectedExceptionAttribute(typeof(ArgumentNullException))]
        public void ConstructorTestThrowsArgumentNullException8102()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                MefModuleManager mefModuleManager;
                mefModuleManager = this.ConstructorTest
                                       ((IModuleInitializer)null, (IModuleCatalog)null, (ILoggerFacade)null);
                disposables.Add((IDisposable)mefModuleManager);
                disposables.Dispose();
            }
        }

        [TestMethodAttribute]
        [PexGeneratedBy(typeof(MefModuleManagerTest))]
        [ExpectedExceptionAttribute(typeof(ArgumentNullException))]
        public void ConstructorTestThrowsArgumentNullException8103()
        {
            using (PexDisposableContext disposables = PexDisposableContext.Create())
            {
                MefModuleManager mefModuleManager;
                mefModuleManager = this.ConstructorTest
                                       ((IModuleInitializer)null, (IModuleCatalog)null, (ILoggerFacade)null);
                disposables.Add((IDisposable)mefModuleManager);
                disposables.Dispose();
            }
        }
    }
}
