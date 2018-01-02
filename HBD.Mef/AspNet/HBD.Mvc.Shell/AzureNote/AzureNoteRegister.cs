#region using

using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Optimization;
using AzureNote.Controllers;
using HBD.Mef.Mvc;
using HBD.Mef.Mvc.Core;
using HBD.Mef.Mvc.Icons;
using HBD.Mef.Mvc.Navigation;

#endregion

namespace AzureNote
{
    [Export(typeof(IMefAreaRegistration))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AzureNoteAreaRegistration : MefAreaRegistration
    {
        public override string AreaName => "AzureNote";

        public override string PrimaryRouteName => $"{AreaName}_default";

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

        public override void RegisterFooterNavigation(IFooterNavigationService footerNavigationService)
        {
            footerNavigationService.AddLink(l => l.WithTitle("Azure Source Code")
                    .WithIcon("https://assets-cdn.github.com/images/modules/logos_page/GitHub-Mark.png")
                    .For("https://github.com/baoduy"))
                .AddLink(a => a.For("http://drunkcoding.net"));
        }

        public override void RegisterNavigation(INavigationService navigationService)
        {
            navigationService.AddNavigation(a => a.WithTitle("Azure Note")
                .WithIcon(FontAwesome.fa_cloud)
                .For<HomeController>(c => nameof(c.Index)));
        }

        public override void RegisterBundleResources(BundleCollection bundles)
        {
            bundles.AddAreaStyleBundle(AreaName)
                .Include("~/Content/azurenote.css",
                    "~/Content/sidebar.css",
                    "~/Content/summernote/summernote.css");

            bundles.AddAreaScriptBundle(AreaName)
                .Include("~/Scripts/jquery.nicescroll.js",
                    "~/Scripts/sidebar.js",
                    "~/Scripts/summernote/summernote.js",
                    "~/Scripts/initializeEditor.js");
        }
    }
}