using HBD.Framework;
using HBD.Mef.Logging;
using HBD.Mef.StTests.TestObs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Composition;
using System.Linq;

namespace HBD.Mef.StTests
{
    [TestClass]
    public class BootstrapperTests
    {
        static readonly TestObs.TestBootstrapper b = new TestObs.TestBootstrapper();

        static BootstrapperTests()
        {
            b.Run();
        }

        [TestMethod]
        public void TestServiceLocator()
        {
            var p1 = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<TestObs.StartUp2>();
            Assert.IsNotNull(p1);
        }

        [TestMethod]
        public void TestManualExport_Plugin_Type()
        {
            var p1 = b.Container.GetExport<TestObs.StartUp2>();
            Assert.IsNotNull(p1);
        }

        [TestMethod]
        public void TestAutoExport_Plugin_Type()
        {
            var p1 = b.Container.GetExport<TestObs.StartUp1>();
            Assert.IsNotNull(p1);
        }

        [TestMethod]
        public void TestAutoExport_Plugin_Name()
        {
            var p1 = b.Container.GetExports<TestObs.IPlugin>();

            Assert.IsTrue(p1.Any());
        }

        [TestMethod]
        public void Able_To_Get_Logger()
        {
            var a = b.Container.GetExport<ILogger>();
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Able_To_Get_CompositionContainer()
        {
            var a = b.Container.GetExport<CompositionContext>();
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Able_To_Get_IServiceLocator()
        {
            var a = b.Container.GetExport<Microsoft.Practices.ServiceLocation.IServiceLocator>();
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Able_To_Get_Duy_Plugin()
        {
            var a = b.Container.GetExport<IPlugin>("Duy");

            Assert.IsNotNull(a);
            Assert.IsNotNull(a.PropertyValue("Container"));
            Assert.IsNotNull(a.PropertyValue("Logger"));
        }
    }
}
