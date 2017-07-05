#region using

using HBD.Framework;

#endregion

namespace HBD.Mef.Mvc.Navigation.NavigateInfo
{
    public class LinkInfo : NavigationInfoBase
    {
        public LinkInfo(string areaName = null) : base(areaName)
        {
        }

        public string Link { get; set; }

        public override IMenuInfo Clone()
        {
            return new LinkInfo(AreaName)
            {
                Roles = Roles,
                Icon = Icon,
                Title = Title,
                DisplayType = DisplayType,
                DisplayIndex = DisplayIndex,
                Alignment = Alignment,
                Link = Link,
                ReguireAuthorized = ReguireAuthorized
            };
        }

        public void For(string link)
        {
            if (Title.IsNullOrEmpty())
                Title = link;
            Link = link;
        }
    }
}