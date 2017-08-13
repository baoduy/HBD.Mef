#region using

using System;
using System.IO;

#endregion

namespace HBD.Mef.Logging
{
    public abstract class LoggerBase : ILogger
    {
#if !NETSTANDARD2_0 || !NETSTANDARD1_6
        public const string AppSettingKey="HBD.Mef.Logging.AllowDebugLog";
#endif

        public readonly string DefaultOutFileName;
        public bool AllowDebugLog { get; }

        protected LoggerBase(bool allowDebugLog)
        {
#if NETSTANDARD2_0 || NETSTANDARD1_6
            DefaultOutFileName = Path.Combine(AppContext.BaseDirectory, $"Logs\\Log_{GetType().Name}.log");
#else
            DefaultOutFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Logs\\Log_{GetType().Name}.log");

            var appSetting = System.Configuration.ConfigurationManager.AppSettings[AppSettingKey];
            if (string.Compare(appSetting, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
                allowDebugLog = true;
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