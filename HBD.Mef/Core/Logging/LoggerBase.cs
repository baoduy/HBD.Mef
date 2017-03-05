#region

using System;
using System.IO;
using Prism.Logging;

#endregion

namespace HBD.Mef.Core.Logging
{
    public abstract class LoggerBase : ILoggerFacade
    {
        public readonly string DefaultOutFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs\\Log.log");

        public abstract void Log(string message, Category category, Priority priority);
    }
}