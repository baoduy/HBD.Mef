#region using

using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows.Forms;
using HBD.Mef.Logging;
using HBD.Mef.WinForms.Services;
using System.ComponentModel.Composition.Registration;
using System.Linq;
#endregion

namespace HBD.Mef.WinForms
{
    /// <summary>
    ///     The Bootstrapper for Window Forms app.
    /// </summary>
    public abstract class MefWinFormBootstrapper : MefBootstrapper
    {
        public Form MainWindow { get; private set; }

        protected override RegistrationBuilder CreateReflectionContext()
        {
            var registration = base.CreateReflectionContext();

            registration.ForTypesMatching(t => typeof(ViewBase).IsAssignableFrom(t)
                    && t.GetCustomAttribute<ExportAttribute>() == null)
                .SetCreationPolicy(CreationPolicy.NonShared).Export().Export<UserControl>();

            registration.ForTypesMatching(t => typeof(FormBase).IsAssignableFrom(t)
                    && !t.GetCustomAttributes<ExportAttribute>().Any())
                .SetCreationPolicy(CreationPolicy.NonShared).Export().Export<Form>();

            return registration;
        }

        protected override void RegisterExternalObjects()
        {
            base.RegisterExternalObjects();

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