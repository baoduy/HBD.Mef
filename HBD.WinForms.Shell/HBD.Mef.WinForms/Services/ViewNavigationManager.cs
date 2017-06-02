#region using

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using HBD.Framework.Exceptions;
using HBD.Mef.Shell.Navigation;
using HBD.Mef.WinForms.Core;

#endregion

namespace HBD.Mef.WinForms.Services
{
    public class ViewNavigationManager : INavigationManager
    {
        public ViewNavigationManager()
        {
        }

        public ViewNavigationManager(IMainViewService mainViewService)
        {
            MainViewService = mainViewService;
        }

        [Import(AllowDefault = true, AllowRecomposition = true)]
        public IMainViewService MainViewService { protected get; set; }

        public NavigationResult NavigateTo(Type toViewType, IDictionary<string, object> parameters = null)
            => NavigateTo(new ViewInfo(toViewType), parameters);

        public NavigationResult NavigateTo(string toViewName, IDictionary<string, object> parameters = null)
            => NavigateTo(new ViewInfo(toViewName), parameters);

        public virtual NavigationResult NavigateTo(ViewInfo toView, IDictionary<string, object> parameters = null)
        {
            if (MainViewService == null)
                return new NavigationResult(new NotFoundException(nameof(IMainViewService)));

            var view = MainViewService.ActiveView(toView);

            var navAware = view as INavigationAware;

            if (navAware == null)
                return new NavigationResult(new Exception($"View {view.Text} is not a INavigationAware"));

            navAware.OnNavigatedTo(new WinformNavigationContext(parameters));

            return new NavigationResult();
        }
    }
}