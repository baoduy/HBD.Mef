#region using

using System.Linq;
using System.Web;
using System.Web.Optimization;
using HBD.Framework;
using HBD.Mef.Mvc.Optimization;

#endregion

namespace HBD.Mef.Mvc
{
    public static class MefStyles
    {
        public static IHtmlString RenderForArea()
        {
            var areName = BundleExtensions.GetCurrentAreaName();
            if (areName.IsNullOrEmpty()) return null;

            return Styles.Render(BundleExtensions.GetBundlesFor<AreaStyleBundle>()
                .Where(a => a.AreaName.EqualsIgnoreCase(areName))
                .Select(a => a.Path)
                .ToArray());
        }
    }
}