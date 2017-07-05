using HBD.Mef.Modularity;
using HBD.Mef.Shell.Modularity;
using System.ComponentModel.Composition;

namespace HBD.MefTests.TestClasses
{
    [PluginExport(typeof(StartUp2))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class StartUp2: ShellModuleBase
    {
    }
}
