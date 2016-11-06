using System;

namespace HBD.Mef.Core.Navigation
{
    public sealed class NavigationParameterCollector<T> where T : INavigationInfo
    {
        public NavigationParameterCollector(INavigationInfo navigationInfo)
        {
            NavigationInfo = navigationInfo;
        }

        public INavigationInfo NavigationInfo { get; }

        public NavigationParameterCollector<T> AndFor(INavigationParameter parameter)
        {
            NavigationInfo.NavigationParameters.Add(parameter);
            return this;
        }

        public NavigationParameterCollector<T> AndFor(Action action)
        {
            NavigationInfo.NavigationParameters.Add(new ActionNavigationParameter(action));
            return this;
        }

        public NavigationParameterCollector<T> AndForView(Type viewType, string viewName = null)
        {
            NavigationInfo.NavigationParameters.Add(new ViewInfo(viewName, viewType));
            return this;
        }
    }
}