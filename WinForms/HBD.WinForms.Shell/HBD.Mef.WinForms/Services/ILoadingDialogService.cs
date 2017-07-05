#region using

using System.Windows.Forms;

#endregion

namespace HBD.Mef.WinForms.Services
{
    public interface ILoadingDialogService
    {
        void ShowLoading(IWin32Window owner, string message);

        void HideLoading(IWin32Window owner);
    }
}