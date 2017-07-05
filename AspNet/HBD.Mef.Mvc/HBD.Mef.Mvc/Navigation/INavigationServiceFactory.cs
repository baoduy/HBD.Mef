#region using

using System.Collections.Generic;
using System.Security.Principal;
using HBD.Framework.Attributes;
using HBD.Mef.Mvc.Navigation.NavigateInfo;

#endregion

namespace HBD.Mef.Mvc.Navigation
{
    public interface INavigationServiceFactory
    {
        INavigationService CreateNavigationService([NotNull] string areaName);
        bool IsRegistered(string areaName);


        ICollection<IMenuInfo> GetLeftMenuFor(IPrincipal user);
        ICollection<IMenuInfo> GetRightMenuFor(IPrincipal user);
    }
}