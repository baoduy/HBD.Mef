using HBD.Mef.Shell.Services;
using System.ComponentModel.Composition;

namespace TestModule
{
    [Export(typeof(IShellMenuService))]
    public class Class1: ShellMenuService
    {
        readonly DependencyLib.DepLib d;
        public Class1()
        {
            d = new DependencyLib.DepLib();
        }
    }
}
