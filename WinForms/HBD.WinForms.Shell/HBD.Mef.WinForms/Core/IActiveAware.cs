using System;

namespace HBD.Mef.WinForms.Core
{
    public interface IActiveAware
    {
        bool IsActive { get; set; }
        event EventHandler IsActiveChanged;
    }
}
