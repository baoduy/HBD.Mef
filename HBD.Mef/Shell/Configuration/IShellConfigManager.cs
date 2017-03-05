#region

using System.ComponentModel.Composition.Hosting;
using Prism.Logging;

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
        void ImportShellBinaries(AggregateCatalog catalog);

        /// <summary>
        ///     The modules configuration will be loaded based on the config in the Shell.ModulePath and
        ///     import all the require binaries to the Mef.
        /// </summary>
        /// <param name="catalog"></param>
        void ImportModuleBinaries(AggregateCatalog catalog);

        void SaveChanges(ILoggerFacade logger = null);
        void SaveChanges(TModuleConfig module);
        void UndoChanges();
        void UndoChanges(TModuleConfig module);
    }
}