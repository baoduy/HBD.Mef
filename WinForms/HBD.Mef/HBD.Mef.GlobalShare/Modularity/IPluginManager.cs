using System;

namespace HBD.Mef.Modularity
{
    public interface IPluginManager : IDisposable
    {
        void Run();
    }
}
