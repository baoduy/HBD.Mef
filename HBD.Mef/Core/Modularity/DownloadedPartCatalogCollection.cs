#region

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using Prism.Modularity;

#endregion

namespace HBD.Mef.Core.Modularity
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DownloadedPartCatalogCollection
    {
        private readonly Dictionary<ModuleInfo, ComposablePartCatalog> _catalogs =
            new Dictionary<ModuleInfo, ComposablePartCatalog>();

        public void Add(ModuleInfo moduleInfo, ComposablePartCatalog catalog) => _catalogs.Add(moduleInfo, catalog);

        public ComposablePartCatalog Get(ModuleInfo moduleInfo) => _catalogs[moduleInfo];

        public bool TryGet(ModuleInfo moduleInfo, out ComposablePartCatalog catalog)
            => _catalogs.TryGetValue(moduleInfo, out catalog);

        public void Remove(ModuleInfo moduleInfo) => _catalogs.Remove(moduleInfo);

        public void Clear() => _catalogs.Clear();
    }
}