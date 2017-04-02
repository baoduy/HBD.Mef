#region using

using System;
using System.IO;

#endregion

namespace HBD.Mef.Logging
{
    public abstract class LoggerBase : ILogger
    {
        public readonly string DefaultOutFileName;

        protected LoggerBase()
        {
            DefaultOutFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Logs\\Log_{GetType().Name}.log");
        }

        public abstract void Log(string message, LogCategory category);
    }
}