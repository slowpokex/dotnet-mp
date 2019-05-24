using System;
using HtmlAgilityPack;

namespace WebCrawler.Helpers
{
    public class Utils
    {
        public static string GetUrl(HtmlNode node, string attr, string url)
        {
            var link = node.GetAttributeValue(attr, null);

            var baseUrl = new Uri(url);
            var fullUrl = new Uri(baseUrl, link);

            return fullUrl.AbsoluteUri;
            ;
        }

        public static bool IsRelativeURL(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Relative);
        }

        public static bool IsValidURL(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
