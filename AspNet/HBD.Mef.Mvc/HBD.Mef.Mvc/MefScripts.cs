#region using

using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using HBD.Framework;
using HBD.Mef.Mvc.Optimization;

#endregion

namespace HBD.Mef.Mvc
{
    public static class MefScripts
    {
        /// <summary>
        /// Render the Script Bundles for an Area. Calling this after all Scripts.Render in the _Layout page.
        /// </summary>
        /// <returns></returns>
        public static IHtmlString RenderAreaBundle()
        {
            var areaName = BundleExtensions.GetCurrentAreaName();
            if (areaName.IsNullOrEmpty()) return null;

            return Scripts.Render(BundleExtensions.GetBundlesFor<AreaScriptBundle>()
                .Where(a => a.AreaName.EqualsIgnoreCase(areaName))
                .Select(a => a.Path)
                .ToArray());
        }

        /// <summary>
        /// Render the script bundles for page. Calling this method in the _Layout page after MefScripts.RenderAreaBundle() and before RenderSection("scripts", false).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IHtmlString RenderPageBundles(this WebViewPage @this)
        {
            var list = @this.GetOrCreateScriptBundles();
            return list.Count <= 0 ? null : Scripts.Render(list.ToArray());
        }
    }
}