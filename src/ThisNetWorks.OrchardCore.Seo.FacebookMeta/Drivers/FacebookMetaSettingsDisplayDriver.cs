using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using ThisNetWorks.OrchardCore.Seo.FacebookMeta.Models;
using ThisNetWorks.OrchardCore.Seo.FacebookMeta.ViewModels;

namespace ThisNetWorks.OrchardCore.Seo.FacebookMeta.Drivers
{
    public class FacebookMetaSettingsDisplayDriver : SectionDisplayDriver<ISite, FacebookMetaSettings>
    {
        public const string GroupId = "robots";

        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FacebookMetaSettingsDisplayDriver(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<IDisplayResult> EditAsync(FacebookMetaSettings settings, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageRobots))
            {
                return null;
            }

            return Initialize<FacebookMetaSettingsViewModel>("RobotsSettings_Edit", model =>
            {
                model.MatchBaseUrlOrServeDisallow = settings.MatchBaseUrlOrServeDisallow;
                model.RobotsContent = settings.RobotsContent;
            }).Location("Content:5").OnGroup(GroupId);
        }

        public override async Task<IDisplayResult> UpdateAsync(FacebookMetaSettings settings, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageRobots))
            {
                return null;
            }

            if (context.GroupId == GroupId)
            {
                var model = new FacebookMetaSettingsViewModel();

                await context.Updater.TryUpdateModelAsync(model, Prefix);

                settings.MatchBaseUrlOrServeDisallow = model.MatchBaseUrlOrServeDisallow;
                settings.RobotsContent = model.RobotsContent;
            }

            return await EditAsync(settings, context);
        }
    }
}
