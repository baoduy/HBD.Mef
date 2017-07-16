using HBD.Mef.Modularity;
using System;

namespace HBD.MefTests.PluginManagers
{
    public class TestActivationValidator : IModuleActivationValidator
    {
        public int Count { get; private set; }
        public bool CanActivate(IModuleInfo moduleInfo)
        {
            if (moduleInfo.IsSystemModule)
                throw new NotSupportedException();
            Count += 1;

            return true;
        }
    }
}
