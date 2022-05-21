
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Core
{
    public class FileLoggerProvider : ILoggerProvider
    {
        public readonly FileLoggerOptions options;

        
        public FileLoggerProvider(HostBuilderContext context)
        {
            options = new FileLoggerOptions(context);

            if (!Directory.Exists(options.FolderPath))
            {
                Directory.CreateDirectory(options.FolderPath);
            }
        }


        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(this);
        }

        public void Dispose()
        {           
        }
    }
}
