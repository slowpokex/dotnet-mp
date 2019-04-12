namespace FileWatcher.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using FileWatcher.Models;
    using Microsoft.Extensions.Configuration;

    public class AppConfigurationProvider
    {
        private static AppConfigurationProvider _instance;

        private readonly  IConfiguration _configuration;

        private static object syncRoot = new Object();

        private AppConfigurationProvider()
        {
            _configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddCommandLine(Environment.GetCommandLineArgs())
                    .Build();
        }

        private static void InitConfiguration()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new AppConfigurationProvider();
                    }                       
                }
            }            
        }

        public static string GetCurrentLocale()
        {
            InitConfiguration();

            var value = _instance._configuration["Locale"];

            return string.IsNullOrEmpty(value) ? value : "";
        }

        public static IEnumerable<string> GetSourcePaths()
        {
            InitConfiguration();

            return _instance._configuration
                .GetSection("SourcePaths")
                .GetChildren()
                .Select(x => x.Value);
        }

        public static string GetDefaultPath()
        {
            return _instance._configuration["DefaultPath"];
        }

        public static IEnumerable<Pattern> GetPatterns()
        {
            InitConfiguration();

            return _instance._configuration
                .GetSection("Patterns")
                .GetChildren()
                .Select(x => new Pattern {
                    Wildcard = x.GetValue<string>("Wildcard"),
                    Destination = x.GetValue<string>("Destination"),
                    AddDate = x.GetValue<bool>("AddDate"),
                    AddNumber = x.GetValue<bool>("AddNumber")
                });
        }
    }
}
