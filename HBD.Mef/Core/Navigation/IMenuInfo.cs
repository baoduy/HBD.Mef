using System.ComponentModel;
using HBD.Framework.Core;

namespace HBD.Mef.Core.Navigation
{
    public interface IMenuInfo : IParentable<IMenuInfoCollection>, INotifyPropertyChanged
    {
        MenuAlignment Alignment { get; set; }
    }
}