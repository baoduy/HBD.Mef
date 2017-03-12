#region using

using System;
using System.Diagnostics;
using System.IO;
using HBD.Framework;
using Prism.Logging;

#endregion

namespace HBD.Mef.Logging
{
    public class Trace2FileLogger : LoggerBase, IDisposable
    {
        private TraceListener _traceWriter;
        private StreamWriter _writer;

        public Trace2FileLogger(string outputFile = null)
        {
            if (outputFile.IsNullOrEmpty())
                outputFile = DefaultOutFileName;

            // ReSharper disable once AssignNullToNotNullAttribute
            Directory.CreateDirectory(Path.GetDirectoryName(outputFile));

            // ReSharper disable once AssignNullToNotNullAttribute
            _writer = new StreamWriter(outputFile);

            _traceWriter = new TextWriterTraceListener(_writer, GetType().FullName)
            {
                TraceOutputOptions = TraceOptions.DateTime
            };
            Trace.Listeners.Add(_traceWriter);
            Trace.AutoFlush = true;
            Trace.Indent();
        }

        public void Dispose()
        {
            Log($"Disposing {GetType().FullName}", Category.Debug, Priority.Medium);
            Trace.Listeners.Remove(_traceWriter);
            Trace.Unindent();

            _writer.Close();
            _writer.Dispose();
            _writer = null;

            _traceWriter.Close();
            _traceWriter.Dispose();
            _traceWriter = null;

            Trace.Listeners.Clear();
        }

        public override void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
#if DEBUG
                    Trace.TraceInformation(message);
#endif
                    break;

                case Category.Exception:
                    Trace.TraceError(message);
                    break;

                case Category.Info:
                    Trace.TraceInformation(message);
                    break;

                case Category.Warn:
                    Trace.TraceWarning(message);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(category), category, null);
            }

            _traceWriter.Flush();
        }
    }
}