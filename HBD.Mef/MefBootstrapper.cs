#region using

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using HBD.Mef.Logging;
using HBD.Mef.Services;
using Microsoft.Practices.ServiceLocation;
using HBD.Mef.Modularity;

#endregion

namespace HBD.Mef
{
    /// <summary>
    ///     The Bootstrapper for Console app.
    /// </summary>
    public abstract class MefBootstrapper
    {
        public ILogger Logger { get; private set; }
        public CompositionContainer Container { get; private set; }
        protected AggregateCatalog AggregateCatalog { get; private set; }
        //protected IModuleCatalog ModuleCatalog { get; private set; }

        protected virtual ILogger CreateLogger() => new Trace2FileLogger();

        protected virtual AggregateCatalog CreateAggregateCatalog() => new AggregateCatalog();

        //protected virtual IModuleCatalog CreateModuleCatalog() => new ModuleCatalog();

        protected virtual CompositionContainer CreateContainer() => new CompositionContainer(AggregateCatalog);

        protected virtual void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MefBootstrapper).Assembly));
        }

        protected virtual void ConfigureModuleCatalog()
        {
        }

        /// <summary>
        ///     using this.AggregateCatalog.Catalogs.Add(new TypeCatalog(typeof())) to register default
        ///     type if missing.
        /// </summary>
        public virtual void RegisterDefaultTypesIfMissing()
        {
            //this.AggregateCatalog.Catalogs.Add(new TypeCatalog(typeof(IHbdModuleManager), typeof(HbdModuleManager)));
        }

        /// <summary>
        ///     using Container.ComposeExportedValue to register the Bootstrapper Provided Types
        /// </summary>
        protected virtual void RegisterBootstrapperProvidedTypes()
        {
            Container.ComposeExportedValue(Logger);
            //Container.ComposeExportedValue(ModuleCatalog);
            Container.ComposeExportedValue(AggregateCatalog);
            Container.ComposeExportedValue(Container);
            Container.ComposeExportedValue<ICompositionService>(Container);
            Container.ComposeExportedValue<IServiceLocator>(new MefServiceLocatorAdapter(Container));
        }

        /// <summary>
        ///     Initialize Application before start. Load static data, load configuration and prepare the
        ///     resource to be use in the app.
        /// </summary>
        protected virtual void InitializeApplication()
        {
        }

        /// <summary>
        ///     Initialize all registered Modules.
        /// </summary>
        protected virtual void InitializeModules() => Container.GetExportedValue<IPluginManager>().Run();

        public void Run()
        {
            try
            {
                Run(true);
            }
            catch (Exception ex)
            {
                Logger?.Exception(ex);
            }
        }

        public virtual void Run(bool runWithDefaultConfiguration)
        {
            Logger = CreateLogger();
            if (Logger == null)
                throw new InvalidOperationException(nameof(CreateLogger));

            Logger.Debug("Create AggregateCatalog.");
            AggregateCatalog = CreateAggregateCatalog();
            if (AggregateCatalog == null)
                throw new InvalidOperationException(nameof(CreateAggregateCatalog));

            Logger.Debug("Configure AggregateCatalog.");
            ConfigureAggregateCatalog();

            Logger.Debug("RegisterDefaultTypesIfMissing.");
            RegisterDefaultTypesIfMissing();

            //Logger.Debug("CreateModuleCatalog.");
            //ModuleCatalog = CreateModuleCatalog();

            Logger.Debug("Create Container.");
            Container = CreateContainer();
            if (Container == null)
                throw new InvalidOperationException(nameof(CreateContainer));

            Logger.Debug("Register Bootstrapper ProvidedTypes.");
            RegisterBootstrapperProvidedTypes();

            Logger.Debug("Initialize Application.");
            InitializeApplication();

            Logger.Debug("Initialize Modules.");
            InitializeModules();
        }
    }
}