#region

using HBD.Mef.Mvc;
using HBD.Mef.Mvc.Navigation.NavigateInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

#endregion

namespace HBD.Mef.MvcTests.Navigation
{
    [TestClass]
    public class NavigationCloneTests
    {
        [TestMethod]
        public void Clone_MenuInfo_Navigation_Test()
        {
            var menu = new MenuInfo("A")
            {
                DisplayIndex = 1,
                Alignment = MenuAlignment.Left,
                DisplayType = MenuDisplayType.IconAndText,
                Icon = "123",
                Title = "1111"
            };

            menu.Items.Add(new NavigationInfo("123")
            {
                DisplayIndex = 12,
                Roles = {"123"},
                ReguireAuthorized = true,
                Alignment = MenuAlignment.Right,
                DisplayType = MenuDisplayType.IconOnly,
                Icon = "111",
                Title = "BBB",
                Action = "AAA",
                Controller = "BBB"
            });

            var newItem = menu.Clone();

            Assert.AreNotEqual(newItem, menu);
            Assert.AreEqual(JsonConvert.SerializeObject(newItem, Formatting.None),
                JsonConvert.SerializeObject(menu, Formatting.None));
        }

        [TestMethod]
        public void Clone_Link_Test()
        {
            var menu = new LinkInfo("A")
            {
                DisplayIndex = 1,
                Alignment = MenuAlignment.Left,
                DisplayType = MenuDisplayType.IconAndText,
                Icon = "123",
                Title = "1111",
                Roles = {"Aaaa"},
                ReguireAuthorized = true,
                Link = "http://abc.com"
            };

            var newItem = (LinkInfo) menu.Clone();

            Assert.AreNotEqual(newItem, menu);
            Assert.AreEqual(JsonConvert.SerializeObject(newItem, Formatting.None),
                JsonConvert.SerializeObject(menu, Formatting.None));
        }

        [TestMethod]
        public void Clone_ViewInfo_Test()
        {
            var menu = new ViewInfo("A")
            {
                DisplayIndex = 1,
                Alignment = MenuAlignment.Left,
                DisplayType = MenuDisplayType.IconAndText,
                Icon = "123",
                Title = "1111",
                Roles = {"Aaaa"},
                ReguireAuthorized = true
            };

            menu.SetGetter(a => "123");

            var newItem = (ViewInfo) menu.Clone();

            Assert.AreNotEqual(newItem, menu);
            Assert.AreEqual(newItem.GetInfo(null), menu.GetInfo(null));

            Assert.AreEqual(JsonConvert.SerializeObject(newItem, Formatting.None),
                JsonConvert.SerializeObject(menu, Formatting.None));
        }
    }
}