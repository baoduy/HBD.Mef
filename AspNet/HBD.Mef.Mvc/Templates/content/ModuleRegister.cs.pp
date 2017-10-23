#region using

using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Optimization;
using $rootnamespace$.Controllers;
using HBD.Mef.Mvc;
using HBD.Mef.Mvc.Core;
using HBD.Mef.Mvc.Icons;
using HBD.Mef.Mvc.Navigation;

#endregion

namespace $rootnamespace$
{
    [Export(typeof(IMefAreaRegistration))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ModuleRegister : MefAreaRegistration
    {
        /// <summary>
        /// Update your Module Name
        /// </summary>
        public override string AreaName => "NewModule";

        /// <summary>
        /// This is default Router in MefAreaRegistration
        /// Uncomment this and customize if needed.
        /// </summary>
        //
        //public override void RegisterArea(AreaRegistrationContext context)
        //{
        //    context.Routes.MapRoute(
        //            PrimaryRouteName,
        //            $"{AreaName}/{{controller}}/{{action}}/{{id}}",
        //            new {controller = "Home", action = "Index", id = UrlParameter.Optional},
        //            new[] {$"{GetType().Namespace}.Controllers"}
        //        )
        //        .DataTokens.Add("area", AreaName);
        //}

        /// <summary>
        /// Add the footer navigation here.
        /// </summary>
        /// <param name="footerNavigationService"></param>
        public override void RegisterFooterNavigation(IFooterNavigationService footerNavigationService)
        {
            
        }

        /// <summary>
        /// Add the Main menu navigation here.
        /// </summary>
        /// <param name="navigationService"></param>
        public override void RegisterNavigation(INavigationService navigationService)
        {
            navigationService
                .AddNavigation(a => a.WithTitle("New Module")
                    .For<HomeController>(c => nameof(c.Index)));
        }

        /// <summary>
        /// Add the bundles here.
        /// </summary>
        /// <param name="bundles"></param>
        public override void RegisterBundleResources(BundleCollection bundles)
        {
            //bundles.AddAreaStyleBundle(AreaName)
            //    .Include("~/Content/azurenote.css",
            //        "~/Content/sidebar.css",
            //        "~/Content/summernote/summernote.css");

            //bundles.AddAreaScriptBundle(AreaName)
            //    .Include("~/Scripts/jquery.nicescroll.js",
            //        "~/Scripts/sidebar.js",
            //        "~/Scripts/summernote/summernote.js",
            //        "~/Scripts/initializeEditor.js");
        }
    }
}