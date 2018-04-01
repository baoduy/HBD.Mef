#region

using System.Web.Mvc;
using System.Web.Optimization;
using HBD.Mef.Mvc.Core;
using HBD.Mef.Mvc.Navigation;

#endregion

namespace HBD.Mef.Mvc
{
    public abstract class MefAreaRegistration : AreaRegistration, IMefAreaRegistration
    {
        public virtual string PrimaryRouteName => $"{AreaName}_Default";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapRoute(
                    PrimaryRouteName,
                    $"{AreaName}/{{controller}}/{{action}}/{{id}}",
                    new {controller = "Home", action = "Index", id = UrlParameter.Optional},
                    new[] {$"{GetType().Namespace}.Controllers"}
                )
                .DataTokens.Add("area", AreaName);
        }

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