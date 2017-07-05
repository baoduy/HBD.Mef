using System.Web.Mvc;
using System.Web.Optimization;
using HBD.Mef.Mvc.Core;
using HBD.Mef.Mvc.Navigation;

namespace HBD.Mef.Mvc
{
    public abstract class MefAreaRegistration : AreaRegistration, IMefAreaRegistration
    {
        public virtual string PrimaryRouteName => $"{AreaName}_Default";

        public virtual void RegisterFooterNavigation(IFooterNavigationService footerNavigationService)
        {
        }

        public virtual void RegisterBundleResources(BundleCollection bundles)
        {
        }

        public virtual void RegisterNavigation(INavigationService navigationService)
        {
        }
    }
}