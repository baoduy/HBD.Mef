#region using

using System;
using System.Collections.Generic;
using HBD.Mef.Shell.Navigation;
using HBD.Mef.WinForms.Core;

#endregion

namespace HBD.Mef.WinForms.Services
{
    /// <summary>
    ///     The view navigation manager. That allows view navigate to the other view.
    /// </summary>
    public interface INavigationManager
    {
        NavigationResult NavigateTo(ViewInfo toView, IDictionary<string, object> parameters = null);
        NavigationResult NavigateTo(Type toViewType, IDictionary<string, object> parameters = null);
        NavigationResult NavigateTo(string toViewName, IDictionary<string, object> parameters = null);
    }
}