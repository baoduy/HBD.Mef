#region using

using System;
using System.Diagnostics;
using HBD.Framework.Core;
using HBD.Mef.Mvc.Core;

#endregion

namespace HBD.Mef.Mvc.Navigation.NavigateInfo
{
    [DebuggerDisplay("Title = {" + nameof(Title) + "}")]
    public class MenuInfo : DisplayabilityBase, IMenuInfo, ITitleObject, IConable
    {
        public MenuInfo(string areaName)
        {
            AreaName = areaName;
            Items = new NavigationCollection(this);
            DisplayIndex = ushort.MaxValue;
        }

        public NavigationCollection Items { get; private set; }

        public string AreaName { get; }

        public object Icon { get; set; }

        public IMenuInfo Clone()
        {
            return new MenuInfo(AreaName)
            {
                Alignment = Alignment,
                DisplayIndex = DisplayIndex,
                DisplayType = DisplayType,
                Icon = Icon,
                Items = Items.Clone(),
                Title = Title
            };
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public string Title { get; set; }
    }
}