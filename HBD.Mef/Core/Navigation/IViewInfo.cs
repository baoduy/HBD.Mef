using System;

namespace HBD.Mef.Core.Navigation
{
    public interface IViewInfo
    {
        string ViewName { get; set; }
        Type ViewType { get; set; }
    }
}