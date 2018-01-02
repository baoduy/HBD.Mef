#region using

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Principal;
using HBD.Mef.Mvc.Navigation.NavigateInfo;

#endregion

namespace HBD.Mef.Mvc.Navigation
{
    [Export(typeof(IFooterNavigationService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FooterNavigationService : IFooterNavigationService
    {
        public FooterNavigationService()
        {
            Items = new List<INavigationInfo>();
        }

        public List<INavigationInfo> Items { get; }

        public IList<INavigationInfo> GetMenuFor(IPrincipal user)
        {
            return Items.Where(i => i.IsValidRoles(user)).ToList();
        }
    }
}