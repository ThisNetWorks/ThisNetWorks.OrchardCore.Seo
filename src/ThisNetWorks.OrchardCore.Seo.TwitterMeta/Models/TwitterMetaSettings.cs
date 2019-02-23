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
        public string TwitterCreator { get; set; }

        public string TwitterSite { get; set; }

        public string TwitterUrl { get; set; }

        public string DefaultImageUrl { get; set; }
        
        public string DefaultImageAlt { get; set; }

    }
}
