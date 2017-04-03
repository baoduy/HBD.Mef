#region using

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;

#endregion

namespace HBD.Mef.Modularity
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginExportAttribute : ExportAttribute, IModuleExport
    {
        public PluginExportAttribute(Type moduleType)
            : base(typeof(IPlugin))
        {
            if (moduleType == null)
                throw new ArgumentNullException(nameof(moduleType));

            ModuleName = moduleType.Name;
            ModuleType = moduleType;
        }

        public PluginExportAttribute(string moduleName, Type moduleType)
            : base(typeof(IPlugin))
        {
            ModuleName = moduleName;
            ModuleType = moduleType;
        }

        public string ModuleName { get; }

        public Type ModuleType { get; }

        [DefaultValue(new string[] { })]
        public string[] DependsOnModuleNames { get; set; }
    }
}