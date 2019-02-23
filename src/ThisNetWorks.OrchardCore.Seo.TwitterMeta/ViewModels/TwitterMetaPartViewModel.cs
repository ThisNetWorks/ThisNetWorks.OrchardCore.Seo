using Microsoft.AspNetCore.Mvc.ModelBinding;
using ThisNetWorks.OrchardCore.Seo.TwitterMeta.Models;

namespace ThisNetWorks.OrchardCore.Seo.TwitterMeta.ViewModels
{
    public class TwitterMetaPartViewModel
    {
        //max 70
        public string Title { get; set; } //fallsback to og:title

        //max length 200
        public string Description { get; set; } // fallsback to og:description
        
        public string ImageAlt { get; set; }

        [BindNever]
        public TwitterMetaPart TwitterMetaPart { get; set; }
        

    }
}
