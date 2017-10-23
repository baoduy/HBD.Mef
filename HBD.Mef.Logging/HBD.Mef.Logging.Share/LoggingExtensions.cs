#region using

using System;
using System.Text;

#endregion

namespace HBD.Mef.Logging
{
    public static class LoggingExtensions
    {
        internal static string CreateExceptionString(this Exception @this, string indent = null)
        {
            if (@this == null) return string.Empty;
            if (string.IsNullOrWhiteSpace(indent))
                indent = string.Empty;

            var builder = new StringBuilder();
            while (true)
            {
                if (!string.IsNullOrEmpty(indent))
                    builder.Append("====Inner Exception====").Append(Environment.NewLine);

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

        public static void Log(this ILogger @this, Exception exception)
        {
            @this?.Log(exception.CreateExceptionString(), LogCategory.Exception);
        }

        /// <summary>
        ///     NOTE: Debug message won't be written into the file when running on Release mode.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="message"></param>
        public static void Debug(this ILogger @this, string message)
        {
            @this?.Log(message, LogCategory.Debug);
        }

        public static void Info(this ILogger @this, string message)
        {
            @this?.Log(message, LogCategory.Info);
        }

        public static void Warn(this ILogger @this, string message)
        {
            @this?.Log(message, LogCategory.Warn);
        }

        public static void Exception(this ILogger @this, string message)
        {
            @this?.Log(message, LogCategory.Exception);
        }

        public static void Exception(this ILogger @this, Exception exception)
        {
            @this?.Log(exception);
        }
    }
}