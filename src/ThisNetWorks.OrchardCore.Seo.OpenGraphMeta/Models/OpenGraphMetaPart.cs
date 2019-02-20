using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.ContentManagement;

namespace ThisNetWorks.OrchardCore.Seo.OpenGraphMeta.Models
{
    public class OpenGraphMetaPart : ContentPart
    {
        public string Title { get; set; }

        public OpenGraphType Type { get; set; }

        public string ImageUrl { get; set; }

        public string ImageSecureUrl { get; set; }

        //mimetype
        public string ImageType { get; set; }

        public string ImageHeight { get; set; }

        public string ImageWidth { get; set; }

        public string ImageAlt { get; set; }

        public string Url { get; set; }


        public string Description { get; set; }

        public Determiner Determiner { get; set; }

        //get from site settings?
        public string Locale { get; set; }

        public string[] LocaleAlternates { get; set; }

        public string SiteName { get; set; }

        public string VideoUrl { get; set; }

        public string VideoSecureUrl { get; set; }

        //mimetype
        public string VideoType { get; set; }

        public string VideoHeight { get; set; }

        public string VideoWidth { get; set; }

        public string VideoAlt { get; set; }


        //optional
        public string AudioUrl { get; set; }
        public string AudioSecureUrl { get; set; }
        public string AudioType { get; set; }

        //just do as widgets, and then can be added site by site / custom by custom
        public IList<ContentItem> OpenGraphWidgets { get; set; }




    }
}
