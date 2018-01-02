using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using HBD.Framework.Attributes;
using HBD.Framework.Core;
using HBD.Mef.Mvc.Services;

namespace HBD.Mef.Mvc.Catalogs
{
    /// <summary>
    /// This catalog will help to load the binaries from "bin" and Area folders.
    /// </summary>
    public class DefaultMvcBinaryFoldersCatalog : ComposablePartCatalog, ICompositionElement
    {
        protected ConcurrentDictionary<string, AssemblyCatalog> AssemblyCatalogs { get; }
        protected ICustomBinaryManager CustomBinaryManager { get; }
        private bool _isInitialized;

        public DefaultMvcBinaryFoldersCatalog([NotNull]ICustomBinaryManager customBinaryManager)
        {
            Guard.ArgumentIsNotNull(customBinaryManager, nameof(customBinaryManager));

            CustomBinaryManager = customBinaryManager;
            AssemblyCatalogs = new ConcurrentDictionary<string, AssemblyCatalog>();
        }

        public virtual string DisplayName => "Default Mvc Binary Folders: '~\\bin\\*.dll' and '~\\Areas\\**\\*.dll'";
        ICompositionElement ICompositionElement.Origin => null;

        protected virtual void Initialize()
        {
            if (_isInitialized) return;
            _isInitialized = true;

            foreach (var assembly in CustomBinaryManager.LoadAssemblies($"{AppDomain.CurrentDomain.BaseDirectory}\\bin")
                .IncludedAppDomainAssemblies())
            {
                AssemblyCatalogs.GetOrAdd(assembly.FullName, new AssemblyCatalog(assembly));
            }

            foreach (var assembly in CustomBinaryManager.LoadAssemblies($"{AppDomain.CurrentDomain.BaseDirectory}\\Areas")
                .IncludedSubFolder())
            {
                AssemblyCatalogs.GetOrAdd(assembly.FullName, new AssemblyCatalog(assembly));
            }
        }

        public override IEnumerator<ComposablePartDefinition> GetEnumerator()
        {
            this.Initialize();
            return this.AssemblyCatalogs.SelectMany(k => k.Value).GetEnumerator();
        }

        public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(
            ImportDefinition definition)
        {
            this.Initialize();
            return this.AssemblyCatalogs.SelectMany(k => k.Value.GetExports(definition));
        }
    }
}
