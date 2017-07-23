using HBD.Framework.Attributes;
using HBD.Mef.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using HBD.Framework.Exceptions;
using HBD.Framework;

namespace HBD.Mef.Modularity
{
    [Export(typeof(IPluginManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class PluginManager : IPluginManager
    {
        private bool _isRan = false;
        [Import]
        protected AggregateCatalog AggregateCatalog { get; set; }

        [Import(AllowRecomposition = true, AllowDefault = true)]
        protected CompositionContainer ContainerService { get; set; }

        [Import(AllowRecomposition = true, AllowDefault = true)]
        protected ILogger Logger { get; set; }

        public List<PluginInfo> _plugins;

        public PluginManager()
        {
            _plugins = new List<PluginInfo>();
        }

        public IReadOnlyCollection<PluginInfo> Plugins => _plugins.ToReadOnly();
        public IReadOnlyCollection<IModuleActivationValidator> ModuleActivationValidators { get; private set; }

        public void Run()
        {
            if (_isRan)
                throw new NotSupportedException($"The {this.GetType().Name} already ran.");
            _isRan = true;

            _plugins.AddRange(GetModuleInfos());

            if (!Plugins.Any()) return;

            ModuleActivationValidators = ContainerService.GetExportedValues<IModuleActivationValidator>().ToReadOnly();

            foreach (var moduleInfo in Plugins)
                ActivateModule(moduleInfo);
        }

        private IEnumerable<PluginInfo> GetModuleInfos()
        {
            foreach (var item in AggregateCatalog.SelectMany(p => p.ExportDefinitions)
                .Where(p => p.Metadata.ContainsKey(Constants.ModuleName) && p.Metadata.ContainsKey(Constants.ModuleType)))
            {
                var name = item.Metadata[Constants.ModuleName] as string;
                var type = item.Metadata[Constants.ModuleType] as Type;
                var dependsOnModuleNames = item.Metadata[Constants.DependsOnModuleNames] as string[];

                if (_plugins.Any(p => p.ModuleType == type)) continue;

                if (dependsOnModuleNames?.Any() == true)
                    yield return new PluginInfo(name, type, dependsOnModuleNames);
                else yield return new PluginInfo(name, type);
            }
        }

        private bool IsActivatable([NotNull]PluginInfo module)
        {
            if (module.State == PluginState.Initializing
               || module.State == PluginState.Initialized
               || module.State == PluginState.Invalid)
            {
                return false;
            }

            var moduleInfo = module.GetModuleInfo();

            if (moduleInfo != null
                && !moduleInfo.IsSystemModule
                && ModuleActivationValidators.Any(v => !v.CanActivate(moduleInfo)))
            {
                return false;
            }

            return true;
        }

        private IPlugin TryGetPlugin([NotNull]PluginInfo module)
        {
            //Manual Export
            var manual = ContainerService.GetExports<IPlugin, IPluginExport>()
               .FirstOrDefault(i => i.Metadata.ModuleType == module.ModuleType && i.Metadata.ModuleName == module.ModuleName);

            if (manual != null)
                return manual.Value;

            //Auto Export
            return ServiceLocator.Current.GetInstance(module.ModuleType)as IPlugin;
        }

        /// <summary>
        /// Activate Module. Before activate this module the <see cref="IModuleActivationValidator"/> will be called.
        /// The module won't be activated of any IModuleValidator reject it.
        /// </summary>
        /// <param name="module"></param>
        protected virtual void ActivateModule([NotNull]PluginInfo module)
        {
            if (!IsActivatable(module))
                return;

            ValidateDependsModules(module.DependsOn);

            module.State = PluginState.Initializing;

            ActivateDependsModules(module.DependsOn);

            var instance = TryGetPlugin(module);

            if (instance == null)
            {
                module.State = PluginState.Invalid;
                throw new NotFoundException(module.ModuleType.FullName);
            }

            instance.Initialize();
            module.State = PluginState.Initialized;
        }

        protected virtual void ActivateDependsModules(Collection<string> dependsOnModuleNames)
        {
            if (dependsOnModuleNames?.Any() == false)
                return;

            // ReSharper disable once PossibleNullReferenceException
            foreach (var source in Plugins.Where(m => dependsOnModuleNames.Contains(m.ModuleName)))
                ActivateModule(source);
        }

        /// <summary>
        /// Validate module. 
        /// </summary>
        /// <param name="dependsOnModuleNames"></param>
        protected virtual void ValidateDependsModules(Collection<string> dependsOnModuleNames)
        {
            if (dependsOnModuleNames?.Any() == false)
                return;

            // ReSharper disable once AssignNullToNotNullAttribute
            var notfounds = dependsOnModuleNames.Where(m => Plugins.All(t => t.ModuleName != m)).ToArray();

            //Verify exists.
            if (notfounds.Any())
                throw new NotFoundException(notfounds);

        }

        public void Dispose() => Dispose(true);

        protected void Dispose(bool isDisposing)
        {
        }
    }
}
