#region using

using System;
using System.IO;

#endregion

namespace HBD.Mef.Logging
{
    public abstract class LoggerBase : ILogger
    {
        public const string AllowDebugLogKey = "HBD.Logging.AllowDebugLog";

        public readonly string DefaultOutFileName;
        public bool AllowDebugLog { get; }

        protected LoggerBase(bool allowDebugLog)
        {
#if NETSTANDARD2_0 || NETSTANDARD1_6
            DefaultOutFileName = Path.Combine(AppContext.BaseDirectory, $"Logs\\Log_{GetType().Name}.log");
#else
            DefaultOutFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Logs\\Log_{GetType().Name}.log");
            if(!allowDebugLog)
                allowDebugLog = HBD.Framework.Configuration.ConfigurationManager.GetAppSettingValue<bool>(AllowDebugLogKey);
#endif
            AllowDebugLog = allowDebugLog;
        }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool isDisposing)
        {
        }

        public abstract void Log(string message, LogCategory category);
    }
}