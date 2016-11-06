﻿using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Prism.Logging;

namespace HBD.Mef.Core.Logging
{
    public class TextFileLogger : ILoggerFacade
    {
        public const string DefaultOutFileName = "Logs\\Log.log";

        private ILog _log;

        public TextFileLogger(string outputFile = null)
        {
            Initialize(outputFile);
        }

        public virtual void Log(string message, Category category, Priority priority)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            switch (category)
            {
                case Category.Debug:
                    _log.Debug(message);
                    break;
                case Category.Exception:
                    _log.Error(message);
                    break;
                case Category.Info:
                    _log.Info(message);
                    break;
                case Category.Warn:
                    _log.Warn(message);
                    break;
                default:
                    _log.Info(message);
                    break;
            }
        }

        /// <summary>
        ///     Initial with default output file.
        /// </summary>
        public void Initialize(string outputFile = null)
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
    }
}