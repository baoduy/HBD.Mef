using System;
using System.Windows.Forms;
using HBD.Mef.Core.Navigation;

namespace HBD.Mef.WinForms.Services
{
    public interface IMainViewService
    {
        void AddView(UserControl control);

        void AddView(Type viewType, string viewName = null);

        void AddView(IViewInfo viewInfo);

        void RemoveView(UserControl control);

        void RemoveView(Type viewType, string viewName = null);

        void RemoveView(IViewInfo viewInfo);
    }
}