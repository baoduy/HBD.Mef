using System.Collections.Generic;

namespace HBD.Mef.Shell.Navigation
{
    public interface INavigationParametersCollection
    {
        IList<INavigationParameter> NavigationParameters { get; }
    }
}
