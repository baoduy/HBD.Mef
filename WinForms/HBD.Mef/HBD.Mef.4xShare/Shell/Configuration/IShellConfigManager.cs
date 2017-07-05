#region using

using HBD.Mef.Logging;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

#endregion

namespace HBD.Mef.Shell.Configuration
{
    public interface IShellConfigManager<out TShellConfig, TModuleConfig> where TShellConfig : ShellConfig, new()
        where TModuleConfig : ModuleConfig, new()
    {
        /// <summary>
        ///     The Shell.json config file must be in the Startup location of the application.
        /// </summary>
        TShellConfig ShellConfig { get; }

        ModuleConfigCollection<TModuleConfig> Modules { get; }

        /// <summary>
        ///     Import the binaries that had been configured in the ShellConfig.ImportedBinaries.
        /// </summary>
        /// <param name="catalog"></param>
        void ImportShellBinaries(AggregateCatalog catalog, ReflectionContext reflectionContext = null);

        /// <summary>
        ///     The modules configuration will be loaded based on the config in the Shell.ModulePath and
        ///     import all the require binaries to the Mef.
        /// </summary>
        /// <param name="catalog"></param>
        void ImportModuleBinaries(AggregateCatalog catalog, ReflectionContext reflectionContext = null);

        void SaveChanges(ILogger logger = null);
        void SaveChanges(TModuleConfig module);
        void UndoChanges();
        void UndoChanges(TModuleConfig module);
    }

    public interface IShellConfigManager : IShellConfigManager<ShellConfig, ModuleConfig>
    {
    }
}