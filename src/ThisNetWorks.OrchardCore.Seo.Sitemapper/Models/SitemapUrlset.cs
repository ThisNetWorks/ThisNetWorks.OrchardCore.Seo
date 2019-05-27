using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ThisNetWorks.OrchardCore.Seo.Sitemapper.Models
{
    [XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class SitemapUrlset
    {
        [XmlElement("url")]
        public List<SitemapUrlItem> Items { get; set; } = new List<SitemapUrlItem>();
    }
}
