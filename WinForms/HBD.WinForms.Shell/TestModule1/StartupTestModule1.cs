using System.Collections.Generic;
using System.ComponentModel.Composition;
using HBD.Mef.Modularity;
using HBD.Mef.Shell;
using HBD.Mef.Shell.Navigation;
using HBD.Mef.Shell.Services;
using HBD.Mef.WinForms.Modularity;

namespace TestModule1
{
    /// <summary>
    ///     The Module Manager for Shell window system. Why PartCreationPolicy is shared because I want
    ///     Mef to create 1 instance of this view only as the tab manager allows only 1 instance of the
    ///     view to be displayed.
    /// </summary>
    [PluginExport(typeof(StartupTestModule1))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class StartupTestModule1 : WinFormModuleBase
    {
        protected override IEnumerable<IViewInfo> GetStartUpViewTypes()
        {
            yield break;
        }

        protected override void MenuConfiguration(IShellMenuService menuSet)
        {
            menuSet.Menu("Test Module 1")
                .Children
                .AddNavigation("Module 1")
                .For(new ViewInfo(typeof(UserControl1)));
        }
    }
}