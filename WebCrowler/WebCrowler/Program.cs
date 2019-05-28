namespace WebCrawler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using HtmlAgilityPack;
    using NLog;
    using WebCrawler.Configuration;
    using WebCrawler.Helpers;

    public class Program
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            Start(args)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            Console.ReadLine();
        }

        private async static Task Start(string[] args)
        {
            var destinationPath = AppConfigurationProvider.GetDestination();

            _logger.Info("Start application");

            var httpClient = new HttpClient();
            var linkList = new List<string>();
            var levels = AppConfigurationProvider.GetDepth();
            var resourcesList = new List<string>();

            var resourcesExtentions = AppConfigurationProvider
                .GetExtentions()
                .ToList();

            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = AppConfigurationProvider.GetMaxConnectionsPerServer()
            };

            _logger.Info("Start ejecting links");

            await LoadDocument(
                httpClient,
                new List<string>() {
                    AppConfigurationProvider.GetUrl()
                },
                linkList,
                resourcesList,
                resourcesExtentions,
                levels,
                levels
            );

            _logger.Info("Finish ejecting links");

            // Download all resources from site
            _logger.Info("Start downloading resources");

            if (!Directory.Exists(destinationPath))
            {
                _logger.Warn($"Directory {destinationPath} isn't exists! The directory will be created!");
                try
                {
                    Directory.CreateDirectory(destinationPath);
                }
                catch (Exception e)
                {
                    _logger.Warn($"Error when creating destination path: {destinationPath}: {e.Message}!");
                    return;
                }
            }

            var resourcesTasks = resourcesList
                .Select(res => DownloadFile(res, Utils.GetDestinationResourcePath(res, destinationPath)))
                .ToArray();

            Task.WaitAll(resourcesTasks);

            _logger.Info("Finish downloading resources");
            _logger.Info("Finish application");
        }

        private async static Task LoadDocument(
            HttpClient client,
            List<string> urls,
            List<string> links,
            List<string> resourcesList,
            List<string> resourcesExtentions,
            int fullDepth,
            int depth)
        {
            if (urls == null || urls.Count == 0 || depth < 0)
            {
                _logger.Debug($"Return from LoadDocument method");
                return;
            }

            _logger.Debug($"Level {fullDepth - depth}");

            var nextLinks = new List<string>();

            foreach (var url in urls)
            {
                try
                {
                    _logger.Info($"Handling url: {url}");

                    _logger.Debug($"Load HTML from {url}");
                    var response = await client.GetAsync(url);
                    var pageContents = await response.Content.ReadAsStringAsync();
                    var pageDocument = new HtmlDocument();

                    pageDocument.LoadHtml(pageContents);

                    _logger.Debug($"Add links for {url}");
                    nextLinks.AddRange(Utils.GetLinksFromUrl(pageDocument, url));

                    _logger.Debug($"Add resources for {url}");
                    resourcesList.AddRange(Utils.GetResourcesFromDocument(pageDocument, url, resourcesExtentions));
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message);
                }
            }

            _logger.Debug($"Go to next level from {depth} to {depth - 1}");

            await LoadDocument(
                client, nextLinks,
                links,
                resourcesList,
                resourcesExtentions,
                fullDepth,
                depth - 1);
        }

        private static async Task DownloadFile(string url, string dest)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(dest))
            {
                _logger.Warn($"Url or Destination apth is empty! Skipping! Url: {url}, Destination: {dest}");

                return;
            }

            using (var client = new WebClient())
            {
                try
                {
                    var directory =  Path.GetDirectoryName(dest);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    await client.DownloadFileTaskAsync(url, dest);
                    _logger.Debug($"Resource {url} has been downloaded to {dest} path");
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message);
                }
            }
        }

    }
}
