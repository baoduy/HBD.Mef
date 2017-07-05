#region using

using HBD.Mef.Modularity;

#endregion

namespace HBD.Mef.Console
{
    public abstract class ConsolePluginBase : PluginBase
    {
        public override void Initialize()
        {
            
        }

        public abstract void Run(params string[] args);
    }
}