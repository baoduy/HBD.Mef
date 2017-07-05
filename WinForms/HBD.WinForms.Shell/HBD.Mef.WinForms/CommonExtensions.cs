namespace HBD.Mef.WinForms
{
    public static class CommonExtensions
    {
        public static T CastAs<T>(this object @this)
        {
            if (@this == null) return default(T);
            if (@this is T) return (T) @this;
            return default(T);
        }
    }
}