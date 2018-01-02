using HBD.Mef.Shell.Modularity;

namespace TestModule1
{
    /// <summary>
    ///     The Module Manager for Shell window system. Why PartCreationPolicy is shared because I want
    ///     Mef to create 1 instance of this view only as the tab manager allows only 1 instance of the
    ///     view to be displayed.
    /// </summary>
    public class StartupTestModule : ShellModuleBase
    {
        
    }
}