namespace WebCrawler
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using HtmlAgilityPack;
    using WebCrawler.Configuration;
    using WebCrawler.Models;

    public class Program
    {
        public static void Main(string[] args)
        {
            var handler = new HttpClientHandler
            {
                MaxConnectionsPerServer = AppConfigurationProvider.GetMaxConnectionsPerServer()
            };

            var client = new HttpClient(handler);

            Start(
                AppConfigurationProvider.GetUrl(),
                AppConfigurationProvider.GetDepth()
            );

            Console.ReadLine();
        }

        private static void Start(string url, int depth)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            var levels = Enumerable.Range(0, depth + 1);

            foreach (var level in levels)
            {
                Console.WriteLine(level);
            }
        }

        private static HtmlPage GetPageFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            var page = new HtmlPage();

            return page;
        }

        private static HtmlDocument GetDocumentFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            var page = new HtmlWeb();

            return page.Load(url);
        }

        private static async Task SendRequest(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            Console.WriteLine($"Received response {response.StatusCode} from {url}");
        }
    }
}
