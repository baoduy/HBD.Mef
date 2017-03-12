#region using

using System.Linq;
using HBD.Framework;
using HBD.Mef.Shell;
using HBD.Mef.Shell.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HBD.MefTests.Core
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
                .Menu("Sub 1");

            Assert.IsTrue(menuSet.Menu("Menu 1").Children.Count == 4);

            Assert.IsTrue(menuSet.OfType<MenuInfo>().First()
                              .Children.Navigation("Navigation 1")
                              .NavigationParameters.Count == 2);
        }

        [TestMethod]
        public void SetIndex_MenuItems_UsingFluentAPI()
        {
            var menuSet = new MenuInfoCollection();

            menuSet
                .Menu("Menu 1")
                .AndMenu("Menu 2")
                .AndMenu("Menu 3")
                .AndMenu("Menu 4");

            menuSet.Menu("Menu 4").DisplayAt(0);
            menuSet.Menu("Menu 1").DisplayAt(2);

            Assert.AreEqual(menuSet[0].GetValueFromProperty("Title"), "Menu 4");
            Assert.AreEqual(menuSet[2].GetValueFromProperty("Title"), "Menu 1");
        }
    }
}