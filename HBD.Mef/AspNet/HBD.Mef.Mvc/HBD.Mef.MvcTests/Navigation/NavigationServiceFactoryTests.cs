#region

using HBD.Mef.Mvc.Navigation;
using HBD.Mef.MvcTests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HBD.Mef.MvcTests.Navigation
{
    [TestClass]
    public class NavigationServiceFactoryTests
    {
        private static readonly TestBootatrapper Bootatrapper = new TestBootatrapper();

        static NavigationServiceFactoryTests()
        {
            Bootatrapper.Start();
        }

        [TestMethod]
        public void Test_NavigationFactory()
        {
            var factory = Bootatrapper.Container.GetExportedValue<INavigationServiceFactory>();
            Assert.IsNotNull(factory);
        }

        [TestMethod]
        public void Test_Ensure_NavigationFactory_IsSingleton()
        {
            var factory1 = Bootatrapper.Container.GetExportedValue<INavigationServiceFactory>();
            Assert.IsNotNull(factory1);

            var factory2 = Bootatrapper.Container.GetExportedValue<INavigationServiceFactory>();
            Assert.IsNotNull(factory2);

            Assert.AreEqual(factory1, factory2);
        }

        [TestMethod]
        public void Test_Create_Navigation()
        {
            var factory = Bootatrapper.Container.GetExportedValue<INavigationServiceFactory>();
            var nav = factory.CreateNavigationService("A");

            Assert.IsNotNull(nav);
        }

        [TestMethod]
        public void Test_Ensure_Navigation_IsNotSingleton_DifferenceArea()
        {
            var factory = Bootatrapper.Container.GetExportedValue<INavigationServiceFactory>();
            var nav1 = factory.CreateNavigationService("A");
            var nav2 = factory.CreateNavigationService("B");

            Assert.IsNotNull(nav1);
            Assert.IsNotNull(nav2);

            Assert.AreNotEqual(nav1, nav2);
        }

        [TestMethod]
        public void Test_Ensure_Navigation_IsSingleton_TheSameArea()
        {
            var factory = Bootatrapper.Container.GetExportedValue<INavigationServiceFactory>();
            var nav1 = factory.CreateNavigationService("A");
            var nav2 = factory.CreateNavigationService("A");

            Assert.AreEqual(nav1, nav2);
        }
    }
}