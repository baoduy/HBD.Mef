#region using

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using HBD.Framework;
using HBD.Framework.Attributes;
using HBD.Framework.Core;
using HBD.Mef.Mvc.Navigation.NavigateInfo;

#endregion

namespace HBD.Mef.Mvc.Navigation
{
    [Export(typeof(INavigationService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NavigationService : INavigationService
    {
        public NavigationService() : this(null)
        {
        }


        public NavigationService(string areaName)
        {
            AreaName = areaName;
            Items = new List<IMenuInfo>();
        }

        public string AreaName { get; set; }
        public IList<IMenuInfo> Items { get; }

        public MenuInfo Menu([NotNull] string title)
        {
            Guard.ArgumentIsNotNull(title, nameof(title));

            var item = GetItemByTitle(title);

            if (item == null)
            {
                var newItem = new MenuInfo(AreaName){Title = title};
                Items.Add(newItem);
                return newItem;
            }

            var info = item as MenuInfo;
            if (info != null)
                return info;

            throw new InvalidOperationException($"The item with Title {title} is existed. But it is not MenuInfo");
        }

        private IMenuInfo GetItemByTitle([NotNull] string title)
        {
            var item = Items.OfType<ITitleObject>().FirstOrDefault(i => i.Title.EqualsIgnoreCase(title));
            return item as IMenuInfo;
        }
    }
}