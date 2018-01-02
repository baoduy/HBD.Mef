#region using

using System.Linq;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

#endregion

namespace HBD.Mef.Logging
{
    public class Log4NetLogger : LoggerBase
    {
        private readonly ILog _log;

        public Log4NetLogger(string outputFile = null, bool allowDebugLog = false) : base(allowDebugLog)
        {
            if (string.IsNullOrWhiteSpace(outputFile))
                outputFile = DefaultOutFileName;

            var type = GetType();
            Hierarchy hierarchy = null;

            try
            {
                hierarchy = LogManager.GetRepository(type.FullName) as Hierarchy;
            }
            catch
            {
                hierarchy = (Hierarchy) LogManager.CreateRepository(type.FullName);
            }

            var roller = new RollingFileAppender
            {
                AppendToFile = true,
                File = outputFile,
                Layout = new PatternLayout("%-5level %newlineTime: %date %newlineMessage: %message %newline %newline"),
                MaxSizeRollBackups = 5,
                MaximumFileSize = "5MB",
                RollingStyle = RollingFileAppender.RollingMode.Composite,
                StaticLogFileName = true
            };

            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);
            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;

            _log = LogManager.GetLogger(type.FullName, type.Name);
        }

        public override void Log(string message, LogCategory category)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            switch (category)
            {
                case LogCategory.Debug:
#if DEBUG
                    _log.Debug(message);
#else
                    if(AllowDebugLog)
                     _log.Debug(message);
#endif
                    break;

                case LogCategory.Exception:
                    _log.Error(message);
                    break;

                case LogCategory.Warn:
                    _log.Warn(message);
                    break;

                case LogCategory.Info:
                default:
                    _log.Info(message);
                    break;
            }
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            var files = LoggerManager.GetAllRepositories().OfType<Hierarchy>();

            foreach (var f in files)
            {
                f.Flush(100);
                f.Shutdown();
            }

            LogManager.Shutdown();
        }
    }
}