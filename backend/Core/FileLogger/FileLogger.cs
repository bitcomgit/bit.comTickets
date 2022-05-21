using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Core
{
    public class FileLogger : ILogger
    {
        public FileLogger([NotNull] FileLoggerProvider _fileLoggerProvider)
        {
            this.fileLoggerProvider = _fileLoggerProvider;
        }


        protected readonly FileLoggerProvider fileLoggerProvider;

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var fullFilePath = fileLoggerProvider.options.LogFilePath;
            var logRecord = string.Format("{0} {1} {2}",  DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") ,  formatter(state, exception), exception != null ? exception.StackTrace : "");

            using (var streamWriter = new StreamWriter(fullFilePath, true))
            {
                streamWriter.WriteLine(logRecord);
            }
        }
    }
}
