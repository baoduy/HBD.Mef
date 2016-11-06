using System.Windows.Forms;

namespace HBD.Mef.WinForms.Services
{
    public interface ILoadingDialodService
    {
        void ShowLoading(IWin32Window owner, string message);

        void HideLoading(IWin32Window owner);
    }
}