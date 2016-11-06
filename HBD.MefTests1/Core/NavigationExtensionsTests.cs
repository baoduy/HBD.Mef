using System.Linq;
using HBD.Mef.Core.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Core.Tests
{
    [TestClass]
    public class NavigationExtensionsTests
    {
        [TestMethod]
        public void AddMenuItems_UsingFluentAPI()
        {
            var menuSet = new MenuInfoCollection();

            menuSet
                .Menu("Menu 1")
                .WithIcon("Menu 1")
                .WithToolTip("Menu 1")
                .Children
                .AddNavigation("Navigation 1")
                .WithIcon("Navigation 1")
                .WithToolTip("Navigation 1")
                .For(new ViewInfo(typeof(object)))
                .AndFor(new ActionNavigationParameter(() => { }))
                .AndNavigation("Navigation 2")
                .AndSeparator();

            Assert.IsTrue(menuSet.Count == 1);
            Assert.IsTrue(menuSet.OfType<MenuInfo>().First().Children.Count == 3);

            menuSet.Menu("Menu 1")
                .Children
                .AddMenu("Sub 1");

            Assert.IsTrue(menuSet.Menu("Menu 1").Children.Count == 4);

            Assert.IsTrue(menuSet.OfType<MenuInfo>().First()
                              .Children.Navigation("Navigation 1")
                              .NavigationParameters.Count == 2);
        }
    }
}