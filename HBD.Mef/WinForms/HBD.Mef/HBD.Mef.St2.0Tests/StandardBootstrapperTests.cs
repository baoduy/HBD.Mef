using HBD.Mef.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System.Linq;
using System.Reflection;

namespace HBD.Mef.StTests
{
    [TestClass]
    public class StandardBootstrapperTests
    {
        [TestMethod]
        public void Verify_TheMethods_Calling()
        {
            var methods = typeof(StandardBootstrapper).GetTypeInfo()
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(m => m.IsVirtual && m.Name!= "Finalize" && !m.GetParameters().Any())
                .Select(m => m.Name).ToArray();

            var b = new Mock<StandardBootstrapper>() { CallBase = true };
            b.Protected().Setup<ILogger>("CreateLogger").Returns(new HBD.Mef.Logging.Log4NetLogger()).Verifiable();

            b.Object.Run();
            b.Object.Dispose();

            foreach (var m in methods)
                b.Protected().Verify(m, Times.Once());

            //Verify the Methods that have parameters manually.
            b.Protected().Verify("Dispose", Times.Once(), true);
        }
    }
}
