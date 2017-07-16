using HBD.Framework.Attributes;
using HBD.Framework.Core;
using HBD.Mef.Modularity;
using System;

namespace HBD.Mef.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ModuleInfoAttribute : Attribute, IModuleInfo
    {
        public string Id { get; }

        public string Name { get; }

        public string Version { get; set; } = "N/A";

        public string Description { get; set; }

        public bool IsSystemModule { get; set; }

        public ModuleInfoAttribute([NotNull]string guid, [NotNull]string name)
        {
            Guard.ArgumentIsNotNull(guid, nameof(guid));
            Guard.ArgumentIsNotNull(name, nameof(name));

            this.Id = guid;
            this.Name = name;
        }
    }
}
