using HBD.Framework;
using HBD.Framework.Attributes;
using HBD.Mef.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using HBD.Framework.Exceptions;

namespace HBD.Mef.Modularity
{
    [Export(typeof(IPluginManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class PluginManager : IPluginManager
    {
        [Import]
        protected AggregateCatalog AggregateCatalog { get; set; }

        [Import(AllowRecomposition = true, AllowDefault = true)]
        protected CompositionContainer ContainerService { get; set; }

        [Import(AllowRecomposition = true, AllowDefault = true)]
        protected ILogger Logger { get; set; }

        public IList<PluginInfo> Plugins { get; } = new List<PluginInfo>();
        public IList<IPlugin> ExportedModules { get; } = new List<IPlugin>();

        public void Run()
        {
            Plugins.AddRange(GetModuleInfos());

            if (!Plugins.Any()) return;

            ExportedModules.AddRange(ContainerService.GetExportedValues<IPlugin>());

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

                if (Plugins.Any(p => p.ModuleType == type)) continue;

                if (dependsOnModuleNames?.Any() == true)
                    yield return new PluginInfo(name, type, dependsOnModuleNames);
                else yield return new PluginInfo(name, type);
            }

            //foreach (var item in AggregateCatalog.Where(p => p.Metadata.ContainsKey(Constants.ModuleName)
            //                && p.Metadata.ContainsKey(Constants.ModuleType)))
            //{
            //    var name = item.Metadata[Constants.ModuleName] as string;
            //    var type = item.Metadata[Constants.ModuleType] as Type;
            //    yield return new PluginInfo(name, type);
            //}
        }

        protected virtual void ActivateModule([NotNull]PluginInfo module)
        {
            if (module.State == PluginState.Initializing
                || module.State == PluginState.Initialized)
                return;

            ValidateDependsModules(module.DependsOn);

            module.State = PluginState.Initializing;

            ActivateDependsModules(module.DependsOn);

            var instance = ExportedModules.FirstOrDefault(i => i.GetType() == module.ModuleType);

            if (instance == null)
            {
                module.State = PluginState.NotStarted;
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
    }
}
