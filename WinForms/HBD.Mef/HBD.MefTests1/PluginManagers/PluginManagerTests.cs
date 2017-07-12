using HBD.Mef.Modularity;
using HBD.MefTests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace HBD.MefTests.PluginManagers
{
    [TestClass]
    public class PluginManagerTests
    {
        [TestMethod]
        public void The_SystemModule_Wont_Be_Validated()
        {
            using (var b = new TestBootstrapper())
            {
                b.Run();

                var p = b.Container.GetExportedValue<IPluginManager>() as PluginManager;

                var validators = new PrivateObject(p).GetFieldOrProperty("ModuleActivationValidators") 
                    as IReadOnlyCollection<IModuleActivationValidator>;
                var t = validators.OfType<TestActivationValidator>().First();

                Assert.IsTrue(t.Count > 0);
            }
        }
    }
}
