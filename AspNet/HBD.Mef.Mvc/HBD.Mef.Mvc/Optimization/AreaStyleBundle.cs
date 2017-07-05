#region using

using System.Linq;
using System.Web.Optimization;
using HBD.Framework.Attributes;

#endregion

namespace HBD.Mef.Mvc.Optimization
{
    public sealed class AreaStyleBundle : StyleBundle
    {
        internal AreaStyleBundle([NotNull] string areaName) : this(areaName, null)
        {
        }

        internal AreaStyleBundle([NotNull] string areaName, string cdnPath) : base(
            BundleExtensions.GetBundleName(areaName), cdnPath)
        {
            AreaName = areaName;
        }

        public string AreaName { get; }

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