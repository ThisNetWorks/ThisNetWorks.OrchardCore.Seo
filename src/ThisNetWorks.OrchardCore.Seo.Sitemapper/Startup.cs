using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using ThisNetWorks.OrchardCore.Seo.Sitemapper.Drivers;
using ThisNetWorks.OrchardCore.Seo.Sitemapper.Handlers;
using ThisNetWorks.OrchardCore.Seo.Sitemapper.Models;
using ThisNetWorks.OrchardCore.Seo.Sitemapper.Settings;

namespace ThisNetWorks.OrchardCore.Seo.Sitemapper
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataMigration, Migrations>();

            services.AddScoped<IContentPartDisplayDriver, SitemapPartDisplay>();
            services.AddSingleton<ContentPart, SitemapPart>();
            services.AddScoped<IContentPartHandler, SitemapPartHandler>();
            services.AddScoped<IContentTypePartDefinitionDisplayDriver, SitemapPartSettingsDisplayDriver>();
        }
    }
}
