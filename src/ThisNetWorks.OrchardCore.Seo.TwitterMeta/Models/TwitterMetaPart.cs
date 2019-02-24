using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using OrchardCore.ContentManagement;
using OrchardCore.Media.Fields;

namespace ThisNetWorks.OrchardCore.Seo.TwitterMeta.Models
{
    //https://developer.twitter.com/en/docs/tweets/optimize-with-cards/overview/markup
    public class TwitterMetaPart : ContentPart
    {
        [StringLength(70)]
        public string Title { get; set; } //fallsback to og:title
        
        public string Description { get; set; } // fallsback to og:description
        
        public string ImageAlt { get; set; }

        public string GoogleSchema { get; set; }

        //TODO
        ////player
        //public string PlayerUrl { get; set; }

        //public int PlayerWidth { get; set; }

        //public int PlayerHeight { get; set; }

        //public string PlayerStreamUrl { get; set; }

        //// app

        //public string AppNameIphone { get; set; }

        //public string AppIdIphone { get; set; }

        //public string AppUrlIphone { get; set; }

        //public string AppNameIpad { get; set; }
        //public string AppIdIpad { get; set; }

        //public string AppUrlIpad { get; set; }

        //public string AppNameGooglePlay { get; set; }

        //public string AppIdGooglePlay { get; set; }

        //public string AppUrlGooglePlay { get; set; }
    }
}
