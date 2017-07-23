using HBD.Mef;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System.Linq;

namespace HBD.MefTests
{
    [TestClass]
    public class StandardBootstrapperTests
    {
        [TestMethod]
        public void Verify_TheMethods_Calling()
        {
            var methods = typeof(StandardBootstrapper)
               .GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
               .Where(m => m.IsVirtual && m.Name != "Finalize" && m.GetParameters().Count() == 0)
               .Select(m => m.Name).ToArray();

            var b = new Mock<StandardBootstrapper>() { CallBase = true };

            b.Object.Run();
            b.Object.Dispose();

            foreach (var m in methods)
                b.Protected().Verify(m, Times.Once());

            b.Protected().Verify("Dispose", Times.Once(), true);
        }
    }
}
