using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Core
{
    public class FileLoggerOptions
    {
        public FileLoggerOptions(HostBuilderContext context)
        {
            InitSettings(context);
        }

        public virtual string FileName { get; private set; }
        public virtual string FolderPath { get; private set; }
        public virtual string LogFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", FolderPath, FileName);
            }
        }

        private string _configLogDirectory;
        private string _configLogFile;

        private string _defaultLogDirectory;
        private string _defaultLogFile;

        private void InitSettings(HostBuilderContext context)
        {
            _defaultLogDirectory = ".\\Logs";
            _defaultLogFile = "application.log";
            if (context.HostingEnvironment.IsDevelopment())

                try
                {
                    _configLogDirectory = context.Configuration.GetSection("Logging").GetSection("FileLogger").GetSection("Options").GetValue<string>("FolderPath");
                    _configLogFile = context.Configuration.GetSection("Logging").GetSection("FileLogger").GetSection("Options").GetValue<string>("FilePath");
                }
                catch (Exception e)
                {
                    _configLogDirectory = string.Empty;
                    _configLogFile = string.Empty;
                }

            if (!string.IsNullOrEmpty(_configLogDirectory) && !string.IsNullOrEmpty(_configLogFile))
            {
                FolderPath = _configLogDirectory;
                FileName = _configLogFile;
            }
            else
            {
                FolderPath = _defaultLogDirectory;
                FileName = _defaultLogFile;
            }
            if (context.HostingEnvironment.IsDevelopment())
            {
                FolderPath = string.Format(".\\bin{0}", _defaultLogDirectory);
                FileName = _defaultLogFile;
            }
        }
    }
}
