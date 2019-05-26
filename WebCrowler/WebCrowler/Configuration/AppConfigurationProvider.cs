namespace WebCrawler.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using NLog;

    public class AppConfigurationProvider
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private static AppConfigurationProvider _instance;

        private readonly IConfiguration _configuration;

        private static object syncRoot = new Object();

        private AppConfigurationProvider()
        {
            _logger.Debug("Initialize AppConfigurationProvider");

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        private static void InitConfiguration()
        {
            _logger.Debug("Start initializing AppConfigurationProvider");

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
            _logger.Debug("Get MaxConnectionsPerServer param");

            return GetIntParam("MaxConnectionsPerServer");
        }

        public static int GetDepth()
        {
            _logger.Debug("Get Depth param");

            return GetIntParam("Depth");
        }

        public static string GetUrl()
        {
            _logger.Debug("Get Url param");

            return GetStringParam("Url");
        }

        public static string GetDestination()
        {
            _logger.Debug("Get Destination param");

            return GetStringParam("Destination");
        }

        public static string GetAllowExternal()
        {
            _logger.Debug("Get AllowExternal param");

            return GetStringParam("AllowExternal");
        }

        public static IEnumerable<string> GetExtentions()
        {
            InitConfiguration();

            _logger.Debug("Get Extentions param");

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
