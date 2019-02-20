using Microsoft.AspNetCore.Mvc.ModelBinding;
using ThisNetWorks.OrchardCore.Seo.OpenGraphMeta.Models;

namespace ThisNetWorks.OrchardCore.Seo.OpenGraphMeta.ViewModels
{
    public class OpenGraphMetaPartViewModel
    {
        public string PageTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        [BindNever]
        public OpenGraphMetaPart SeoMetaPart { get; set; }
        

    }
}
