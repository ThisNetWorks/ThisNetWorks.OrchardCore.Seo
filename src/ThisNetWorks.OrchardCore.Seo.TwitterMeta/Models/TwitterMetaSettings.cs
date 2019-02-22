using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.Media.Fields;

namespace ThisNetWorks.OrchardCore.Seo.TwitterMeta.Models
{
    public class TwitterMetaSettings
    {
        public TwitterCardType DefaultTwitterCardType { get; set; } //fallsback to og:type

        public string DefaultTwitterCreator { get; set; }

        public string TwitterSite { get; set; }

        public string TwitterUrl { get; set; }

        public MediaField DefaultTwitterImageField { get; set; }

        public ContentPartFieldDefinition DefaultTwitterImageFieldDefinition { get; set; }
    }
}
