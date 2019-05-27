using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThisNetWorks.OrchardCore.Seo.Sitemapper.Models;

namespace ThisNetWorks.OrchardCore.Seo.Sitemapper
{
    public interface ISitemapProvider
    {
        Task<IList<SitemapUrlItem>> BuildSitemapItems();
    }
}
