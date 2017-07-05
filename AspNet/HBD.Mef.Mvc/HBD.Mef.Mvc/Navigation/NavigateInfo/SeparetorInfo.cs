using System;

namespace HBD.Mef.Mvc.Navigation.NavigateInfo
{
    public class SeparetorInfo : INavigationInfo
    {
        public IMenuInfo Clone()
        {
            return new SeparetorInfo();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}