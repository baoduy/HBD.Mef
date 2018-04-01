#region

using System.Data;
using HBD.Mef.Mvc.Navigation.NavigateInfo;
using HBD.Mef.MvcTests.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HBD.Mef.Mvc.Navigation.Tests
{
    [TestClass]
    public class NavigationServiceTests
    {
        [TestMethod]
        public void Exception_AddMenu()
        {
            var nav = new NavigationService("Area1");
            var m = nav.Menu("AA");

            Assert.IsNotNull(m);
            Assert.AreEqual("AA", m.Title);
            Assert.AreEqual("Area1", m.AreaName);
        }

        [TestMethod]
        public void Menu_NotDuplicated()
        {
            var nav = new NavigationService("Area1");
            var m1 = nav.Menu("AA");
            var m2 = nav.Menu("AA");

            Assert.AreEqual(m1, m2);
            Assert.IsNotNull(m1);
        }

        [TestMethod]
        public void Navigation()
        {
            var nav = new NavigationService("Area1");
            nav.AddNavigation(a => a.WithTitle("Home").For<TestController>(c => nameof(c.Index)));

            Assert.IsTrue(nav.Items.Count == 1);

            var n = nav.Items[0] as NavigationInfo;

            Assert.IsNotNull(n);
            Assert.AreEqual("Area1", n.AreaName);
            Assert.AreEqual("Home", n.Title);
            Assert.AreEqual("Index", n.Action);
            Assert.AreEqual("Test", n.Controller);
        }


        [TestMethod]
        [ExpectedException(typeof(DuplicateNameException))]
        public void Navigation_NotAllowToAddDuplicate()
        {
            var nav = new NavigationService("Area1");
            nav.AddNavigation(a => a.WithTitle("Home").For<TestController>(c => nameof(c.Index)));
            nav.AddNavigation(a => a.WithTitle("Home").For<TestController>(c => nameof(c.Index)));
        }

        [TestMethod]
        public void Menu_AddNavigation()
        {
            var nav = new NavigationService("Area1");
            var m = nav.Menu("A");

            m.Items.AddNavigation(a => a.WithTitle("Home").For<TestController>(c => nameof(c.Index)));

            Assert.IsNotNull(m.Items[0]);

            var n = m.Items[0] as NavigationInfo;

            Assert.IsNotNull(n);
            Assert.AreEqual("Area1", n.AreaName);
            Assert.AreEqual("Home", n.Title);
            Assert.AreEqual("Index", n.Action);
            Assert.AreEqual("Test", n.Controller);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateNameException))]
        public void Menu_AddNavigation_NotAllowToAddDuplicate()
        {
            var nav = new NavigationService("Area1");
            var m = nav.Menu("A");

            m.Items.AddNavigation(a => a.WithTitle("Home").For<TestController>(c => nameof(c.Index)));
            m.Items.AddNavigation(a => a.WithTitle("Home").For<TestController>(c => nameof(c.Index)));
        }

        [TestMethod]
        public void Menu_AddSeparator()
        {
            var nav = new NavigationService("Area1");
            var m = nav.Menu("A");

            m.Items.AddSeperator();
            Assert.IsInstanceOfType(m.Items[0], typeof(SeparetorInfo));
        }

        [TestMethod]
        public void Menu_RequireAuthorize()
        {
            var nav = new NavigationService("Area1");
            nav.Items.Clear();

            var m = nav.Menu("A");

            m.Items.AddNavigation(a => a.WithTitle("Home")
                .WithAuthorize()
                .For<TestController>(c => nameof(c.Index)));

            Assert.IsTrue(((INavigateRole) m.Items[0]).ReguireAuthorized);
        }

        [TestMethod]
        public void Menu_RequireAuthorize_ManualSetRoles()
        {
            var nav = new NavigationService("Area1");
            nav.Items.Clear();

            var m = nav.Menu("A");

            m.Items.AddNavigation(a => a.WithTitle("Home")
                .WithAuthorize("Admin")
                .For<TestController>(c => nameof(c.Index)));

            Assert.IsTrue(((INavigateRole) m.Items[0]).ReguireAuthorized);
            Assert.IsTrue(((INavigateRole) m.Items[0]).Roles.Contains("Admin"));
        }

        [TestMethod]
        public void Menu_RequireAuthorize_RoleFromController()
        {
            var nav = new NavigationService("Area1");
            nav.Items.Clear();

            var m = nav.Menu("A");

            m.Items.AddNavigation(a => a.WithTitle("Home")
                .For<NonExportedController>(c => nameof(c.Index)));

            Assert.IsTrue(((INavigateRole) m.Items[0]).ReguireAuthorized);
            Assert.IsTrue(((INavigateRole) m.Items[0]).Roles.Count == 2);
        }
    }
}