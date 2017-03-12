namespace HBD.Mef
{
    public static class Extensions
    {
        internal static T CastAs<T>(this object @this) where T : class => @this as T;
    }
}
