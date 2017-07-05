#region using

using System;
using HBD.Framework.Attributes;
using HBD.Framework.Core;

#endregion

namespace HBD.Mef.WinForms.Core
{
    public class NavigationResult
    {
        public NavigationResult(NavigationStatus status = NavigationStatus.Sucessful)
        {
            Status = status;
        }

        public NavigationResult([NotNull] Exception exception)
        {
            Guard.ArgumentIsNotNull(exception, nameof(exception));
            Exception = exception;
            Status = NavigationStatus.Error;
        }

        public NavigationStatus Status { get; private set; }
        public Exception Exception { get; private set; }
    }

    public enum NavigationStatus
    {
        Sucessful,
        Error
    }
}