#region

using HBD.Framework.Core;
using System.Collections.Generic;

#endregion

namespace HBD.Mef.Shell.Navigation
{
    public interface INavigationInfo : IMenuInfo, ITitleObject, IToolTipable
    {
        IList<INavigationParameter> NavigationParameters { get; }
    }
}