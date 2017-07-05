#region using

using System.Collections.Concurrent;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using HBD.Mef.WinForms;
using HBD.Mef.WinForms.Services;

#endregion

namespace HBD.WinForms.Shell.Services
{
    [Export(typeof(ILoadingDialogService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LoadingDialogService : ILoadingDialogService
    {
        private readonly ConcurrentDictionary<IWin32Window, LoadingControl> _currentDialogs =
            new ConcurrentDictionary<IWin32Window, LoadingControl>();

        public void ShowLoading(IWin32Window owner, string message)
        {
            var control = owner as Control;

            var dialog = _currentDialogs.GetOrAdd(owner, new LoadingControl());
            dialog.Message = message;

            if (control.Controls.Contains(dialog)) return;
            control.Controls.Add(dialog);
            dialog.BringToFront();
            dialog.BringToCenter();
        }

        public void HideLoading(IWin32Window owner)
        {
            var control = owner as Control;

            LoadingControl dialog;
            if (!_currentDialogs.TryRemove(owner, out dialog)) return;

            control.Controls.Remove(dialog);
            dialog.Dispose();
        }
    }
}