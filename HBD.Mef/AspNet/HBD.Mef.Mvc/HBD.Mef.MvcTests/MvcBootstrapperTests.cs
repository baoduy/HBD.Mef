#region

using HBD.Mef.MvcTests.Controllers;
using HBD.Mef.MvcTests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HBD.Mef.Mvc.Tests
{
    [TestClass]
    public class MvcBootstrapperTests
    {
        private static readonly TestBootatrapper Bootatrapper = new TestBootatrapper();

        static MvcBootstrapperTests()
        {
            Bootatrapper.Start();
        }

        [TestMethod]
        public void Test_Exported_Controller()
        {
            var c = Bootatrapper.Container.GetExportedValue<TestController>();

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void Test_NonExported_Controller()
        {
            var c = Bootatrapper.Container.GetExportedValue<NonExportedController>();

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void The_Exported_Controllers_AlwayNonShared()
        {
            var c1 = Bootatrapper.Container.GetExportedValue<TestController>();
            var c2 = Bootatrapper.Container.GetExportedValue<TestController>();

            Assert.AreNotEqual(c1, c2);
            Assert.IsNotNull(c2.Logger);

            var c3 = Bootatrapper.Container.GetExportedValue<NonExportedController>();
            var c4 = Bootatrapper.Container.GetExportedValue<NonExportedController>();

            Assert.AreNotEqual(c3, c4);
            Assert.IsNotNull(c4.Logger);
            //var c5 = Bootatrapper.Container.GetExportedValue<ExportAttributeOnlyController>();
            //var c6 = Bootatrapper.Container.GetExportedValue<ExportAttributeOnlyController>();

            //Assert.AreNotEqual(c5, c6);
        }

        [TestMethod]
        public void The_Exported_and_NonExported_MefAreaInterface_AlwayShared()
        {
            var c1 = Bootatrapper.Container.GetExportedValue<ExportingAreaRegistration>();
            var c2 = Bootatrapper.Container.GetExportedValue<ExportingAreaRegistration>();

            Assert.AreEqual(c1, c2);

            var c3 = Bootatrapper.Container.GetExportedValue<NonExportedAreaRegistration>();
            var c4 = Bootatrapper.Container.GetExportedValue<NonExportedAreaRegistration>();

            Assert.AreEqual(c3, c4);
        }
    }
}