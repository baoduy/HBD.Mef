#region

using System.Collections.Generic;
using HBD.Framework.Collections;

#endregion

namespace HBD.Mef.Shell.Configuration
{
    public class ModuleConfigCollection<TModuleConfig> : ChangeTrackingCollection<TModuleConfig>
        where TModuleConfig : ModuleConfig
    {
        public ModuleConfigCollection(IEnumerable<TModuleConfig> collection = null) : base(collection)
        {
        }
    }

    public class ModuleConfigCollection : ModuleConfigCollection<ModuleConfig>
    {
        public ModuleConfigCollection(IEnumerable<ModuleConfig> collection = null) : base(collection)
        {
        }
    }
}