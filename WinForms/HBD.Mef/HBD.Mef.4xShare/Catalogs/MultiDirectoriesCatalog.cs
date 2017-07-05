#region using

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using HBD.Framework;
using HBD.Framework.Attributes;

#endregion

namespace HBD.Mef.Catalogs
{
    /// <summary>
    ///     Load the assemblies from the paths dynamically. If there are 2 version of the same binary it will load 1 only.
    ///     The ExcludedBinaries can be configured in the App setting with the format as below.
    ///     key:'MultiDirectoriesCatalog:ExcludedBinaries' value:'The name of assemblies separate with comma'
    /// </summary>
    public class MultiDirectoriesCatalog : ComposablePartCatalog, ICompositionElement
    {
        public const string AppSettingKey = "MultiDirectoriesCatalog:ExcludedBinaries";
        private readonly ReflectionContext _context;
        private readonly string[] _paths;
        private readonly SearchOption _searchOption;
        private bool _isInitialized;

        public MultiDirectoriesCatalog(params string[] paths) : this(paths, SearchOption.TopDirectoryOnly)
        {
        }

        public MultiDirectoriesCatalog(string[] paths, SearchOption searchOption, ReflectionContext context = null)
        {
            _paths = paths;
            _searchOption = searchOption;
            _context = context;
            Catalogs = new List<AssemblyCatalog>();
            //SubAppDomains = new ConcurrentDictionary<string, AppDomain>();
            ExcludedBinaries = new List<string> {"System", "Microsoft"};
        }

        //protected ConcurrentDictionary<string, AppDomain> SubAppDomains { get; }

        public IReadOnlyCollection<AssemblyCatalog> LoadedCatalogs
        {
            get
            {
                Initialize();
                return Catalogs.ToReadOnly();
            }
        }

        protected IList<AssemblyCatalog> Catalogs { get; }

        public IList<string> ExcludedBinaries { get; }

        public string DisplayName => nameof(MultiDirectoriesCatalog);

        public ICompositionElement Origin => null;

        public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(
            ImportDefinition definition)
        {
            Initialize();
            return Catalogs.SelectMany(c => c.GetExports(definition));
        }

        public override IEnumerator<ComposablePartDefinition> GetEnumerator()
        {
            Initialize();
            return Catalogs.SelectMany(c => c.Parts).GetEnumerator();
        }

        protected virtual void Initialize()
        {
            if (_isInitialized) return;

            lock (_paths)
            {
                var excluedSetting = ConfigurationManager.AppSettings[AppSettingKey];
                if (excluedSetting.IsNotNullOrEmpty())
                    ExcludedBinaries.AddRange(excluedSetting.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries));

                foreach (var path in _paths)
                {
                    var fullPath = Path.GetFullPath(path);
                    if (!Directory.Exists(fullPath))
                        continue;

                    foreach (var file in Directory.GetFiles(fullPath, "*.dll", _searchOption))
                    {
                        if (IsExcluded(file)
                            || IsLoaded(file)) continue;

                        try
                        {
                            var assembly = GetOrLoad(file);
                            if (assembly == null) continue;

                            var ctg = _context == null
                                ? new AssemblyCatalog(assembly)
                                : new AssemblyCatalog(assembly, _context);

                            Catalogs.Add(ctg);
                        }
                        catch (Exception)
                        {
                            //Ignore
                        }
                    }
                }
                _isInitialized = true;
            }
        }

        private bool IsLoaded(string file)
            => Catalogs.Any(a =>
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                return fileName != null && Path.GetFileNameWithoutExtension(a.Assembly.CodeBase).StartsWith(fileName);
            });

        private bool IsExcluded(string file)
            => ExcludedBinaries.Any(e =>
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                return fileName != null && fileName.StartsWith(e);
            });
        
        private static Assembly GetOrLoad([NotNull] string file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            if (fileName == null)
                throw new InvalidDataException($"File:{file}");

            var loadedAss = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => Path.GetFileNameWithoutExtension(a.CodeBase).StartsWith(fileName));

            if (loadedAss != null) return loadedAss;
            return Assembly.LoadFrom(file);
        }
    }
}
