namespace WebCrawler.Configuration
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public class AppConfigurationProvider
    {
        private static AppConfigurationProvider _instance;

        private readonly IConfiguration _configuration;

        private static object syncRoot = new Object();

        private AppConfigurationProvider()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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

        public static int GetMaxConnectionsPerServer()
        {
            return GetIntParam("MaxConnectionsPerServer");
        }

        public static int GetDepth()
        {
            return GetIntParam("Depth");
        }

        public static string GetUrl()
        {
            return GetStringParam("Url");
        }

        private static int GetIntParam(string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                return 0;
            }

            InitConfiguration();

            var value = _instance._configuration[param];

            return int.Parse(value);
        }

        private static string GetStringParam(string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                return "";
            }

            InitConfiguration();

            var value = _instance._configuration[param];

            return value ?? "";
        }
    }
}
