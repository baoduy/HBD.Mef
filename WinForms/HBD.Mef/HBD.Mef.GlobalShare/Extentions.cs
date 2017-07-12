using HBD.Framework.Attributes;
using HBD.Framework.Core;
using HBD.Mef.Attributes;
using HBD.Mef.Modularity;
using System.Linq;
using System.Reflection;

namespace HBD.Mef
{
    public static class Extentions
    {
        /// <summary>
        /// Get module Info from ModuleInfoAttribute of ModuleType if available.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IModuleInfo GetModuleInfo([NotNull]this PluginInfo @this)
        {
            Guard.ArgumentIsNotNull(@this, typeof(PluginInfo).FullName);
            return @this.ModuleType.GetTypeInfo().GetCustomAttributes<ModuleInfoAttribute>().FirstOrDefault();
        }
    }
}
