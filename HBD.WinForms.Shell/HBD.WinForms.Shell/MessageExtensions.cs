#region using

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace HBD.WinForms.Shell
{
    internal static class MessageExtensions
    {
        #region Show Message

        public static void ShowErrorMessage(Exception exception)
            => ShowErrorMessage(exception.Message);

        public static void ShowErrorMessage(string message)
            => MessageBox.Show(message, Resource.ShowMessage_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void ShowInfoMessage(string message)
            =>
                MessageBox.Show(message, Resource.ShowMessage_Information, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

        public static DialogResult ShowConfirmationMessage(string message)
            =>
                MessageBox.Show(message, Resource.ShowMessage_Confirmation, MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

        public static void ShowErrorMessage(this Component @this, Exception exception)
            => ShowErrorMessage(exception.Message);

        public static void ShowErrorMessage(this Component @this, string message)
            => MessageBox.Show(message, Resource.ShowMessage_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void ShowInfoMessage(this Component @this, string message)
            =>
                MessageBox.Show(message, Resource.ShowMessage_Information, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

        public static DialogResult ShowConfirmationMessage(this Component @this, string message)
            =>
                MessageBox.Show(message, Resource.ShowMessage_Confirmation, MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

        #endregion Show Message
    }
}