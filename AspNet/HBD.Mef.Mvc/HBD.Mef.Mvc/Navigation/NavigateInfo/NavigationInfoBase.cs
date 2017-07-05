using System;
using System.Collections.Generic;
using System.Diagnostics;
using HBD.Framework.Core;
using HBD.Mef.Mvc.Core;

namespace HBD.Mef.Mvc.Navigation.NavigateInfo
{
    [DebuggerDisplay("Title = {" + nameof(Title) + "}")]
    public abstract class NavigationInfoBase : DisplayabilityBase, INavigationInfo, INavigateRole, IConable,
        ITitleObject
    {
        private bool _reguireAuthorized;

        protected NavigationInfoBase(string areaName)
        {
            AreaName = areaName;
            Roles = new List<string>();
        }

        public string AreaName { get; }
        public object Icon { get; set; }

        public bool ReguireAuthorized
        {
            get => Roles.Count > 0 || _reguireAuthorized;
            set => _reguireAuthorized = value;
        }

        public IList<string> Roles { get; protected set; }

        public abstract IMenuInfo Clone();

        object ICloneable.Clone()
        {
            return Clone();
        }

        public string Title { get; set; }
    }
}