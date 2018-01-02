#region using

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HBD.Mef.Shell.Navigation;

#endregion

namespace HBD.Mef.WinForms.Services
{
    /// <summary>
    ///     The interface of MainView Control that allow to add multi views in. The poplar instance if
    ///     this one usually a TabControl.
    /// </summary>
    public interface IMainViewService
    {
        IReadOnlyCollection<UserControl> Views { get; }
        void ActiveView(UserControl control);

        UserControl ActiveView(Type viewType, string viewName = null);

        UserControl ActiveView(IViewInfo viewInfo);

        void DeactivateView(UserControl control);

        void DeactivateView(Type viewType, string viewName = null);

        void DeactivateView(IViewInfo viewInfo);
    }
}