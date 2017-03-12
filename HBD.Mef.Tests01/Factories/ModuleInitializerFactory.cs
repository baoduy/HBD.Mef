using Prism.Logging;
using Microsoft.Practices.ServiceLocation;
using Prism.Modularity;
using System;
using Microsoft.Pex.Framework;

namespace Prism.Modularity
{
    /// <summary>A factory for Prism.Modularity.ModuleInitializer instances</summary>
    public static partial class ModuleInitializerFactory
    {
        /// <summary>A factory for Prism.Modularity.ModuleInitializer instances</summary>
        [PexFactoryMethod(typeof(ModuleInitializer))]
        public static ModuleInitializer Create(
            IServiceLocator serviceLocator_iServiceLocator,
            ILoggerFacade loggerFacade_iLoggerFacade
        )
        {
            ModuleInitializer moduleInitializer = new ModuleInitializer
                                              (serviceLocator_iServiceLocator, loggerFacade_iLoggerFacade);
            return moduleInitializer;

            // TODO: Edit factory method of ModuleInitializer
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
