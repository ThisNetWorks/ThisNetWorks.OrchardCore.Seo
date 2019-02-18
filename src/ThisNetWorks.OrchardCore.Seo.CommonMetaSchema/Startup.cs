using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Title;
using OrchardCore.Modules;
using ThisNetWorks.OrchardCore.Seo.Meta.Drivers;
using ThisNetWorks.OrchardCore.Seo.Meta.Models;

namespace ThisNetWorks.OrchardCore.Seo.Meta
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataMigration, Migrations>();

            services.AddScoped<IContentPartDisplayDriver, SeoMetaPartDisplay>();
            services.AddSingleton<ContentPart, SeoMetaPart>();

            services.RemoveAll<IPageTitleBuilder>();

            services.AddScoped<IPageTitleBuilder, SeoPageTitleBuilder>();
        }
        
    }
}
