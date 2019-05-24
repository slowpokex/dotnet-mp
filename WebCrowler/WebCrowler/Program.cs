namespace WebCrawler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using HtmlAgilityPack;
    using WebCrawler.Configuration;
    using WebCrawler.Helpers;

    public class Program
    {
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
            var httpClient = new HttpClient();
            var linkList = new List<string>();
            var levels = AppConfigurationProvider.GetDepth();
            var resourcesList = new List<string>();

            await LoadDocument(
                httpClient,
                new List<string>() {
                    AppConfigurationProvider.GetUrl()
                },
                linkList,
                resourcesList,
                levels,
                levels
            );

            Console.WriteLine(resourcesList);
        }

        private async static Task LoadDocument(HttpClient client, List<string> urls, List<string> links, List<string> resourcesList, int fullDepth, int depth)
        {
            if (urls == null || urls.Count == 0 || depth < 0)
            {
                return;
            }

            Console.WriteLine($"Level {fullDepth - depth}");

            var nextLinks = new List<string>();

            foreach (var url in urls)
            {
                Console.WriteLine($"Url {url}");

                var response = await client.GetAsync(url);
                var pageContents = await response.Content.ReadAsStringAsync();
                var pageDocument = new HtmlDocument();

                pageDocument.LoadHtml(pageContents);

                var images = pageDocument.DocumentNode.Descendants("img")
                                .Select(a => Utils.GetUrl(a, "src", url))
                                .Where(u => Utils.IsValidURL(u))
                                .Distinct();

                var scripts = pageDocument.DocumentNode.Descendants("script")
                                .Select(a => Utils.GetUrl(a, "src", url))
                                .Where(u => Utils.IsValidURL(u))
                                .Distinct();

                var styles = pageDocument.DocumentNode.Descendants("link")
                                .Select(a => Utils.GetUrl(a, "href", url))
                                .Where(u => Utils.IsValidURL(u))
                                .Distinct();

                var hrefLinks = pageDocument.DocumentNode.Descendants("a")
                                                  .Select(a => Utils.GetUrl(a, "href", url))
                                                  .Where(u => Utils.IsValidURL(u))
                                                  .Distinct();

                nextLinks.AddRange(hrefLinks);

                var resources = images
                    .Concat(scripts);
                    // .Concat(styles);

                resourcesList.AddRange(resources);
            }

            await LoadDocument(client, nextLinks, links, resourcesList, fullDepth, depth - 1);
        }
    }
}
