using System;
using HBD.Framework;

namespace HBD.Mef.Core.Navigation
{
    /// <summary>
    ///     The StartupViewInfo to launch the View when application started. This is also a
    ///     INavigationParameter for View launching purpose.
    /// </summary>
    public class ViewInfo : IViewInfo, INavigationParameter
    {
        public ViewInfo(string viewName) : this(viewName, null)
        {
        }

        public ViewInfo(Type viewType) : this(null, viewType)
        {
        }

        public ViewInfo(string viewName, Type viewType)
        {
            if (ViewName.IsNullOrEmpty() && (viewType == null))
                throw new ArgumentNullException($"Either {nameof(ViewName)} or {nameof(viewType)} must not be NULL");

            ViewName = viewName;
            ViewType = viewType;
        }

        public string ViewName { get; set; }

        public Type ViewType { get; set; }
    }
}