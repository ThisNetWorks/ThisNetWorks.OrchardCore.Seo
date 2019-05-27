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
using OrchardCore.ContentManagement.Metadata;
using System.Linq;

namespace ThisNetWorks.OrchardCore.Seo.Sitemapper.Providers
{
    public class AutorouteSitemapProvider : ISitemapProvider
    {
        private readonly YesSql.ISession _session;
        private readonly IUrlHelper _urlHelper;
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public AutorouteSitemapProvider(
            ILogger<AutorouteSitemapProvider> logger,
            YesSql.ISession session,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelperFactory urlHelperFactory,
            IContentDefinitionManager contentDefinitionManager)
        {
            Logger = logger;
            _session = session;
            _urlHelper = urlHelperFactory.GetUrlHelper(new ActionContext(httpContextAccessor.HttpContext, new RouteData(), new ActionDescriptor()));
            _contentDefinitionManager = contentDefinitionManager;
        }

        public ILogger<AutorouteSitemapProvider> Logger { get; }

        public async Task<IList<SitemapUrlItem>> BuildSitemapItems()
        {
            var sitemapUrlItems = new List<SitemapUrlItem>();

            var items = await _session
                .Query<ContentItem, AutoroutePartIndex>(x => x.Published)
                .ListAsync();

            foreach(var item in items)
            {
                var autoroutePart = item.As<AutoroutePart>();
                var sitemapPart = item.As<SitemapPart>();
                
                var exclude = sitemapPart != null ? sitemapPart.Exclude : false;
                if (!exclude)
                {
                    var sitemapPartSettings = GetSettings(sitemapPart);
                    exclude = sitemapPartSettings != null ? sitemapPartSettings.ExcludePart : false;
                }
                if (exclude)
                    continue;

                var changeFrequency = sitemapPart != null ? sitemapPart.ChangeFrequency : ChangeFrequency.Daily;
                var priority = sitemapPart != null ? sitemapPart.Priority : 0.5f;

                string path = autoroutePart.Path;
                if (!String.IsNullOrEmpty(path))
                {
                    path = path.StartsWith("/") ? path : path.Insert(0, "/");
                    path = path.StartsWith("~/") ? path.Replace("~", String.Empty) : path;
                }

                var urlItem = new SitemapUrlItem()
                {
                    Location = _urlHelper.ToAbsoluteUrl(path),
                    ChangeFrequency = changeFrequency.ToString().ToLower(),
                    Priority = priority,
                    LastModified = item.ModifiedUtc.GetValueOrDefault().ToString("yyyy-MM-ddTHH:mm:sszzz")
            };
                sitemapUrlItems.Add(urlItem);
            }
            return sitemapUrlItems;
        }

        private SitemapPartSettings GetSettings(SitemapPart sitemapPart)
        {
            if (sitemapPart == null)
                return null;

            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(sitemapPart.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(x => String.Equals(x.PartDefinition.Name, nameof(SitemapPart), StringComparison.Ordinal));
            return contentTypePartDefinition.Settings.ToObject<SitemapPartSettings>();
        }
    }
}
