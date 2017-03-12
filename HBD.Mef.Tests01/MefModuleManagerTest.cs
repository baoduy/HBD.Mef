using System.Collections.Generic;
using Prism.Logging;
using Prism.Modularity;
using System;
using HBD.Mef.Modularity;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Modularity.Tests
{
    /// <summary>This class contains parameterized unit tests for MefModuleManager</summary>
    [TestClassAttribute]
    [PexClass(typeof(MefModuleManager))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class MefModuleManagerTest
    {

        /// <summary>Test stub for .ctor(IModuleInitializer, IModuleCatalog, ILoggerFacade)</summary>
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public MefModuleManager ConstructorTest(
            IModuleInitializer moduleInitializer,
            IModuleCatalog moduleCatalog,
            ILoggerFacade loggerFacade
        )
        {
            MefModuleManager target = new MefModuleManager(moduleInitializer, moduleCatalog, loggerFacade);
            return target;
            // TODO: add assertions to method MefModuleManagerTest.ConstructorTest(IModuleInitializer, IModuleCatalog, ILoggerFacade)
        }

        /// <summary>Test stub for OnImportsSatisfied()</summary>
        [PexMethod]
        public void OnImportsSatisfiedTest([PexAssumeUnderTest]MefModuleManager target)
        {
            target.OnImportsSatisfied();
            // TODO: add assertions to method MefModuleManagerTest.OnImportsSatisfiedTest(MefModuleManager)
        }

        /// <summary>Test stub for get_ModuleTypeLoaders()</summary>
        [PexMethod]
        public IEnumerable<IModuleTypeLoader> ModuleTypeLoadersGetTest([PexAssumeUnderTest]MefModuleManager target)
        {
            IEnumerable<IModuleTypeLoader> result = target.ModuleTypeLoaders;
            return result;
            // TODO: add assertions to method MefModuleManagerTest.ModuleTypeLoadersGetTest(MefModuleManager)
        }

        /// <summary>Test stub for set_ModuleTypeLoaders(IEnumerable`1&lt;IModuleTypeLoader&gt;)</summary>
        [PexMethod]
        public void ModuleTypeLoadersSetTest([PexAssumeUnderTest]MefModuleManager target, IEnumerable<IModuleTypeLoader> value)
        {
            target.ModuleTypeLoaders = value;
            // TODO: add assertions to method MefModuleManagerTest.ModuleTypeLoadersSetTest(MefModuleManager, IEnumerable`1<IModuleTypeLoader>)
        }
    }
}
