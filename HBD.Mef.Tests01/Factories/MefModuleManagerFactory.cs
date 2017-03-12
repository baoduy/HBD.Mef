using System.Collections.Generic;
using Prism.Logging;
using Prism.Modularity;
using HBD.Mef.Modularity;
using System;
using Microsoft.Pex.Framework;

namespace HBD.Mef.Modularity
{
    /// <summary>A factory for HBD.Mef.Modularity.MefModuleManager instances</summary>
    public static partial class MefModuleManagerFactory
    {
        /// <summary>A factory for HBD.Mef.Modularity.MefModuleManager instances</summary>
        [PexFactoryMethod(typeof(MefModuleManager))]
        public static MefModuleManager Create(
            IModuleInitializer moduleInitializer_iModuleInitializer,
            IModuleCatalog moduleCatalog_iModuleCatalog,
            ILoggerFacade loggerFacade_iLoggerFacade,
            MefFileModuleTypeLoader value_mefFileModuleTypeLoader,
            IEnumerable<IModuleTypeLoader> value_iEnumerable
        )
        {
            MefModuleManager mefModuleManager
       = new MefModuleManager(moduleInitializer_iModuleInitializer,
                              moduleCatalog_iModuleCatalog, loggerFacade_iLoggerFacade);
            mefModuleManager.MefFileModuleTypeLoader = value_mefFileModuleTypeLoader;
            mefModuleManager.ModuleTypeLoaders = value_iEnumerable;
            return mefModuleManager;

            // TODO: Edit factory method of MefModuleManager
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
