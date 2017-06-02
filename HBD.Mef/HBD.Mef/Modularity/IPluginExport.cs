#region using

using System;
using System.ComponentModel;

#endregion

namespace HBD.Mef.Modularity
{
    public interface IPluginExport
    {
        string ModuleName { get; }

        Type ModuleType { get; }

        [DefaultValue(null)]
        string[] DependsOnModuleNames { get; }
    }
}