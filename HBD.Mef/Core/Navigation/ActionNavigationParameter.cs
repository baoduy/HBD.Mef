using System;
using HBD.Framework.Core;

namespace HBD.Mef.Core.Navigation
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