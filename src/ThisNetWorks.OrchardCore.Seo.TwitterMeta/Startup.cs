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
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Title;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Settings;
using ThisNetWorks.OrchardCore.Seo.TwitterMeta.Drivers;
using ThisNetWorks.OrchardCore.Seo.TwitterMeta.Models;

namespace ThisNetWorks.OrchardCore.Seo.TwitterMeta
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataMigration, Migrations>();

            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<IContentPartDisplayDriver, TwitterMetaPartDisplay>();
            services.AddSingleton<ContentPart, TwitterMetaPart>();
            services.AddScoped<IDisplayDriver<ISite>, TwitterMetaSettingsDisplayDriver>();
        }
        
    }
}
