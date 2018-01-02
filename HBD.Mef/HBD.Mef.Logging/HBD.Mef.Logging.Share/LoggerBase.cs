#region using

using System;
using System.IO;

#endregion

namespace HBD.Mef.Logging
{
    public abstract class LoggerBase : ILogger
    {
        public readonly string DefaultOutFileName;

        protected LoggerBase(bool allowDebugLog)
        {
#if NETSTANDARD2_0 || NETSTANDARD1_6
            DefaultOutFileName = Path.Combine(AppContext.BaseDirectory, $"Logs\\Log_{GetType().Name}.log");
#else
            DefaultOutFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Logs\\Log_{GetType().Name}.log");
#endif
            AllowDebugLog = allowDebugLog;
        }

        public bool AllowDebugLog { get; }

        public void Dispose()
        {
            Dispose(true);
        }

        public abstract void Log(string message, LogCategory category);

        protected virtual void Dispose(bool isDisposing)
        {
        }
    }
}