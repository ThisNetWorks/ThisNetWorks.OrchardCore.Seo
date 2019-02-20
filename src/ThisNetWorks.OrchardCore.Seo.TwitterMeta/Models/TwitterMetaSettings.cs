using System;
using System.Collections.Generic;
using System.Text;

namespace ThisNetWorks.OrchardCore.Seo.TwitterMeta.Models
{
    public class TwitterMetaSettings
    {
        public TwitterCardType DefaultTwitterCardType { get; set; } //fallsback to og:type

        public string DefaultTwitterCreator { get; set; }

        public string TwitterSite { get; set; }

        public string TwitterUrl { get; set; }
    }
}
