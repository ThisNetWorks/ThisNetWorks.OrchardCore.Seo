using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrchardCore.Entities;
using OrchardCore.Settings;
using ThisNetWorks.OrchardCore.Seo.Robots.Models;

namespace ThisNetWorks.OrchardCore.Seo.Robots.Controllers
{
    public class RobotsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISiteService _siteService;

        public RobotsController(
            IHttpContextAccessor httpContextAccessor,
            ISiteService siteService,
            ILogger<RobotsController> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _siteService = siteService;
            Logger = logger;
        }

        public ILogger Logger { get; set; }

        public async Task<IActionResult> Index()
        {
            var siteSettings = await _siteService.GetSiteSettingsAsync();
            var robotsSettings = siteSettings.As<RobotsSettings>();

            if (robotsSettings == null || String.IsNullOrEmpty(robotsSettings.RobotsContent))
            {
                Logger.LogWarning("RobotsSettings or RobotsContent is empty, serving disallow");
                return DisallowAllPages();
            }

            if (robotsSettings.MatchBaseUrlOrServeDisallow && !_httpContextAccessor.HttpContext.Request.Query.ContainsKey("test"))
            {
                if (String.IsNullOrEmpty(siteSettings.BaseUrl))
                {
                    Logger.LogWarning("Base Url setting is empty, serving disallow");
                    return DisallowAllPages();
                }
                var sanitizedBaseUrl = siteSettings.BaseUrl.Replace("http://", String.Empty);
                sanitizedBaseUrl = sanitizedBaseUrl.Replace("https://", String.Empty);
                sanitizedBaseUrl = sanitizedBaseUrl.TrimEnd('/');
                var requestUrl = _httpContextAccessor.HttpContext.Request.Host.ToString();
                if (requestUrl != sanitizedBaseUrl)
                {
                    Logger.LogWarning("Robots request is not on main site Base Url, serving disallow");
                    return DisallowAllPages();
                }
            }

            return Content(robotsSettings.RobotsContent, MediaTypeNames.Text.Plain);
        }

        private IActionResult DisallowAllPages()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("user-agent: *");
            stringBuilder.AppendLine("disallow: *");
            return Content(stringBuilder.ToString(), MediaTypeNames.Text.Plain);
        }
    }
}

