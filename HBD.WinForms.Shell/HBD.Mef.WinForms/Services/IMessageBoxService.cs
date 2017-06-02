#region using

using System;
using System.Windows.Forms;

#endregion

namespace HBD.Mef.WinForms.Services
{
    /// <summary>
    ///     The Message box service for WindowForms
    /// </summary>
    public interface IMessageBoxService
    {
        void Info(string message, string title = "Information");

        void Alert(string message, string title = "Alert");

        DialogResult Confirm(string message, string title = "Confirmation");

        void Show(Exception exception, string title = "Unhanded Exception");

        DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon);
    }
}