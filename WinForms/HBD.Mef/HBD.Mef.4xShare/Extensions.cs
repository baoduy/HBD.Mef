using HBD.Framework.Attributes;
using HBD.Framework.Core;
using HBD.Mef.Attributes;
using HBD.Mef.Modularity;
using System.Collections.Generic;
using System.Reflection;

namespace HBD.Mef
{
    public static class Extensions
    {
        internal static T CastAs<T>(this object @this) where T : class => @this as T;

        public static IEnumerable<IModuleInfo> GetModuleInfos([NotNull]this IPluginManager manager)
        {
            Guard.ArgumentIsNotNull(manager, nameof(manager));
            foreach (var p in manager.Plugins)
            {
                var att = p.ModuleType.GetCustomAttribute<ModuleInfoAttribute>();
                yield return att ?? new ModuleInfoAttribute(p.ModuleType.FullName, p.ModuleType.Name);
            }
        }
    }
}
