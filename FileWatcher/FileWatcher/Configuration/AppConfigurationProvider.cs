namespace FileWatcher.Configuration
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public class AppConfigurationProvider
    {
        private static AppConfigurationProvider _instance;

        public IConfiguration Configuration { get; private set; }
        private static object syncRoot = new Object();

        private AppConfigurationProvider()
        {
            this.Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddCommandLine(Environment.GetCommandLineArgs())
                    .Build();
        }

        public static AppConfigurationProvider getInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                        _instance = new AppConfigurationProvider();
                }
            }
            return _instance;
        }
    }
}
