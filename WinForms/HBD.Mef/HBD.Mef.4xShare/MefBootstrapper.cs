#region using

using System.ComponentModel.Composition;
using HBD.Mef.Logging;
using HBD.Mef.Modularity;
using HBD.Mef.Services;
using Microsoft.Practices.ServiceLocation;
using System.ComponentModel.Composition.Registration;

#endregion

namespace HBD.Mef
{
    /// <summary>
    ///     The Bootstrapper for console, Winforms and WPF applications.
    /// </summary>
    public abstract class MefBootstrapper : StandardBootstrapper
    {
        protected override RegistrationBuilder CreateReflectionContext()
        {
            var registration = new RegistrationBuilder();

            //Auto export for IPlugin
            registration.ForTypesDerivedFrom<IPlugin>()
                .SetCreationPolicy(CreationPolicy.Shared)
                .Export()
                .Export<IPlugin>(a =>
                {
                    a.AddMetadata(Constants.ModuleName, t => t.Name);
                    a.AddMetadata(Constants.ModuleType, t => t);
                    a.AddMetadata(Constants.DependsOnModuleNames, null);
                });

            //Auto export for IModuleActivationValidator
            registration.ForTypesDerivedFrom<IModuleActivationValidator>()
                .SetCreationPolicy(CreationPolicy.Shared)
                .Export().ExportInterfaces();

            return registration;
        }

        /// <summary>
        /// Register the external and singleton objects to Mef.
        /// </summary>
        protected override void RegisterExternalObjects()
        {
            base.RegisterExternalObjects();
            Container.ComposeExportedValue<IServiceLocator>(new MefServiceLocatorAdapter(Container));
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
            => Container.GetExportedValue<IPluginManager>().Run();

        protected override void DoRun()
        {
            base.DoRun();

            Logger.Debug("Register Bootstrapper ProvidedTypes.");
            RegisterExternalObjects();

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