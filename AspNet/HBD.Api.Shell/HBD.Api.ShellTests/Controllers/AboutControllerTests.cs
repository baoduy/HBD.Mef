using Microsoft.VisualStudio.TestTools.UnitTesting;
using HBD.Api.ShellTests;
using TestStack.Seleno.PageObjects;

namespace HBD.Api.Shell.Controllers.Tests
{
    [TestClass()]
    public class AboutControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            var p = Hosting.Instance.NavigateToInitialPage<Page>();
        }
    }
}