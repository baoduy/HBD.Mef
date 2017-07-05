#region using

using HBD.Mef.Shell.Core;

#endregion

namespace HBD.Mef.Shell.Services
{
    /// <summary>
    ///     Provide the Service to interact with Shell Status bar
    /// </summary>
    public interface IShellStatusService
    {
        void SetStatus(string status);

        void Set(IStatusInfo info);
    }
}