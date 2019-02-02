using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using OrchardCore.Autoroute.Model;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using ThisNetWorks.OrchardCore.Seo.Sitemapper.Models;
using YesSql;
using OrchardCore.Mvc.Core.Utilities;

namespace ThisNetWorks.OrchardCore.Seo.Sitemapper.Providers
{
    public class AutorouteSitemapProvider : ISitemapProvider
    {
        private readonly YesSql.ISession _session;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;

        public AutorouteSitemapProvider(
            ILogger<AutorouteSitemapProvider> logger
            , YesSql.ISession session
            , IHttpContextAccessor httpContextAccessor
            , IUrlHelperFactory urlHelperFactory)
        {
            Logger = logger;
            _session = session;
            //_httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelperFactory.GetUrlHelper(new ActionContext(httpContextAccessor.HttpContext, new RouteData(), new ActionDescriptor()));


        }

        public ILogger<AutorouteSitemapProvider> Logger { get; }

        public async Task<IList<SitemapUrlItem>> BuildSitemapItems()
        {
            var sitemapUrlItems = new List<SitemapUrlItem>();

            var query = _session.Query<ContentItem>()
                .With<AutoroutePartIndex>(x => x.Published)
                .With<ContentItemIndex>(x => x.Published)
                .OrderByDescending(x => x.ModifiedUtc);
                //TODO - need to understand if this is an exclusive with (ie only get if has sitemap index, or an outer join, i.e. null if no SitemapPart)
                //.With<SitemapPartIndex>
                

            var items = await query.ListAsync();
            foreach(var item in items)
            {
                var autoroutePart = item.As<AutoroutePart>();
                var sitemapPart = item.As<SitemapPart>();

                //TODO get content definition settings (cache?) for general exclusion settings
                var exclude = sitemapPart != null ? (sitemapPart.Exclude) : false;

                if (exclude)
                    continue;

                var changeFrequency = sitemapPart != null ? sitemapPart.ChangeFrequency : ChangeFrequency.Daily;
                var priority = sitemapPart != null ? sitemapPart.Priority : 0.5f;

                //urlhelper for this
                var urlItem = new SitemapUrlItem()
                {
                    Location = _urlHelper.ToAbsoluteUrl(autoroutePart.Path),
                    ChangeFrequency = changeFrequency.ToString().ToLower(),
                    Priority = priority,
                    LastModified = item.ModifiedUtc.GetValueOrDefault().ToString("yyyy-MM-ddTHH:mm:sszzz")
            };
                sitemapUrlItems.Add(urlItem);
            }
            return sitemapUrlItems;
        }
    }
}
