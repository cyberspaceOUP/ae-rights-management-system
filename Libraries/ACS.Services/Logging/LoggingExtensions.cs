using System;
//using ACS.Core.Domain.Customers;
using ACS.Core.Domain.Logging;

namespace ACS.Services.Logging
{
    public static class LoggingExtensions
    {
        public static void Debug(this ILogger logger, string message, Exception exception = null)
        {
            FilteredLog(logger, LogLevel.Debug, message, exception);
        }
        public static void Information(this ILogger logger, string message, Exception exception = null)
        {
            FilteredLog(logger, LogLevel.Information, message, exception);
        }
        public static void Warning(this ILogger logger, string message, Exception exception = null)
        {
            FilteredLog(logger, LogLevel.Warning, message, exception);
        }
        public static void Error(this ILogger logger, string message, Exception exception = null)
        {
            FilteredLog(logger, LogLevel.Error, message, exception);
        }

        //public static void Error(this ILogger logger, string message, Exception exception = null)
        //{
        //    FilteredLog(logger, LogLevel.Error, message, exception);
        //}

        public static void Fatal(this ILogger logger, string message, Exception exception = null)
        {
            FilteredLog(logger, LogLevel.Fatal, message, exception);
        }

        private static void FilteredLog(ILogger logger, LogLevel level, string message, Exception exception = null)
        {
            //don't log thread abort exception
            if ((exception != null) && (exception is System.Threading.ThreadAbortException))
                return;

            if (logger.IsEnabled(level))
            {
                string fullMessage = exception == null ? string.Empty : exception.ToString();
                logger.InsertLog(level, message, fullMessage);
            }
        }
    }
}
