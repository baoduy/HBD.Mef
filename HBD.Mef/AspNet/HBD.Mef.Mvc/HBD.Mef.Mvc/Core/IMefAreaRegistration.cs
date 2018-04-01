#region

using System.Web.Mvc;
using System.Web.Optimization;
using HBD.Mef.Mvc.Navigation;

#endregion

namespace HBD.Mef.Mvc.Core
{
    public interface IMefAreaRegistration
    {
        string AreaName { get; }

        /// <summary>
        ///     The name of primary or default route of this Area to identify that the Area already registed or not.
        /// </summary>
        string PrimaryRouteName { get; }

        /// <summary>
        ///     TThe area will be registers if it has not done yet.
        /// </summary>
        /// <param name="context"></param>
        void RegisterArea(AreaRegistrationContext context);

        /// <summary>
        ///     Register the main menu for the Area.
        /// </summary>
        /// <param name="navigationService"></param>
        void RegisterNavigation(INavigationService navigationService);

        /// <summary>
        ///     Register the links that will be display on the footer page.
        /// </summary>
        /// <param name="footerNavigationService"></param>
        void RegisterFooterNavigation(IFooterNavigationService footerNavigationService);

        /// <summary>
        ///     Register Area bundle resouces after app started.
        /// </summary>
        /// <param name="bundles"></param>
        void RegisterBundleResources(BundleCollection bundles);
    }
}