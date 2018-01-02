#region using

using HBD.Mef.Logging;

#endregion

namespace HBD.Mef.Shell.Configuration
{
    public interface IShellConfigManager<out TShellConfig> where TShellConfig : ShellConfig, new()
    {
        /// <summary>
        ///     The Shell.json config file must be in the Startup location of the application.
        /// </summary>
        TShellConfig ShellConfig { get; }

        void SaveChanges(ILogger logger = null);
        void UndoChanges();
    }

    public interface IShellConfigManager : IShellConfigManager<ShellConfig>
    {
    }
}