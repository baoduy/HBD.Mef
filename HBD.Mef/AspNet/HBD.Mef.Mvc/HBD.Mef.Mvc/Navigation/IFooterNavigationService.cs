#region

using System.Collections.Generic;
using System.Security.Principal;
using HBD.Mef.Mvc.Navigation.NavigateInfo;

#endregion

namespace HBD.Mef.Mvc.Navigation
{
    public interface IFooterNavigationService
    {
        List<INavigationInfo> Items { get; }

        IList<INavigationInfo> GetMenuFor(IPrincipal user);
    }
}