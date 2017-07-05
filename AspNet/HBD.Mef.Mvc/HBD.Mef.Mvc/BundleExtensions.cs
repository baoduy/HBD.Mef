#region using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Optimization;
using HBD.Framework;
using HBD.Framework.Attributes;
using HBD.Framework.Core;
using HBD.Mef.Mvc.Optimization;

#endregion

namespace HBD.Mef.Mvc
{
    public static class BundleExtensions
    {
        private const string AreaVirtualPathFormat = "~/Areas/{0}/{1}";
        private const string BinConfigFileVirtualPath = "~/bin/{0}.config";
        internal static VirtualPathProvider VirtualPathProvider => HostingEnvironment.VirtualPathProvider;

        internal static string GetCurrentAreaName()
        {
            return HttpContext.Current?.Request.RequestContext.RouteData
                       .DataTokens["area"] as string ?? string.Empty;
        }

        internal static IEnumerable<T> GetBundlesFor<T>() where T : Bundle
        {
            return BundleTable.Bundles.GetRegisteredBundles().OfType<T>();
        }

        internal static string GetBundleName([NotNull] string areaName)
        {
            return string.Format(AreaVirtualPathFormat, areaName, Guid.NewGuid());
        }

        /// <summary>
        ///     Get virtual path of Area config file: ~/Areas/AreaName/Web.config
        /// </summary>
        /// <param name="areaName"></param>
        /// <returns></returns>
        internal static string GetAreaConfigFile([NotNull] string areaName)
        {
            return string.Format(AreaVirtualPathFormat, areaName, "Web.config");
        }

        /// <summary>
        ///     Get virtual path of Module config file: ~/bin/ModuleName.config
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        internal static string GetBinConfigFile([NotNull] string moduleName)
        {
            return string.Format(BinConfigFileVirtualPath, moduleName);
        }

        /// <summary>
        ///     Get the Area virtual path of the file
        ///     Ex: path = ~/Content/area.css then the returns will be ~/Areas/[AreaName]/Content/area.css.
        /// </summary>
        /// <param name="areaName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static string NornalizeAreaVirtualPath([NotNull] string areaName, string path)
        {
            if (path.StartsWith("~/Areas", StringComparison.OrdinalIgnoreCase) &&
                path.ContainsIgnoreCase(areaName)) return path;
            return path.StartsWith("http", StringComparison.OrdinalIgnoreCase)
                ? path
                : string.Format(AreaVirtualPathFormat, areaName, path.Replace("~/", string.Empty));
        }

        public static AreaScriptBundle AddAreaScriptBundle(this BundleCollection @this, [NotNull] string areaName)
        {
            Guard.ArgumentIsNotNull(areaName, nameof(areaName));
            var b = @this.OfType<AreaScriptBundle>().FirstOrDefault(i => i.AreaName.EqualsIgnoreCase(areaName))
                    ?? new AreaScriptBundle(areaName);
            @this.Add(b);
            return b;
        }

        public static AreaStyleBundle AddAreaStyleBundle(this BundleCollection @this, [NotNull] string areaName)
        {
            Guard.ArgumentIsNotNull(areaName, nameof(areaName));
            var b = @this.OfType<AreaStyleBundle>().FirstOrDefault(i => i.AreaName.EqualsIgnoreCase(areaName))
                    ?? new AreaStyleBundle(areaName);
            @this.Add(b);
            return b;
        }
    }
}