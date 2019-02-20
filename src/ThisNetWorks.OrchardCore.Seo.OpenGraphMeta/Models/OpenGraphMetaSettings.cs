using System;
using System.Collections.Generic;
using System.Text;

namespace ThisNetWorks.OrchardCore.Seo.OpenGraphMeta.Models
{
    public class OpenGraphMetaSettings
    {
        public OpenGraphType DefaultOpenGraphType { get; set; }

        public string DefaultOpenGraphImage { get; set; }

        public string FacebookUrl { get; set; }

        public string FacebookLocale { get; set; }

        public string FacebookWebsiteName { get; set; }
    }
}
