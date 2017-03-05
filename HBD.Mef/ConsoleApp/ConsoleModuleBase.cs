#region

using HBD.Mef.Core.Modularity;

#endregion

namespace HBD.Mef.ConsoleApp
{
    public abstract class ConsoleModuleBase : ModuleBase
    {
        public abstract void Run(params string[] args);
    }
}