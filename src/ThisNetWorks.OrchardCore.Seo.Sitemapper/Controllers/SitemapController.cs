using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrchardCore.Entities;
using OrchardCore.Settings;
using ThisNetWorks.OrchardCore.Seo.Sitemapper.Models;

namespace ThisNetWorks.OrchardCore.Seo.Sitemapper.Controllers
{
    public class SitemapController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISiteService _siteService;
        private readonly ISitemapManager _sitemapManager;

        public SitemapController(
            IHttpContextAccessor httpContextAccessor,
            ISiteService siteService,
            ILogger<SitemapController> logger,
            ISitemapManager sitemapManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _siteService = siteService;
            Logger = logger;
            _sitemapManager = sitemapManager;
        }

        public ILogger Logger { get; set; }

        public async Task<IActionResult> Index()
        {
            var siteSettings = await _siteService.GetSiteSettingsAsync();

            var sitemap = await _sitemapManager.BuildSitemap();

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "http://www.sitemaps.org/schemas/sitemap/0.9");
            var ser = new XmlSerializer(typeof(SitemapUrlset));
            using (var outStream = new Utf8StringWriter()) {
                ser.Serialize(outStream, sitemap, ns);

                //return Content("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9></urlset>\">", MediaTypeNames.Application.Xml);
                return Content(outStream.ToString(), MediaTypeNames.Application.Xml);
            }
        }

        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }

    }
}

