using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ThisNetWorks.OrchardCore.Seo.Sitemapper.Models;

namespace ThisNetWorks.OrchardCore.Seo.Sitemapper
{
    public class DefaultSitemapManager : ISitemapManager
    {
        private IEnumerable<ISitemapProvider> _sitemapProviders;
        public DefaultSitemapManager(
            ILogger<DefaultSitemapManager> logger,
            IEnumerable<ISitemapProvider> sitemapProviders)
        {
            Logger = logger;
            _sitemapProviders = sitemapProviders;
        }

        public ILogger<DefaultSitemapManager> Logger { get; }

        public async Task<SitemapUrlset> BuildSitemap()
        {
            var sitemapUrlset = new SitemapUrlset();

            foreach(var sitemapProvider in _sitemapProviders)
            {
                sitemapUrlset.Items.AddRange(await sitemapProvider.BuildSitemapItems());
            }
            return sitemapUrlset;
        }
    }
}
