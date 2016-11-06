using System.Drawing;

namespace HBD.Mef.WinForms.Services
{
    public interface IStatusBarService
    {
        void SetStatus(string status);

        void SetStatus(string status, Color color);
    }
}