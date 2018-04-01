#region

using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using HBD.Mef.Mvc.Optimization;

#endregion

namespace HBD.Mef.Mvc
{
    public static class MefStyles
    {
        /// <summary>
        ///     Render the Script Bundles for an Area. Calling this after all Styles.Render in the _Layout page.
        /// </summary>
        /// <returns></returns>
        public static IHtmlString RenderAreaBundles()
        {
            return Styles.Render(BundleExtensions.GetBundlesForCurrentArea<AreaStyleBundle>()
                .Select(a => a.Path)
                .ToArray());
        }

        /// <summary>
        ///     Render the script bundles for page. Calling this method in the _Layout page after MefStyles.RenderForArea().
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IHtmlString RenderPageBundles(this WebViewPage @this)
        {
            var list = @this.GetOrCreateStyleBundles();
            return list.Count <= 0 ? null : Styles.Render(list.ToArray());
        }
    }
}