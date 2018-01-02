using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Reflection;
using System.Linq;

namespace HBD.Mef.Catalogs
{
    public abstract class Catalog : ICatalog
    {
        readonly Lazy<List<Assembly>> _assemblies;

        protected Catalog() : this(null) { }

        protected Catalog(AttributedModelProvider provider)
        {
            Provider = provider;
            _assemblies = new Lazy<List<Assembly>>(() => GetAssemblies().ToList());
        }

        public IReadOnlyCollection<Assembly> Assemblies => _assemblies.Value;

        public AttributedModelProvider Provider { get; }

        protected abstract IEnumerable<Assembly> GetAssemblies();
    }
}
