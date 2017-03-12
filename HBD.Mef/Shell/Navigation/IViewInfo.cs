#region using

using System;

#endregion

namespace HBD.Mef.Shell.Navigation
{
    public interface IViewInfo
    {
        string ViewName { get; set; }
        Type ViewType { get; set; }
    }
}