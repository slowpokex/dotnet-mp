using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace WebCrawler.Helpers
{
    public class Utils
    {
        public static IEnumerable<string> GetResourcesFromDocument(HtmlDocument pageDocument, string url, List<string> resourcesExtentions)
        {
            if (pageDocument == null || string.IsNullOrEmpty(url))
            {
                return null;
            }

            var images = pageDocument.DocumentNode.Descendants("img")
                            .Select(a => GetResourceUrl(a, "src", url));

            var scripts = pageDocument.DocumentNode.Descendants("script")
                            .Select(a => GetResourceUrl(a, "src", url));

            var links = pageDocument.DocumentNode.Descendants("link")
                            .Select(a => GetResourceUrl(a, "href", url));

            return images
                .Concat(scripts)
                .Concat(links)
                .Where(u => IsValidURL(u) && IsMatchByExt(resourcesExtentions, u))
                .Distinct();
        }

        public static IEnumerable<string> GetLinksFromUrl(HtmlDocument pageDocument, string url)
        {
            if (pageDocument == null || string.IsNullOrEmpty(url))
            {
                return null;
            }

            return pageDocument.DocumentNode.Descendants("a")
                            .Select(a => GetUrl(a, "href", url))
                            .Where(u => IsValidURL(u))
                            .Distinct();
        }

        public static string GetUrl(HtmlNode node, string attr, string url)
        {
            var link = node.GetAttributeValue(attr, null);

            var baseUrl = new Uri(url);
            var fullUrl = new Uri(baseUrl, link);

            return fullUrl.AbsoluteUri;
        }

        public static string GetResourceUrl(HtmlNode node, string attr, string url)
        {
            var link = node.GetAttributeValue(attr, null);

            var baseUrl = new Uri(url);
            var fullUrl = new Uri(baseUrl, link);

            return fullUrl.GetLeftPart(UriPartial.Path);
        }

        public static string GetDestinationResourcePath(string url, string dest)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(dest))
            {
                return "";
            }

            var baseUrl = new Uri(url);

            return dest + baseUrl.AbsolutePath;
        }

        public static bool IsRelativeURL(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Relative);
        }

        public static bool IsValidURL(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        public static bool IsMatchByExt(List<string> patterns, string url)
        {
            if (patterns == null || patterns.Count == 0)
            {
                return true;
            }

            return patterns.Any(pattern => Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase));
        }

        public static bool IsExternalResource(string baseUrl, string url)
        {
            return Uri.Compare(new Uri(baseUrl), new Uri(url), UriComponents.Host, UriFormat.SafeUnescaped, StringComparison.CurrentCulture) != 0;
        }

    }
}
