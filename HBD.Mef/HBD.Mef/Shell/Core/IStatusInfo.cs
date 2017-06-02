#region using

using System.ComponentModel;

#endregion

namespace HBD.Mef.Shell.Core
{
    public interface IStatusInfo : INotifyPropertyChanged
    {
        string Message { get; set; }
        object Icon { get; set; }
    }
}