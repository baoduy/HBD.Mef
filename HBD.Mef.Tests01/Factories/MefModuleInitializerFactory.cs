using System.ComponentModel.Composition.Hosting;
using Prism.Logging;
using Microsoft.Practices.ServiceLocation;
using HBD.Mef.Modularity;
using System;
using Microsoft.Pex.Framework;

namespace HBD.Mef.Modularity
{
    /// <summary>A factory for HBD.Mef.Modularity.MefModuleInitializer instances</summary>
    public static partial class MefModuleInitializerFactory
    {
        /// <summary>A factory for HBD.Mef.Modularity.MefModuleInitializer instances</summary>
        [PexFactoryMethod(typeof(MefModuleInitializer))]
        public static MefModuleInitializer Create(
            IServiceLocator serviceLocator_iServiceLocator,
            ILoggerFacade loggerFacade_iLoggerFacade,
            DownloadedPartCatalogCollection downloadedPartCatalogs_downloadedPartCatalogCollection,
            AggregateCatalog aggregateCatalog_aggregateCatalog
        )
        {
            MefModuleInitializer mefModuleInitializer = new MefModuleInitializer
                                                    (serviceLocator_iServiceLocator, loggerFacade_iLoggerFacade,
                                                     downloadedPartCatalogs_downloadedPartCatalogCollection,
                                                     aggregateCatalog_aggregateCatalog);
            return mefModuleInitializer;

            // TODO: Edit factory method of MefModuleInitializer
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
