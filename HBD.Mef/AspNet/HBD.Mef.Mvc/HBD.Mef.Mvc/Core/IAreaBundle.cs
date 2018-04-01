namespace HBD.Mef.Mvc.Core
{
    public interface IAreaBundle
    {
        string Path { get; }
        string AreaName { get; }
        string BundleName { get; }
    }
}