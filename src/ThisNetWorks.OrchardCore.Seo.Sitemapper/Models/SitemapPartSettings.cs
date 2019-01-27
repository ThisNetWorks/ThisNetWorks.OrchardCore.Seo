using System;
using System.Collections.Generic;
using System.Text;

namespace ThisNetWorks.OrchardCore.Seo.Sitemapper.Models
{
    public class SitemapPartSettings
    {
        /// <summary>
        /// Always exclude all content items of this content type 
        /// </summary>
        public bool ExcludePart { get; set; }

        /// <summary>
        /// Exclude content item by default, but allow overriding at content item level
        /// </summary>
        public bool ExcludeByDefault { get; set; }
    }
}
