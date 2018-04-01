#region

using HBD.Mef.Mvc.Icons;
using HBD.Mef.Mvc.Navigation.NavigateInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HBD.Mef.Mvc.Navigation.Tests
{
    [TestClass]
    public class MenuInfoTests
    {
        [TestMethod]
        public void Create_MenuInfo_1()
        {
            var menu = new MenuInfo("Area1")
                .WithIcon(Glyphicon.glyphicon_adjust)
                .WithTitle("AAA")
                .DisplayAt(1)
                .DisplayIconOnly();

            Assert.AreEqual("Area1", menu.AreaName);
            Assert.AreEqual("AAA", menu.Title);
            Assert.AreEqual((ushort) 1, menu.DisplayIndex);
            Assert.AreEqual(MenuDisplayType.IconOnly, menu.DisplayType);
            Assert.AreEqual(MenuAlignment.Left, menu.Alignment);
            Assert.AreEqual(Glyphicon.glyphicon_adjust, menu.Icon);
        }

        [TestMethod]
        public void Create_MenuInfo_2()
        {
            var menu = new MenuInfo("Area1")
                .WithIcon("123")
                .AlignAtRight();

            Assert.AreEqual("Area1", menu.AreaName);
            Assert.IsNull(menu.Title);
            Assert.IsTrue(menu.DisplayIndex > 100);
            Assert.AreEqual(MenuDisplayType.IconAndText, menu.DisplayType);
            Assert.AreEqual(MenuAlignment.Right, menu.Alignment);
            Assert.AreEqual("123", menu.Icon);
        }
    }
}