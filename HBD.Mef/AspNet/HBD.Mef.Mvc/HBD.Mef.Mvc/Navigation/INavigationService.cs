#region using

using System.Collections.Generic;
using HBD.Framework.Attributes;
using HBD.Mef.Mvc.Navigation.NavigateInfo;

#endregion

namespace HBD.Mef.Mvc.Navigation
{
    public interface INavigationService
    {
        string AreaName { get; set; }
        IList<IMenuInfo> Items { get; }
        MenuInfo Menu([NotNull] string title);
    }
}