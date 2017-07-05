#region using

using System.Web.Mvc;

#endregion

namespace HBD.Mef.Mvc.Navigation.NavigateInfo
{
    public class ViewInfo : LazyInfo<WebViewPage, string>
    {
        public ViewInfo(string areaName) : base(areaName)
        {
        }

        public override IMenuInfo Clone()
        {
            return new ViewInfo(AreaName)
            {
                Roles = Roles,
                ReguireAuthorized = ReguireAuthorized,
                Icon = Icon,
                DisplayType = DisplayType,
                DisplayIndex = DisplayIndex,
                Alignment = Alignment,
                InfoGetter = InfoGetter,
                Title = Title
            };
        }
    }
}