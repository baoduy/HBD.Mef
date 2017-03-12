#region using

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using HBD.Framework;
using Prism.Logging;
using Prism.Modularity;

#endregion

namespace HBD.Mef.Modularity
{
    [Export(typeof(IModuleManager))]
    public class MefModuleManager : ModuleManager, IPartImportsSatisfiedNotification
    {
        [Import]
        public MefFileModuleTypeLoader MefFileModuleTypeLoader { protected get; set; }

        private IEnumerable<IModuleTypeLoader> _mefTypeLoaders;

        [ImportingConstructor]
        public MefModuleManager(IModuleInitializer moduleInitializer, IModuleCatalog moduleCatalog,
            ILoggerFacade loggerFacade)
            : base(moduleInitializer, moduleCatalog, loggerFacade)
        {
        }

        [ImportMany(AllowRecomposition = true)]
        protected IEnumerable<Lazy<IModule, IModuleExport>> ImportedModules { get; set; }

        public override IEnumerable<IModuleTypeLoader> ModuleTypeLoaders
        {
            get
            {
                return _mefTypeLoaders ?? (_mefTypeLoaders = new List<IModuleTypeLoader> {MefFileModuleTypeLoader});
            }
            set { _mefTypeLoaders = value; }
        }

        public virtual void OnImportsSatisfied()
        {
            IDictionary<string, ModuleInfo> dictionary = ModuleCatalog.Modules.ToDictionary(m => m.ModuleName);
            foreach (var importedModule in ImportedModules)
            {
                var moduleType = importedModule.Metadata.ModuleType;

                ModuleInfo moduleInfo1;
                if (!dictionary.TryGetValue(importedModule.Metadata.ModuleName, out moduleInfo1))
                {
                    var moduleInfo2 = new ModuleInfo
                    {
                        ModuleName = importedModule.Metadata.ModuleName,
                        ModuleType = moduleType.AssemblyQualifiedName,
                        InitializationMode = importedModule.Metadata.InitializationMode,
                        State =
                            (ModuleState)
                            (importedModule.Metadata.InitializationMode == InitializationMode.OnDemand ? 0 : 2)
                    };
                    if (importedModule.Metadata.DependsOnModuleNames != null)
                        moduleInfo2.DependsOn.AddRange(importedModule.Metadata.DependsOnModuleNames);
                    ModuleCatalog.AddModule(moduleInfo2);
                }
                else
                {
                    moduleInfo1.ModuleType = moduleType.AssemblyQualifiedName;
                }
            }
            LoadModulesThatAreReadyForLoad();
        }

        /// <summary>
        ///     Checks if the module needs to be retrieved before it's initialized.
        /// </summary>
        /// <param name="moduleInfo">Module that is being checked if needs retrieval.</param>
        /// <returns>True if the module needs to be retrieved. Otherwise, false.</returns>
        protected override bool ModuleNeedsRetrieval(ModuleInfo moduleInfo)
            =>
                ImportedModules == null ||
                ImportedModules.All(lazyModule => lazyModule.Metadata.ModuleName != moduleInfo.ModuleName);
    }
}