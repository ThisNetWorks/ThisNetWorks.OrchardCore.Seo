using Microsoft.AspNetCore.Mvc.ModelBinding;
using ThisNetWorks.OrchardCore.Seo.TwitterMeta.Models;

namespace ThisNetWorks.OrchardCore.Seo.TwitterMeta.ViewModels
{
    public class TwitterMetaPartViewModel
    {
        public bool UseDefaultTwitterCardType { get; set; }

        public TwitterCardType TwitterCardType { get; set; } //og:type

        public bool UseDefaultCreator { get; set; }

        public string Creator { get; set; }

        //max 70
        public string Title { get; set; } //fallsback to og:title

        //max length 200
        public string Description { get; set; } // fallsback to og:description

        public string ImageUrl { get; set; } //fallsback to og:image

        public string ImageAlt { get; set; }

        [BindNever]
        public TwitterMetaPart TwitterMetaPart { get; set; }
        

    }
}
