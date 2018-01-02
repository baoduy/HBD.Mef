#region using

using HBD.Framework.Core;

#endregion

namespace HBD.Mef.Shell.Navigation
{
    public interface INavigationInfo : INavigationParametersCollection, IMenuInfo, ITitleObject, IToolTipable
    {
    }
}