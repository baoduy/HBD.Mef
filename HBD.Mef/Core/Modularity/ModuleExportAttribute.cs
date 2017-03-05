#region

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Prism.Modularity;

#endregion

namespace HBD.Mef.Core.Modularity
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleExportAttribute : ExportAttribute, IModuleExport
    {
        public ModuleExportAttribute(Type moduleType)
            : base(typeof(IModule))
        {
            if (moduleType == null)
                throw new ArgumentNullException(nameof(moduleType));
            ModuleName = moduleType.Name;
            ModuleType = moduleType;
        }

        public ModuleExportAttribute(string moduleName, Type moduleType)
            : base(typeof(IModule))
        {
            ModuleName = moduleName;
            ModuleType = moduleType;
        }

        public string ModuleName { get; }

        public Type ModuleType { get; }

        public InitializationMode InitializationMode { get; set; }

        [DefaultValue(new string[] {})]
        public string[] DependsOnModuleNames { get; set; }
    }
}