#region

using System;
using System.Text;
using Prism.Logging;

#endregion

namespace HBD.Mef.Core.Logging
{
    public static class LoggingExtensions
    {
        private static string CreateExceptionString(this Exception @this, string indent = null)
        {
            if (@this == null) return string.Empty;
            if (string.IsNullOrWhiteSpace(indent))
                indent = string.Empty;

            var builder = new StringBuilder();
            while (true)
            {
                if (!string.IsNullOrEmpty(indent))
                    builder.Append($"====Inner Exception====").Append(Environment.NewLine);

                builder.Append($"Error: {@this.Message}{Environment.NewLine}");
                builder.Append($"{indent}Source: {@this.Source}{Environment.NewLine}");
                builder.Append($"{indent}Stack trace: {@this.StackTrace}");

                if (@this.InnerException != null)
                {
                    builder.Append(Environment.NewLine);
                    @this = @this.InnerException;
                    indent = indent + "   ";
                    continue;
                }
                break;
            }

            return builder.ToString();
        }

        public static void Log(this ILoggerFacade @this, Exception exception)
            => @this?.Log(exception.CreateExceptionString(), Category.Exception, Priority.High);

        /// <summary>
        ///     NOTE: Debug message won't be written into the file when running on Release mode.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="message"></param>
        public static void Debug(this ILoggerFacade @this, string message)
            => @this?.Log(message, Category.Debug, Priority.Low);

        public static void Info(this ILoggerFacade @this, string message)
            => @this?.Log(message, Category.Info, Priority.Low);

        public static void Warn(this ILoggerFacade @this, string message)
            => @this?.Log(message, Category.Warn, Priority.Low);

        public static void Exception(this ILoggerFacade @this, string message)
            => @this?.Log(message, Category.Exception, Priority.Low);

        public static void Exception(this ILoggerFacade @this, Exception exception)
            => @this?.Log(exception);
    }
}