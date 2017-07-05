using System.Collections.Generic;
using System.Composition.Convention;
using System.IO;
using System.Reflection;
using System.Linq;

namespace HBD.Mef.Catalogs
{
    public class MultiDirectoriesCatalog : Catalog
    {
        private readonly string[] _paths;
        private readonly SearchOption _searchOption;
        public IList<string> ExcludedBinaries { get; set; }

        public MultiDirectoriesCatalog(params string[] paths) : this(paths, SearchOption.TopDirectoryOnly)
        {
        }

        public MultiDirectoriesCatalog(string[] paths, SearchOption searchOption, ConventionBuilder context = null)
            : base(context)
        {
            _paths = paths;
            _searchOption = searchOption;
            ExcludedBinaries = new List<string> { "System", "Microsoft" };
        }

        private bool IsExcluded(string file)
          => ExcludedBinaries.Any(e =>
          {
              var fileName = Path.GetFileNameWithoutExtension(file);
              return fileName != null && fileName.StartsWith(e);
          });

        protected override IEnumerable<Assembly> GetAssemblies()
        {
            foreach (var path in _paths)
            {
                var fullPath = Path.GetFullPath(path);
                if (!Directory.Exists(fullPath))
                    continue;

                foreach (var file in Directory.GetFiles(fullPath, "*.dll", _searchOption))
                {
                    if (IsExcluded(file)) continue;

                    var assembly = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
                    if (assembly == null) continue;

                    yield return assembly;
                }
            }
        }
    }
}
