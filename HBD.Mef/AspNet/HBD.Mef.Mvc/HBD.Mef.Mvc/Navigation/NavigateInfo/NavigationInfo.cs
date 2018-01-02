#region using

#endregion

namespace HBD.Mef.Mvc.Navigation.NavigateInfo
{
    public class NavigationInfo : NavigationInfoBase
    {
        public NavigationInfo(string areaName) : base(areaName)
        {
        }

        public string Controller { get; set; }
        public string Action { get; set; }
        public bool IsRootLevel { get; internal set; }

        public override IMenuInfo Clone()
        {
            return new NavigationInfo(AreaName)
            {
                Roles = Roles,
                Title = Title,
                Icon = Icon,
                DisplayType = DisplayType,
                DisplayIndex = DisplayIndex,
                Alignment = Alignment,
                Action = Action,
                Controller = Controller,
                ReguireAuthorized = ReguireAuthorized,
                IsRootLevel = IsRootLevel
            };
        }
    }
}