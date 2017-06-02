#region using

using System.ComponentModel.Composition;
using System.Windows.Forms;
using HBD.Mef.Logging;
using HBD.Mef.WinForms.Services;

#endregion

namespace HBD.Mef.WinForms
{
    /// <summary>
    ///     The Bootstrapper for Window Forms app.
    /// </summary>
    public abstract class MefWinFormBootstrapper : MefBootstrapper
    {
        public Form MainWindow { get; private set; }

        protected override void RegisterBootstrapperProvidedTypes()
        {
            base.RegisterBootstrapperProvidedTypes();

            Container.ComposeExportedValue<IMessageBoxService>(new MessageBoxService());
            var navm = new ViewNavigationManager();
            Container.ComposeParts(navm);
            Container.ComposeExportedValue<INavigationManager>(navm);
        }

        protected abstract Form CreateMainWindow();

        protected override void InitializeApplication()
        {
            base.InitializeApplication();

            Logger.Debug("Create MainWindow ");
            MainWindow = CreateMainWindow();
        }

        protected override void RunApplication()
        {
            if (MainWindow == null) return;
            Logger.Debug("Run the Application");
            Application.Run(MainWindow);
        }
    }
}