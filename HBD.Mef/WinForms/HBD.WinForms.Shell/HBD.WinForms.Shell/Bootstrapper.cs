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
using HBD.Mef.Modularity;
using HBD.Mef.Catalogs;

#endregion

namespace HBD.WinForms.Shell
{
    internal class Bootstrapper : MefWinFormBootstrapper
    {
        private IShellConfigManager ShellConfigManager { get; } = new ShellConfigManager();

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            this.AggregateCatalog.Catalogs.Add(
                new MultiDirectoriesCatalog(new[] { this.ShellConfigManager.ShellConfig.ModulePath }, 
                System.IO.SearchOption.AllDirectories, 
                CreateReflectionContext()));
        }

        protected override void RegisterExternalObjects()
        {
            base.RegisterExternalObjects();

            Container.ComposeExportedValue<IShellMenuService>(new ShellMenuService());
            Container.ComposeExportedValue<IStartupViewCollection>(new StartupViewCollection());
            Container.ComposeExportedValue(ShellConfigManager);
        }

        protected override Form CreateMainWindow() => Container.GetExportedValue<FrMain>();

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