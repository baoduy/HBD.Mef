using HBD.Mef.Attributes;
using HBD.Mef.Modularity;
using HBD.Mef.Shell.Modularity;
using System.ComponentModel.Composition;

namespace HBD.MefTests.TestClasses
{
    [PluginExport(typeof(SystemModuleStartUp))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [ModuleInfo("6AFEEBB6-1CC1-4623-84D6-B77FE108C5AB", "System Module", IsSystemModule = true)]
    public class SystemModuleStartUp : ShellModuleBase
    {
    }
}
