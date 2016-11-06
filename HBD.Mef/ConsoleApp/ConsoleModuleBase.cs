using Prism.Modularity;

namespace HBD.Mef.ConsoleApp
{
    public abstract class ConsoleModuleBase : IModule
    {
        public abstract void Initialize();

        public abstract void Run(params string[] args);
    }
}