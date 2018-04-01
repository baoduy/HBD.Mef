#region

using System.Linq;
using System.Web.Optimization;
using HBD.Framework.Attributes;
using HBD.Mef.Mvc.Core;

#endregion

namespace HBD.Mef.Mvc.Optimization
{
    public sealed class AreaStyleBundle : StyleBundle, IAreaBundle
    {
        internal AreaStyleBundle([NotNull] string areaName, string bundleName = null) : this(areaName, bundleName, null)
        {
        }

        internal AreaStyleBundle([NotNull] string areaName, string bundleName, string cdnPath) : base(
            BundleExtensions.GetBundleName(areaName, bundleName), cdnPath)
        {
            AreaName = areaName;
            BundleName = bundleName;
        }

        public string AreaName { get; }
        public string BundleName { get; }

        public override Bundle Include(params string[] virtualPaths)
        {
            var paths = virtualPaths.Select(p => BundleExtensions.NornalizeAreaVirtualPath(AreaName, p)).ToArray();
            return base.Include(paths);
        }

        public override Bundle Include(string virtualPath, params IItemTransform[] transforms)
        {
            virtualPath = BundleExtensions.NornalizeAreaVirtualPath(AreaName, virtualPath);
            return base.Include(virtualPath, transforms);
        }
    }
}