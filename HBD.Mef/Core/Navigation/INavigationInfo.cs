using System.Collections.Generic;
using HBD.Framework.Core;

namespace HBD.Mef.Core.Navigation
{
    public interface INavigationInfo : IMenuInfo, ITitleObject, IToolTipable
    {
        IList<INavigationParameter> NavigationParameters { get; }
    }
}