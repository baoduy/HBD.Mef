#region using

using System;
using System.Diagnostics;
using System.IO;

#endregion

namespace HBD.Mef.Logging
{
    public class Trace2FileLogger : LoggerBase
    {
        private TraceListener _traceWriter;
        private TextWriter _writer;

        public Trace2FileLogger(string outputFile = null, bool allowDebugLog = true) : base(allowDebugLog)
        {
            if (string.IsNullOrWhiteSpace(outputFile))
                outputFile = DefaultOutFileName;

            // ReSharper disable once AssignNullToNotNullAttribute
            Directory.CreateDirectory(Path.GetDirectoryName(outputFile));

            // ReSharper disable once AssignNullToNotNullAttribute
            _writer = new StreamWriter(File.Create(outputFile));

            _traceWriter = new TextWriterTraceListener(_writer, GetType().FullName)
            {
                TraceOutputOptions = TraceOptions.DateTime
            };
            Trace.Listeners.Add(_traceWriter);
            Trace.AutoFlush = true;
            Trace.Indent();
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            Log($"Disposing {GetType().FullName}", LogCategory.Debug);
            Trace.Listeners.Remove(_traceWriter);
            Trace.Unindent();

            _writer.Dispose();
            _writer = null;

            _traceWriter.Dispose();
            _traceWriter = null;

            Trace.Listeners.Clear();
        }

        public override void Log(string message, LogCategory category)
        {
            switch (category)
            {
                case LogCategory.Debug:
#if DEBUG
                    Trace.TraceInformation(message);
#else
                    if(AllowDebugLog)
                        Trace.TraceInformation(message);
#endif
                    break;

                case LogCategory.Exception:
                    Trace.TraceError(message);
                    break;

                case LogCategory.Info:
                    Trace.TraceInformation(message);
                    break;

                case LogCategory.Warn:
                    Trace.TraceWarning(message);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(category), category, null);
            }

            _traceWriter.Flush();
        }
    }
}