namespace WebCrawler.Models
{
    using System.Collections.Generic;
    using HtmlAgilityPack;

    public class HtmlPage
    {
        public HtmlDocument Page { get; set; }

        public int Depth { get; set; }

        public List<HtmlDocument> ChildrenPages { get; set; }
    }
}
