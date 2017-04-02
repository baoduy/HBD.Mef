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
    [Export(typeof(IHbdModuleManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class HbdModuleManager : IHbdModuleManager
    {
        [Import]
        protected AggregateCatalog AggregateCatalog { get; set; }

        [Import(AllowRecomposition = true, AllowDefault = true)]
        protected CompositionContainer ContainerService { get; set; }

        [Import(AllowRecomposition = true, AllowDefault = true)]
        protected ILogger Logger { get; set; }

        private IList<ModuleInfo> Modules { get; } = new List<ModuleInfo>();
        private IList<IHbdModule> ExportedModules { get; } = new List<IHbdModule>();

        public void Run()
        {


            Modules.AddRange(GetModuleInfos());

            if (!Modules.Any()) return;

            ExportedModules.AddRange(ContainerService.GetExportedValues<IHbdModule>());

            foreach (var moduleInfo in Modules)
                ActivateModule(moduleInfo);
        }

        private IEnumerable<ModuleInfo> GetModuleInfos()
        {
            foreach (var item in AggregateCatalog.SelectMany(p => p.ExportDefinitions)
                .Where(p => p.Metadata.ContainsKey("ModuleName")
                            && p.Metadata.ContainsKey("ModuleType")
                            && p.Metadata.ContainsKey("DependsOnModuleNames")))
            {
                var name = item.Metadata["ModuleName"] as string;
                var type = item.Metadata["ModuleType"] as Type;
                var dependsOnModuleNames = item.Metadata["DependsOnModuleNames"] as string[];

                if (dependsOnModuleNames?.Any() == true)
                    yield return new ModuleInfo(name, type, dependsOnModuleNames);
                else yield return new ModuleInfo(name, type);
            }
        }

        protected virtual void ActivateModule([NotNull]ModuleInfo module)
        {
            if (module.State == ModuleState.Initializing
                || module.State == ModuleState.Initialized)
                return;

            ValidateDependsModules(module.DependsOn);

            module.State = ModuleState.Initializing;

            ActivateDependsModules(module.DependsOn);

            var instance =  ExportedModules.FirstOrDefault(i=>i.GetType()==module.ModuleType);

            if (instance == null)
            {
                module.State = ModuleState.NotStarted;
                throw new NotFoundException(module.ModuleType.FullName);
            }

            instance.Initialize();
            module.State = ModuleState.Initialized;
        }

        protected virtual void ActivateDependsModules(Collection<string> dependsOnModuleNames)
        {
            if (dependsOnModuleNames?.Any() == false)
                return;

         

            // ReSharper disable once PossibleNullReferenceException
            foreach (var source in Modules.Where(m => dependsOnModuleNames.Contains(m.ModuleName)))
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
            var notfounds = dependsOnModuleNames.Where(m => Modules.All(t => t.ModuleName != m)).ToArray();

            //Verify exists.
            if (notfounds.Any())
                throw new NotFoundException(notfounds);

        }
    }
}
