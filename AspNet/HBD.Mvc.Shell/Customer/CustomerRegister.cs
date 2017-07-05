#region using

using System.ComponentModel.Composition;
using System.Web.Mvc;
using System.Web.Optimization;
using Customer.Controllers;
using HBD.Mef.Mvc;
using HBD.Mef.Mvc.Core;
using HBD.Mef.Mvc.Icons;
using HBD.Mef.Mvc.Navigation;

#endregion

namespace Customer
{
    [Export(typeof(IMefAreaRegistration))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CustomerAreaRegistration : MefAreaRegistration
    {
        public override string AreaName { get; } = "Customer";

        public override string PrimaryRouteName => $"{AreaName}_default";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapRoute(
                    PrimaryRouteName,
                    $"{AreaName}/{{controller}}/{{action}}/{{id}}",
                    new { controller = "View", action = "Index", id = UrlParameter.Optional },
                    new[] { $"{GetType().Namespace}.Controllers" }
                )
                .DataTokens.Add("area", AreaName);

            //Add more routes here.
        }

        public override void RegisterBundleResources(BundleCollection bundles)
        {
            base.RegisterBundleResources(bundles);
            bundles.AddAreaStyleBundle(AreaName)
                .Include("~/Content/*.css");

            bundles.AddAreaScriptBundle(AreaName)
                .Include("~/Scripts/jquery.nicescroll.js",
                    "~/Scripts/sidebar.js");
        }

        public override void RegisterNavigation(INavigationService navigationService)
        {
            navigationService.Menu("Import Data")
                .WithIcon(Glyphicon.glyphicon_import)
                .Items
                .AddSeperator()
                .AddNavigation(a => a.WithTitle($"Import {AreaName} From File")
                    .For<ImportController>(c => nameof(c.ImportFromFile)))
                .AddNavigation(a => a.WithTitle($"Import {AreaName} From Service")
                    .For<ImportController>(c => nameof(c.ImportFromService)));

            navigationService.Menu("Export Data")
                .WithIcon(Glyphicon.glyphicon_export)
                .Items
                .AddNavigation(a => a.WithTitle($"Export {AreaName} To Excel")
                    .For<ExportController>(c => nameof(c.ExportToExcel)))
                .AddNavigation(a => a.WithTitle($"Export {AreaName} To Json")
                    .For<ExportController>(c => nameof(c.ExportToJson)));

            navigationService.Menu("View Data")
                .Items
                .AddSeperator()
                .AddNavigation(a => a.WithTitle($"View {AreaName}")
                    .WithIcon("Images/customers.png")
                    .For<ViewController>(c => nameof(c.Index)))
                .AddNavigation(
                    a => a.WithTitle($"View {AreaName} Details.").For<ViewController>(c => nameof(c.Details)));
        }
    }
}