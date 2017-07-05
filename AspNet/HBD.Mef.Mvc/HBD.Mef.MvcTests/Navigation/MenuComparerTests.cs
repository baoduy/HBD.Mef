#region using

using HBD.Mef.Mvc.Navigation.NavigateInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HBD.Mef.Mvc.Navigation.Tests
{
    [TestClass]
    public class MenuComparerTests
    {
        [TestMethod]
        public void CompareTest()
        {
            var comparer = new MenuComparer();

            Assert.IsTrue(comparer.Compare(new MenuInfo("AA").DisplayAt(12), new NavigationInfo("AA")) < 0);
            Assert.IsTrue(comparer.Compare(new MenuInfo("AA").DisplayAt(1), new NavigationInfo("AA").DisplayAt(1)) ==
                          0);
            Assert.IsTrue(comparer.Compare(new MenuInfo("AA"), new NavigationInfo("AA").DisplayAt(1)) > 0);
        }
    }
}