using Microsoft.AspNetCore.Mvc.ModelBinding;
using ThisNetWorks.OrchardCore.Seo.FacebookMeta.Models;

namespace ThisNetWorks.OrchardCore.Seo.FacebookMeta.ViewModels
{
    public class FacebookMetaPartViewModel
    {
        public string PageTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        [BindNever]
        public FacebookMetaPart SeoMetaPart { get; set; }
        

    }
}
