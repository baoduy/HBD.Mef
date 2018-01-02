#region using

using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Optimization;
using HBD.Mef.Mvc;
using HBD.Mef.Mvc.Core;
using HBD.Mef.Mvc.Navigation;

#endregion

namespace HBD.Mef.MvcTests.TestClasses
{
    [Export(typeof(IMefAreaRegistration))]
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ExportingAreaRegistration : MefAreaRegistration
    {
        public override string AreaName => "AzureNote";

        public override string PrimaryRouteName => $"{AreaName}_default";

        public override void RegisterArea(AreaRegistrationContext context)
        {
        }

        public override void RegisterFooterNavigation(IFooterNavigationService footerNavigationService)
        {
            footerNavigationService.AddLink(l => l.WithTitle("Azure Source Code")
                    .WithIcon("https://assets-cdn.github.com/images/modules/logos_page/GitHub-Mark.png")
                    .For("https://github.com/baoduy"))
                .AddLink(a => a.For("http://drunkcoding.net"));
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