using Microsoft.AspNetCore.Mvc.ModelBinding;
using ThisNetWorks.OrchardCore.Seo.Meta.Models;

namespace ThisNetWorks.OrchardCore.Seo.TwitterMeta.ViewModels
{
    public class TwitterMetaPartViewModel
    {
        public string PageTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        [BindNever]
        public SeoMetaPart SeoMetaPart { get; set; }
        

    }
}
