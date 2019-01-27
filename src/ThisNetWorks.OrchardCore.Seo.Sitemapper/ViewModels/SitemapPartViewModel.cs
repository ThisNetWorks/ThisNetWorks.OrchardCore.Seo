using Microsoft.AspNetCore.Mvc.ModelBinding;
using ThisNetWorks.OrchardCore.Seo.Sitemapper.Models;

namespace ThisNetWorks.OrchardCore.Seo.Sitemapper.ViewModels
{
    public class SitemapPartViewModel
    {
        public ChangeFrequency ChangeFrequency { get; set; }

        public float Priority { get; set; } = 0.5f;

        public bool Exclude { get; set; }

        [BindNever]
        public SitemapPart SitemapPart { get; set; }
        
    }
}
