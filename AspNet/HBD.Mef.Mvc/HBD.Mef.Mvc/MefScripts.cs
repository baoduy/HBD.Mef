#region using

using System.Linq;
using System.Web;
using System.Web.Optimization;
using HBD.Framework;
using HBD.Mef.Mvc.Optimization;

#endregion

namespace HBD.Mef.Mvc
{
    public static class MefScripts
    {
       

        public static IHtmlString RenderAreaBundle()
        {
            var areName = BundleExtensions.GetCurrentAreaName();
            if (areName.IsNullOrEmpty()) return null;

            return Scripts.Render(BundleExtensions.GetBundlesFor<AreaScriptBundle>()
                .Where(a => a.AreaName.EqualsIgnoreCase(areName))
                .Select(a => a.Path)
                .ToArray());
        }
    }
}