namespace HBD.Mef.Modularity
{
    public interface IModuleActivationValidator
    {
        bool CanActivate(IModuleInfo moduleInfo);
    }
}
