using System;
using System.Collections.Generic;

namespace HBD.Mef.Modularity
{
    public interface IPluginManager : IDisposable
    {
        IReadOnlyCollection<PluginInfo> Plugins { get; }
        IReadOnlyCollection<IModuleActivationValidator> ModuleActivationValidators { get; }

        void Run();
    }
}
