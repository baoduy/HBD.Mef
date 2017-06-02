#region using

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using HBD.Mef.Logging;
using HBD.Mef.Modularity;
using HBD.Mef.Services;
using Microsoft.Practices.ServiceLocation;

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

        protected virtual ILogger CreateLogger()
        {
            return new Trace2FileLogger();
        }

        protected virtual AggregateCatalog CreateAggregateCatalog()
        {
            return new AggregateCatalog();
        }

        protected virtual CompositionContainer CreateContainer()
        {
            return new CompositionContainer(AggregateCatalog);
        }

        protected virtual void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MefBootstrapper).Assembly));
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
        protected virtual void InitializeModules()
        {
            Container.GetExportedValue<IPluginManager>().Run();
        }

        public void Run()
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

            Logger.Debug("Run Application.");
            RunApplication();
        }

        /// <summary>
        ///     This method will be executed by Run() use to open the MainWindow forms. This method will be call in the the end of
        ///     the Run method.
        /// </summary>
        protected virtual void RunApplication()
        {
        }
    }
}