#region using

using System;
using System.Windows.Forms;

#endregion

namespace HBD.Mef.WinForms.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public void Alert(string message, string title = "Alert")
            => Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        public DialogResult Confirm(string message, string title = "Confirmation")
            => Show(message, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

        public void Info(string message, string title = "Information")
            => Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

        public void Show(Exception exception, string title = "Unhanded Exception")
            => Show(exception.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

        public DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
            => MessageBox.Show(message, title, buttons, icon);
    }
}