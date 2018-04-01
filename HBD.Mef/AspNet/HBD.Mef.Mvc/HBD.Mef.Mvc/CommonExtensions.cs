#region

using System.Web.Http.Routing;

#endregion

namespace HBD.Mef.Mvc
{
    public static class CommonExtensions
    {
        public static T GetRouteVariable<T>(this IHttpRouteData @this, string name)
        {
            if (@this.Values.TryGetValue(name, out object result))
                return (T) result;
            return default(T);
        }
    }
}