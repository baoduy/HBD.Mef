#region using

using System.Collections.Generic;
using HBD.Framework;

#endregion

namespace HBD.Mef.WinForms.Core
{
    public class WinformNavigationContext
    {
        public WinformNavigationContext(IDictionary<string, object> parameters = null)
        {
            if (parameters != null)
                NavigationParameters.AddRange(parameters);
        }

        public IDictionary<string, object> NavigationParameters { get; } = new Dictionary<string, object>();
    }
}