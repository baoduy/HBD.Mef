#region

using HBD.Framework.Core;
using System;

#endregion

namespace HBD.Mef.Shell.Navigation
{
    /// <summary>
    ///     The ActionNavigationParameter to execute an Action
    /// </summary>
    public class ActionNavigationParameter : INavigationParameter
    {
        public ActionNavigationParameter(Action action)
        {
            Guard.ArgumentIsNotNull(action, nameof(action));
            Action = action;
        }

        public Action Action { get; }
    }
}