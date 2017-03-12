using System;
using HBD.Mef.Modularity;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Modularity.Tests
{
    /// <summary>This class contains parameterized unit tests for ModuleExportAttribute</summary>
    [TestClassAttribute]
    [PexClass(typeof(ModuleExportAttribute))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ModuleExportAttributeTest
    {

        /// <summary>Test stub for .ctor(Type)</summary>
        [PexMethod]
        public ModuleExportAttribute ConstructorTest(Type moduleType)
        {
            ModuleExportAttribute target = new ModuleExportAttribute(moduleType);
            return target;
            // TODO: add assertions to method ModuleExportAttributeTest.ConstructorTest(Type)
        }

        /// <summary>Test stub for .ctor(String, Type)</summary>
        [PexMethod]
        public ModuleExportAttribute ConstructorTest01(string moduleName, Type moduleType)
        {
            ModuleExportAttribute target = new ModuleExportAttribute(moduleName, moduleType);
            return target;
            // TODO: add assertions to method ModuleExportAttributeTest.ConstructorTest01(String, Type)
        }
    }
}
