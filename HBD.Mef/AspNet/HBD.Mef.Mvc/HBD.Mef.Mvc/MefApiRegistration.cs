using HBD.Mef.Mvc.Core;

namespace HBD.Mef.Mvc
{
    public abstract class MefApiRegistration : IMefApiRegistration
    {
        public abstract string AreaName { get; }
        //public virtual string PrimaryRouteName => $"{AreaName}_Default";

        //public abstract void Register(HttpConfiguration config);
    }
}