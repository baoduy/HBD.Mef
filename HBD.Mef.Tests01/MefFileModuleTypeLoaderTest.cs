using Prism.Modularity;
using System;
using HBD.Mef.Modularity;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Modularity.Tests
{
    /// <summary>This class contains parameterized unit tests for MefFileModuleTypeLoader</summary>
    [TestClassAttribute]
    [PexClass(typeof(MefFileModuleTypeLoader))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class MefFileModuleTypeLoaderTest
    {

        /// <summary>Test stub for CanLoadModuleType(ModuleInfo)</summary>
        [PexMethod(MaxRunsWithoutNewTests = 200)]
        public bool CanLoadModuleTypeTest([PexAssumeUnderTest]MefFileModuleTypeLoader target, ModuleInfo moduleInfo)
        {
            bool result = target.CanLoadModuleType(moduleInfo);
            return result;
            // TODO: add assertions to method MefFileModuleTypeLoaderTest.CanLoadModuleTypeTest(MefFileModuleTypeLoader, ModuleInfo)
        }

        /// <summary>Test stub for LoadModuleType(ModuleInfo)</summary>
        [PexMethod(MaxRunsWithoutNewTests = 200, MaxConditions = 1000, MaxConstraintSolverTime = 2, Timeout = 240)]
        public void LoadModuleTypeTest([PexAssumeUnderTest]MefFileModuleTypeLoader target, ModuleInfo moduleInfo)
        {
            target.LoadModuleType(moduleInfo);
            // TODO: add assertions to method MefFileModuleTypeLoaderTest.LoadModuleTypeTest(MefFileModuleTypeLoader, ModuleInfo)
        }
    }
}
