using System.Windows.Forms;
using HBD.Mef.Core;
using HBD.Mef.Core.Logging;

namespace HBD.Mef.WinForms
{
    /// <summary>
    ///     The Bootstrapper for Window Forms app.
    /// </summary>
    public abstract class MefWinFormBootstrapper : MefBootstrapper
    {
        public Form MainWindow { get; protected set; }

        protected abstract Form CreateMainWindow();

        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);

            Logger.Debug("Create MainWindow ");
            MainWindow = CreateMainWindow();

            if (MainWindow != null)
            {
                Logger.Debug("Run the Application");
                Application.Run(MainWindow);
            }
        }
    }
}