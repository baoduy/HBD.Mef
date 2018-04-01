#region

using System;
using System.Collections.Generic;
using System.Linq;
using HBD.Framework.Core;

#endregion

namespace HBD.Mef.Mvc.Navigation.NavigateInfo
{
    public class NavigationCollection : List<INavigationInfo>, IParentable<MenuInfo>, ICloneable<NavigationCollection>
    {
        internal NavigationCollection()
        {
        }

        public NavigationCollection(MenuInfo parent)
        {
            Parent = parent;
        }

        public NavigationCollection Clone()
        {
            var list = new NavigationCollection();
            list.AddRange(this.Select(i => i.Clone() as INavigationInfo));
            return list;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public MenuInfo Parent { get; }
    }
}