using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ThisNetWorks.OrchardCore.Seo.Sitemapper.Models
{
    public class SitemapUrlItem
    {
        [XmlElement("loc")]
        public string Location { get; set; }

        [XmlElement("lastmod")]
        public string LastModified { get; set; }

        [XmlElement("changefreq")]
        public string ChangeFrequency { get; set; }

        [XmlElement("priority")]
        public float Priority { get; set; }
    }
}
