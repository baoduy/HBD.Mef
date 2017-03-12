#region using

using System;
using System.IO;
using Prism.Logging;

#endregion

namespace HBD.Mef.Logging
{
    public abstract class LoggerBase : ILoggerFacade
    {
        public readonly string DefaultOutFileName;

        protected LoggerBase()
        {
            DefaultOutFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Logs\\Log_{this.GetType().Name}.log");
        }

        public abstract void Log(string message, Category category, Priority priority);
    }
}