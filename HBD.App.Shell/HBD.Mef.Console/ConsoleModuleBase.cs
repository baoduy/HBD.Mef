#region using

using HBD.Mef.Modularity;

#endregion

namespace HBD.Mef.Console
{
    public abstract class ConsoleModuleBase : PluginBase
    {
        public abstract void Run(params string[] args);
    }
}