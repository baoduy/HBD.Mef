#region using

using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using HBD.Framework.Core;
using HBD.Mef.Logging;
using HBD.Mef.Catalogs;

#endregion

namespace HBD.Mef.Shell.Configuration
{
    public abstract class ShellConfigManager<TShellConfig> : IShellConfigManager<TShellConfig> where TShellConfig : ShellConfig, new()
    {
        private static ChangeTrackingEntry<TShellConfig> _shellConfigEntry;

        protected ShellConfigManager()
        {
        }

        protected ShellConfigManager(string shellConfigFile)
        {
            Guard.ArgumentIsNotNull(shellConfigFile, nameof(shellConfigFile));
            ShellConfigFile = shellConfigFile;
        }

        protected virtual string ShellConfigFile { get; } = "Shell.json";

        /// <summary>
        ///     The Shell.json config file must be in the Startup location of the application.
        /// </summary>
        public virtual TShellConfig ShellConfig
        {
            get
            {
                if (_shellConfigEntry != null) return _shellConfigEntry.Entity;
                _shellConfigEntry =
                    new ChangeTrackingEntry<TShellConfig>(JsonConfigHelper.ReadConfig<TShellConfig>(ShellConfigFile));
                return _shellConfigEntry.Entity;
            }
        }

        private MultiDirectoriesCatalog CreateMultiDirectoriesCatalog(string location, ReflectionContext reflectionContext = null)
            => new MultiDirectoriesCatalog(new[] { location }, SearchOption.TopDirectoryOnly, reflectionContext);

        private AssemblyCatalog CreateAssemblyCatalog(Assembly assembly, ReflectionContext reflectionContext = null)
            => reflectionContext == null ? new AssemblyCatalog(assembly) : new AssemblyCatalog(assembly, reflectionContext);

        public virtual void SaveChanges(ILogger logger = null)
        {
            if (_shellConfigEntry?.IsChanged == true)
            {
                JsonConfigHelper.SaveConfig(_shellConfigEntry.Entity, ShellConfigFile);
                _shellConfigEntry.AcceptChanges();
            }
        }

        public virtual void UndoChanges()
        {
            if (_shellConfigEntry?.IsChanged == true)
                _shellConfigEntry.UndoChanges();
        }

    }

    public class ShellConfigManager : ShellConfigManager<ShellConfig>, IShellConfigManager
    {
    }
}