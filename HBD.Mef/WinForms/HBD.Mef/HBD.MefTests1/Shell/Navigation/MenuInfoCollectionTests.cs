using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.Mef.Shell.Navigation.Tests
{
    [TestClass()]
    public class MenuInfoCollectionTests
    {
        [TestMethod()]
        public void MenuInfoCollection_WithIndex_Test()
        {
            var menuset = new MenuInfoCollection();

            menuset.Menu("Item1").DisplayAt(10);
            menuset.Menu("Item2").DisplayAt(20);
            menuset.Menu("Item3").DisplayAt(30);
            menuset.Menu("Item0").DisplayAt(0);


            Assert.IsTrue(menuset[0].DisplayIndex == 0);
            Assert.IsTrue(menuset[1].DisplayIndex == 10);
            Assert.IsTrue(menuset[2].DisplayIndex == 20);
        }

        [TestMethod()]
        public void MenuInfoCollection_Test()
        {
            var menuset = new MenuInfoCollection();

            menuset.Menu("Item1");
            menuset.Menu("Item2");
            menuset.Menu("Item3");
            menuset.Menu("Item0").DisplayAt(0);

            Assert.IsTrue(menuset[0].DisplayIndex == 0);
            Assert.IsTrue(menuset[1].DisplayIndex == 1);
            Assert.IsTrue(menuset[2].DisplayIndex == 2);
        }
    }
}