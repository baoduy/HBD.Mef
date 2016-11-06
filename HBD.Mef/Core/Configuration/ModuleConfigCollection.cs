using System.Collections.Generic;
using HBD.Framework.Collections;

namespace HBD.Mef.Core.Configuration
{
    public class ModuleConfigCollection : ChangeTrackingCollection<ModuleConfig>
    {
        public ModuleConfigCollection(IEnumerable<ModuleConfig> collection = null) : base(collection)
        {
        }
    }
}