using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.ContentManagement;

namespace ThisNetWorks.OrchardCore.Seo.TwitterMeta.Models
{
    public class TwitterMetaPart : ContentPart
    {
        public string PageTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }
    }
}
