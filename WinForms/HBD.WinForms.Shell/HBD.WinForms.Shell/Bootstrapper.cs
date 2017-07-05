#region using

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows.Forms;
using HBD.Mef.Logging;
using HBD.Mef.Shell.Configuration;
using HBD.Mef.Shell.Navigation;
using HBD.Mef.Shell.Services;
using HBD.Mef.WinForms;
using HBD.WinForms.Shell.Properties;

#endregion

namespace HBD.WinForms.Shell
{
    internal class Bootstrapper : MefWinFormBootstrapper
    {
        private IShellConfigManager ShellConfigManager { get; } = new ShellConfigManager();

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            var rfc = CreateReflectionContext();

            Logger.Info("Import Shell Binaries");
            ShellConfigManager.ImportShellBinaries(AggregateCatalog, rfc);

            Logger.Info("Import Module Binaries");
            ShellConfigManager.ImportModuleBinaries(AggregateCatalog, rfc);
        }

        protected override void RegisterBootstrapperProvidedTypes()
        {
            base.RegisterBootstrapperProvidedTypes();
            Container.ComposeExportedValue<IShellMenuService>(new ShellMenuService());
            Container.ComposeExportedValue<IStartupViewCollection>(new StartupViewCollection());
            Container.ComposeExportedValue(ShellConfigManager);
        }

        protected override Form CreateMainWindow()
        {
            return Container.GetExportedValue<FrMain>();
        }

        protected override void InitializeApplication()
        {
            try
            {
                base.InitializeApplication();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}