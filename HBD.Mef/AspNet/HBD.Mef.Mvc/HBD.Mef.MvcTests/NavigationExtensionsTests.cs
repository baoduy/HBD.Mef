using System.Security.Principal;
using Moq;

#region using

using System.Linq;
using HBD.Mef.Mvc.Icons;
using HBD.Mef.Mvc.Navigation.NavigateInfo;
using HBD.Mef.MvcTests.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HBD.Mef.Mvc.Tests
{
    [TestClass]
    public class NavigationExtensionsTests
    {
        [TestMethod]
        public void AddNavigationTest()
        {
            var coll = new NavigationCollection(new MenuInfo("AAA"));

            coll.AddNavigation(a => a.WithTitle("123")
                .WithIcon(Glyphicon.glyphicon_adjust)
                .Action = nameof(TestController.Index));

            Assert.IsTrue(coll.Count == 1);
            Assert.IsInstanceOfType(coll[0], typeof(NavigationInfo));
            Assert.AreEqual(((NavigationInfo) coll[0]).Action, "Index");
        }

        [TestMethod]
        public void GetRolesTest()
        {
            var a = NavigationExtensions.GetRoles<NonExportedController>("Contact");

            Assert.IsTrue(a.Item1);
            Assert.IsTrue(a.Item2.Contains("View"));

            a = NavigationExtensions.GetRoles<NonExportedController>("About");

            Assert.IsTrue(a.Item1);
            Assert.IsTrue(a.Item2.Contains("Admin"));

            a = NavigationExtensions.GetRoles<NonExportedController>("Index");

            Assert.IsTrue(a.Item1);
            Assert.IsTrue(a.Item2.Contains("Support"));

            a = NavigationExtensions.GetRoles<NonExportedController>();
            Assert.IsTrue(a.Item1);
            Assert.IsTrue(a.Item2.Contains("Admin") && a.Item2.Contains("Support"));

            a = NavigationExtensions.GetRoles<TestController>();
            Assert.IsFalse(a.Item1);
            Assert.IsNull(a.Item2);
        }

        [TestMethod]
        public void IsValidRoles_UserHasNoRole_Test()
        {
            var identity = new Mock<IIdentity>();
            identity.SetupGet(i => i.IsAuthenticated).Returns(true);

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(a => a.Identity).Returns(identity.Object);
            userMock.Setup(a => a.IsInRole(It.IsAny<string>())).Returns(false);

            Assert.IsTrue(new NavigationInfo("A")
                .WithAuthorize()
                .IsValidRoles(userMock.Object));

            Assert.IsFalse(new NavigationInfo("A")
                .WithAuthorize("View")
                .IsValidRoles(userMock.Object));

            Assert.IsTrue(new NavigationInfo("A")
                .IsValidRoles(userMock.Object));
        }

        [TestMethod]
        public void IsValidRoles_AnonymousUser_Test()
        {
            var identity = new Mock<IIdentity>();
            identity.SetupGet(i => i.IsAuthenticated).Returns(false);

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(a => a.Identity).Returns(identity.Object);
            userMock.Setup(a => a.IsInRole(It.IsAny<string>())).Returns(false);

            Assert.IsFalse(new NavigationInfo("A")
                .WithAuthorize()
                .IsValidRoles(userMock.Object));

            Assert.IsFalse(new NavigationInfo("A")
                .WithAuthorize("View")
                .IsValidRoles(userMock.Object));

            Assert.IsTrue(new NavigationInfo("A")
                .IsValidRoles(userMock.Object));

            Assert.IsTrue(new MenuInfo("A")
                .IsValidRoles(userMock.Object));
        }

        [TestMethod]
        public void IsValidRoles_UserHasRole_Test()
        {
            var identity = new Mock<IIdentity>();
            identity.SetupGet(i => i.IsAuthenticated).Returns(true);

            var userMock = new Mock<IPrincipal>();
            userMock.SetupGet(a => a.Identity).Returns(identity.Object);
            userMock.Setup(a => a.IsInRole(It.IsAny<string>())).Returns(true);

            Assert.IsTrue(new NavigationInfo("A")
                .WithAuthorize()
                .IsValidRoles(userMock.Object));

            Assert.IsTrue(new NavigationInfo("A")
                .WithAuthorize("View")
                .IsValidRoles(userMock.Object));

            Assert.IsTrue(new NavigationInfo("A")
                .IsValidRoles(userMock.Object));
        }
    }
}