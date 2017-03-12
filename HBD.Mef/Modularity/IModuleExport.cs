﻿#region using

using System;
using System.ComponentModel;
using Prism.Modularity;

#endregion

namespace HBD.Mef.Modularity
{
    public interface IModuleExport
    {
        string ModuleName { get; }

        Type ModuleType { get; }

        [DefaultValue(InitializationMode.WhenAvailable)]
        InitializationMode InitializationMode { get; }

        [DefaultValue(null)]
        string[] DependsOnModuleNames { get; }
    }
}