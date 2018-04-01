#region

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using HBD.Framework.Configuration;
using HBD.Framework.Core;
using HBD.Mef.Logging;
using HBD.Mef.Mvc.Adapters;
using HBD.Mef.Mvc.Catalogs;
using HBD.Mef.Mvc.Core;
using HBD.Mef.Mvc.Navigation;
using HBD.Mef.Mvc.Services;

#endregion

namespace HBD.Mef.Mvc
{
    public abstract class MvcBootstrapper : IMvcBootstrapper
    {
        public ILogger Logger { get; private set; }
        public CompositionContainer Container { get; private set; }

        protected AggregateCatalog AggregateCatalog { get; private set; }

        protected INavigationService MainNavigationService { get; private set; }

        protected virtual void SetResolver()
        {
            DependencyResolver.SetResolver(Container.GetExportedValue<IServiceLocator>());
            ServiceLocator.SetServiceLocator(() => Container);

            if (GlobalConfiguration.Configuration != null)
                GlobalConfiguration.Configuration.DependencyResolver =
                    new MefApiDependencyResolver(Container.GetExportedValue<IServiceLocator>());
        }

        protected virtual void InitializeApiModules()
        {
            var modules = Container.GetExports<IMefApiRegistration>().Select(a => a.Value);

            foreach (var a in modules)
                //GlobalConfiguration.Configure(a.Register);
                RegisterConfigFile(a.AreaName);
        }

        protected virtual void RegisterAreasResources()
        {
            if (BundleTable.Bundles == null) return;

            var areas = Container.GetExports<IMefAreaRegistration>().Select(a => a.Value);

            foreach (var a in areas)
                a.RegisterBundleResources(BundleTable.Bundles);
        }

        /// <summary>
        ///     This method is dedicated for MVC Shell app to register the Main Menus for the Shell.
        /// </summary>
        /// <param name="navigationService"></param>
        protected virtual void RegisterMainNavigation(INavigationService navigationService)
        {
        }

        protected virtual void Dispose(bool disposing)
        {
            Container?.Dispose();
            Logger?.Dispose();
            AggregateCatalog?.Dispose();
        }

        protected virtual ILogger CreateLogger()
        {
            return new Log4NetLogger();
        }

        protected virtual INavigationService CreateMainNavigationService()
        {
            var nav = Container.GetExportedValue<INavigationServiceFactory>() as NavigationServiceFactory;
            return nav?.GetMainNavigationService();
        }

        protected virtual AggregateCatalog CreateAggregateCatalog()
        {
            return new AggregateCatalog();
        }

        protected virtual CompositionContainer CreateContainer()
        {
            return new CompositionContainer(AggregateCatalog);
        }

        /// <summary>
        ///     The AggregateCatalog Configuration.
        ///     If you want to import the Types from Assemblies. you should override this method.
        /// </summary>
        protected virtual void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new MultiDirectoriesCatalog(
                new[] {$"{AppDomain.CurrentDomain.BaseDirectory}bin"},
                SearchOption.TopDirectoryOnly,
                CreateReflectionContext()
            ));
        }

        /// <summary>
        ///     Create a RegistrationBuilder that apply the CreationPolicy.NonShared for all Controllers.
        /// </summary>
        /// <returns></returns>
        protected virtual RegistrationBuilder CreateReflectionContext()
        {
            var registration = new RegistrationBuilder();

            registration.ForTypesDerivedFrom<IController>()
                .SetCreationPolicy(CreationPolicy.NonShared).Export().Export<IController>();

            registration.ForTypesDerivedFrom<IHttpController>()
                .SetCreationPolicy(CreationPolicy.NonShared).Export().Export<IHttpController>();

            registration.ForTypesDerivedFrom<IMefApiRegistration>()
                .SetCreationPolicy(CreationPolicy.Shared).Export().Export<IMefApiRegistration>();

            registration.ForTypesDerivedFrom<IMefAreaRegistration>()
                .SetCreationPolicy(CreationPolicy.Shared).Export().Export<IMefAreaRegistration>();

            return registration;
        }

        /// <summary>
        ///     using Container.ComposeExportedValue to register the Bootstrapper Provided Types
        /// </summary>
        protected virtual void RegisterBootstrapperProvidedTypes()
        {
            Container.ComposeExportedValue(Logger);
            Container.ComposeExportedValue(Container);
            Container.ComposeExportedValue<ICompositionService>(Container);
            Container.ComposeExportedValue<IServiceLocator>(new MefServiceLocator(Container));
            Container.ComposeExportedValue<IHttpControllerSelector>(
                new MefApiHttpControllerSelector(GlobalConfiguration.Configuration, Logger));
        }

        /// <summary>
        ///     Initialize all registered Areas.
        /// </summary>
        protected virtual void InitializeAreas()
        {
            var menuService = Container.GetExportedValue<INavigationServiceFactory>();
            var footerService = Container.GetExportedValue<IFooterNavigationService>();
            var areas = Container.GetExports<IMefAreaRegistration>().Select(a => a.Value);

            foreach (var a in areas)
            {
                //try
                //{
                //    if (RouteTable.Routes[a.PrimaryRouteName] == null)
                //        RegisterArea(a);
                //}
                //catch
                //{
                //    //Ignored when the are already registered by default.
                //}

                if (!menuService.IsRegistered(a.AreaName))
                {
                    a.RegisterNavigation(menuService.CreateNavigationService(a.AreaName));
                    a.RegisterFooterNavigation(footerService);
                }

                RegisterConfigFile(a.AreaName);
            }
        }

        /// <summary>
        ///     Load the AppSettings and ConnectionStrings of Areas/[ModuleName]/Web.config or bin/[ModuleName].config to
        ///     System.Configuration.ConfigurationManager.
        /// </summary>
        /// <param name="areaName"></param>
        protected virtual void RegisterConfigFile(string areaName)
        {
            Guard.ArgumentIsNotNull(areaName, nameof(areaName));

            var file = HostingEnvironment.MapPath(BundleExtensions.GetAreaConfigFile(areaName));
            if (!File.Exists(file))
                file = HostingEnvironment.MapPath(BundleExtensions.GetBinConfigFile(areaName));
            if (!File.Exists(file)) return;

            ConfigurationManager.MergeConfigFrom(file);
        }

        #region Public Methods

        public void Start()
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

            Logger.Debug("Create Container.");
            Container = CreateContainer();
            if (Container == null)
                throw new InvalidOperationException(nameof(CreateContainer));

            try
            {
                Logger.Debug("Register Bootstrapper ProvidedTypes.");
                RegisterBootstrapperProvidedTypes();

                Logger.Debug("Initialize ApiModules.");
                InitializeApiModules();

                Logger.Debug("Initialize Areas.");
                InitializeAreas();

                Logger.Debug("Initialize Main NavigationService.");
                MainNavigationService = CreateMainNavigationService();

                if (MainNavigationService != null)
                {
                    Logger.Debug("Register Main Navigation.");
                    RegisterMainNavigation(MainNavigationService);
                }

                Logger.Debug("Register Dependency Resolver.");
                SetResolver();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        public void PostStart()
        {
            RegisterAreasResources();
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector),
                Container.GetExportedValue<IHttpControllerSelector>());
            GlobalConfiguration.Configuration.EnsureInitialized();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Shutdown()
        {
            Dispose();
        }

        #endregion
    }
}