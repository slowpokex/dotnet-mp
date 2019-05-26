namespace WebCrawler.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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

        public static string GetDestination()
        {
            return GetStringParam("Destination");
        }

        public static string GetAllowExternal()
        {
            return GetStringParam("AllowExternal");
        }

        public static IEnumerable<string> GetExtentions()
        {
            InitConfiguration();

            return _instance._configuration
                .GetSection("Extentions")
                .GetChildren()
                .Select(x => x.Value);
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

        private static bool GetBoolParam(string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                return false;
            }

            InitConfiguration();

            var value = _instance._configuration[param];

            return bool.Parse(value);
        }
    }
}
