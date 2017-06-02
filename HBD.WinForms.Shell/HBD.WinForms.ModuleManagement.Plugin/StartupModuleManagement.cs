using HBD.Mef.Modularity;
using HBD.Mef.Shell;
using HBD.Mef.Shell.Navigation;
using HBD.Mef.Shell.Services;
using HBD.Mef.WinForms.Modularity;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace HBD.WinForms.ModuleManagement.Plugin
{
    /// <summary>
    /// The Module Manager for Shell window system. Why PartCreationPolicy is shared because I want
    /// Mef to create 1 instance of this view only as the tab manager allows only 1 instance of the
    /// view to be displayed.
    /// </summary>
    [PluginExport(typeof(StartupModuleManagement))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class StartupModuleManagement : WinFormModuleBase
    {
        protected override IEnumerable<IViewInfo> GetStartUpViewTypes()
        {
            yield break;
        }

        protected override void MenuConfiguration(IShellMenuService menuSet)
        {
            menuSet.Menu("System")
                .WithIcon(Resource.system)
                .WithAlignment(MenuAlignment.Right)
                .Children
                .AddNavigation("Module Manager")
                .WithIcon(Resource.Module)
                .For(new ViewInfo(typeof(ManageModuleView)));
        }
    }
}