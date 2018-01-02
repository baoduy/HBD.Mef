#region using

using System.Collections.Generic;

#endregion

namespace HBD.Mef.Shell.Navigation
{
    public class NavigationInfo : MenuInfoBase, INavigationInfo
    {
        private IList<INavigationParameter> _navigationParameters;

        public NavigationInfo(IMenuInfoCollection parent) : base(parent)
        {
            NavigationParameters = new List<INavigationParameter>();
        }

        public IList<INavigationParameter> NavigationParameters
        {
            get { return _navigationParameters; }
            protected set { SetValue(ref _navigationParameters, value); }
        }
    }
}