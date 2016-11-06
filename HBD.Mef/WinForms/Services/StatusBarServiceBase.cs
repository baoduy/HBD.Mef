using System.Drawing;

namespace HBD.Mef.WinForms.Services
{
    public abstract class StatusBarServiceBase : IStatusBarService
    {
        public void SetStatus(string status)
            => SetStatus(status, Color.Empty);

        public abstract void SetStatus(string status, Color color);
    }
}