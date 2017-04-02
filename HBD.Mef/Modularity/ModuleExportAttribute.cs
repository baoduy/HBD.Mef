#region using

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;

#endregion

namespace HBD.Mef.Modularity
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleExportAttribute : ExportAttribute, IModuleExport
    {
        public ModuleExportAttribute(Type moduleType)
            : base(typeof(IHbdModule))
        {
            if (moduleType == null)
                throw new ArgumentNullException(nameof(moduleType));

            ModuleName = moduleType.Name;
            ModuleType = moduleType;
        }

        public ModuleExportAttribute(string moduleName, Type moduleType)
            : base(typeof(IHbdModule))
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