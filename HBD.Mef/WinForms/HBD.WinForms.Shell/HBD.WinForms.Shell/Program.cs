#region using

using System;
using System.Windows.Forms;

#endregion

namespace HBD.WinForms.Shell
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new Bootstrapper().Run();
        }
    }
}