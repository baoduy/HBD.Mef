using HBD.Mef.Logging;
using System;

#if NETSTANDARD2_0 || NETSTANDARD1_6
using System.Composition;
using System.Composition.Hosting;
using System.Reflection;
using System.Composition.Convention;
using HBD.Mef.Hosting;
using HBD.Mef.Catalogs;
#else 
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
#endif

namespace HBD.Mef
{
    /// <summary>
    /// The simple bootstrapper for cross-platform. It will provide the ILogger and Mef only.
    /// </summary>
    public abstract class StandardBootstrapper : IDisposable
    {
        public ILogger Logger { get; private set; }

#if NETSTANDARD2_0 || NETSTANDARD1_6
        protected StandardBootstrapper()
        {
            Catalogs = new CatalogCollection();
        }
        public CompositionContext Container { get; private set; }
        protected CatalogCollection Catalogs { get; }

        protected virtual void ConfigureCatalog()
        { }

        protected virtual CompositionContext CreateContainer()
        {
            var context = CreateContainerConfiguration().CreateContainer();
#if NETSTANDARD2_0
            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => new Mef.Services.MefServiceLocatorAdapter(context));
#endif
            return context;
        }

        protected virtual ExtendedContainerConfiguration CreateContainerConfiguration()
        {
            var configuration = new ExtendedContainerConfiguration()
                .WithInstance(() => Logger)
                .WithInstance(() => this.Container)
#if NETSTANDARD2_0
                .WithInstance<Microsoft.Practices.ServiceLocation.IServiceLocator>(() => new Services.MefServiceLocatorAdapter(this.Container))
#endif
                .WithAssembly(typeof(StandardBootstrapper).GetTypeInfo().Assembly);

            foreach (var item in Catalogs)
            {
                if (item.Provider == null)
                    configuration.WithAssemblies(item.Assemblies);
                else configuration.WithAssemblies(item.Assemblies, item.Provider);
            }

            return (ExtendedContainerConfiguration)configuration;
        }
#else
        public CompositionContainer Container { get; private set; }

        protected AggregateCatalog AggregateCatalog { get; private set; }

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
            var refc = CreateReflectionContext();

            if (refc == null)
                AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(StandardBootstrapper).Assembly));
            else
                AggregateCatalog.Catalogs.Add(new ApplicationCatalog(refc));
        }

        protected virtual RegistrationBuilder CreateReflectionContext()
        {
            return null;
        }

        /// <summary>
        /// Register the external and singleton objects to Mef.
        /// </summary>
        protected virtual void RegisterExternalObjects()
        {
            Container.ComposeExportedValue(Logger);
            Container.ComposeExportedValue(AggregateCatalog);
            Container.ComposeExportedValue(Container);
            Container.ComposeExportedValue<ICompositionService>(Container);
        }

#endif

        protected virtual ILogger CreateLogger()
        {
            return new Trace2FileLogger();
        }

        public void Run() => DoRun();

        protected virtual void DoRun()
        {
            Logger = CreateLogger();
            if (Logger == null)
                throw new InvalidOperationException(nameof(CreateLogger));

#if NETSTANDARD2_0 || NETSTANDARD1_6
            Logger.Debug("Configure Catalogs.");
            ConfigureCatalog();
#else
            Logger.Debug("Create AggregateCatalog.");
            AggregateCatalog = CreateAggregateCatalog();
            if (AggregateCatalog == null)
                throw new InvalidOperationException(nameof(CreateAggregateCatalog));

            Logger.Debug("Configure AggregateCatalog.");
            ConfigureAggregateCatalog();
#endif
            Logger.Debug("Create Container.");
            Container = CreateContainer();
            if (Container == null)
                throw new InvalidOperationException(nameof(CreateContainer));

#if !NETSTANDARD2_0 && !NETSTANDARD1_6
            Logger.Debug("Configure RegisterExternalObjects.");
            RegisterExternalObjects();
#endif

        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool isDisposing)
        {
            this.Logger?.Dispose();

            if (this.Container is IDisposable a)
                a.Dispose();

            this.Logger = null;
            this.Container = null;
        }
    }
}
