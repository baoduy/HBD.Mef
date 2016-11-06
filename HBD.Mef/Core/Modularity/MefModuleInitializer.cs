using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Prism.Logging;
using Prism.Modularity;

namespace HBD.Mef.Core.Modularity
{
    [Export(typeof(IModuleInitializer))]
    public class MefModuleInitializer : ModuleInitializer
    {
        private readonly DownloadedPartCatalogCollection _downloadedPartCatalogs;

        [ImportingConstructor]
        public MefModuleInitializer(IServiceLocator serviceLocator, ILoggerFacade loggerFacade,
            DownloadedPartCatalogCollection downloadedPartCatalogs, AggregateCatalog aggregateCatalog)
            : base(serviceLocator, loggerFacade)
        {
            if (downloadedPartCatalogs == null)
                throw new ArgumentNullException(nameof(downloadedPartCatalogs));
            if (aggregateCatalog == null)
                throw new ArgumentNullException(nameof(aggregateCatalog));
            _downloadedPartCatalogs = downloadedPartCatalogs;
            AggregateCatalog = aggregateCatalog;
        }

        private AggregateCatalog AggregateCatalog { get; }

        [ImportMany(AllowRecomposition = true)]
        private IEnumerable<Lazy<IModule, IModuleExport>> ImportedModules { get; set; }

        protected override IModule CreateModule(ModuleInfo moduleInfo)
        {
            ComposablePartCatalog catalog;
            if (_downloadedPartCatalogs.TryGet(moduleInfo, out catalog))
            {
                if (!AggregateCatalog.Catalogs.Contains(catalog))
                    AggregateCatalog.Catalogs.Add(catalog);
                _downloadedPartCatalogs.Remove(moduleInfo);
            }
            if ((ImportedModules != null) && (ImportedModules.Count() != 0))
            {
                var lazy = ImportedModules.FirstOrDefault(x => x.Metadata.ModuleName == moduleInfo.ModuleName);
                if (lazy != null)
                    return lazy.Value;
            }
            throw new ModuleInitializeException(moduleInfo.ModuleType);
        }
    }
}