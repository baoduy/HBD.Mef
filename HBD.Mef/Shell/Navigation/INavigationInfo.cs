#region using

using System.Collections.Generic;
using HBD.Framework.Core;

#endregion

namespace HBD.Mef.Shell.Navigation
{
    public interface INavigationInfo : IMenuInfo, ITitleObject, IToolTipable
    {
        IList<INavigationParameter> NavigationParameters { get; }
    }
}