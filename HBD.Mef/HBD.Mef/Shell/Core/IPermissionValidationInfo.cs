namespace HBD.Mef.Shell.Core
{
    public interface IPermissionValidationInfo
    {
    }

    public interface IOperationValidationInfo : IPermissionValidationInfo
    {
        string[] Operations { get; set; }
    }

    public interface IRoleValidationInfo : IOperationValidationInfo
    {
        string[] Roles { get; set; }
    }
}