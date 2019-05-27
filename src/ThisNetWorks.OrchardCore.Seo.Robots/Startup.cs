using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.Navigation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using ThisNetWorks.OrchardCore.Seo.Robots.Drivers;

namespace ThisNetWorks.OrchardCore.Seo.Robots
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<IDisplayDriver<ISite>, RobotsSettingsDisplayDriver>();
            services.AddScoped<IPermissionProvider, Permissions>();
        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaRoute(
                   name: "Robots.txt",
                   areaName: "ThisNetWorks.OrchardCore.Seo.Robots",
                   template: "robots.txt",
                   defaults: new { controller = "Robots", action = "Index" }
               );
        }
    }
}
