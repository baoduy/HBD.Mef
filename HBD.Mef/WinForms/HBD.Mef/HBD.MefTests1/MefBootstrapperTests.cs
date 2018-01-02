using HBD.Mef;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System.Linq;

namespace HBD.MefTests
{
    [TestClass]
    public class MefBootstrapperTests
    {
        [TestMethod]
        public void Verify_TheMethods_Calling()
        {
            var methods = typeof(MefBootstrapper)
                .GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .Where(m => m.IsVirtual && m.Name != "Finalize" && m.GetParameters().Count() == 0)
                .Select(m => m.Name).ToArray();

            var b = new Mock<MefBootstrapper>() { CallBase = true };

            b.Object.Run();
            b.Object.Dispose();

            foreach (var m in methods)
                b.Protected().Verify(m, Times.Once());

            //Verify the Methods that have parameters manually.
            b.Protected().Verify("Dispose", Times.Once(), true);
        }
    }
}
