#region using

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

        public Log4NetLogger(string outputFile = null)
        {
            if (string.IsNullOrWhiteSpace(outputFile))
                outputFile = DefaultOutFileName;

            var hierarchy = (Hierarchy) LogManager.GetRepository();

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

            _log = LogManager.GetLogger(GetType());
        }

        public override void Log(string message, LogCategory category)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            switch (category)
            {
                case LogCategory.Debug:
#if DEBUG
                    _log.Debug(message);
#endif
                    break;

                case LogCategory.Exception:
                    _log.Error(message);
                    break;

                case LogCategory.Info:
                    _log.Info(message);
                    break;

                case LogCategory.Warn:
                    _log.Warn(message);
                    break;

                default:
                    _log.Info(message);
                    break;
            }
        }
    }
}