using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.Media.Fields;
using ThisNetWorks.OrchardCore.Seo.TwitterMeta.Models;

namespace ThisNetWorks.OrchardCore.Seo.TwitterMeta.ViewModels
{
    public class TwitterMetaSettingsViewModel
    {
        public string TwitterCreator { get; set; }

        public string TwitterSite { get; set; }

        public string DefaultImageUrl { get; set; }

        public string DefaultImageAlt { get; set; }

    }
}
