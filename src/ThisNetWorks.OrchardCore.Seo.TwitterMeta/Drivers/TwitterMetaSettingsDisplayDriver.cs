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
using ThisNetWorks.OrchardCore.Seo.TwitterMeta.Models;
using ThisNetWorks.OrchardCore.Seo.TwitterMeta.ViewModels;

namespace ThisNetWorks.OrchardCore.Seo.TwitterMeta.Drivers
{
    public class TwitterMetaSettingsDisplayDriver : SectionDisplayDriver<ISite, TwitterMetaSettings>
    {
        public const string GroupId = "robots";

        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TwitterMetaSettingsDisplayDriver(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<IDisplayResult> EditAsync(TwitterMetaSettings settings, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageRobots))
            {
                return null;
            }

            return Initialize<TwitterMetaSettingsViewModel>("RobotsSettings_Edit", model =>
            {
                model.MatchBaseUrlOrServeDisallow = settings.MatchBaseUrlOrServeDisallow;
                model.RobotsContent = settings.RobotsContent;
            }).Location("Content:5").OnGroup(GroupId);
        }

        public override async Task<IDisplayResult> UpdateAsync(TwitterMetaSettings settings, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageRobots))
            {
                return null;
            }

            if (context.GroupId == GroupId)
            {
                var model = new TwitterMetaSettingsViewModel();

                await context.Updater.TryUpdateModelAsync(model, Prefix);

                settings.MatchBaseUrlOrServeDisallow = model.MatchBaseUrlOrServeDisallow;
                settings.RobotsContent = model.RobotsContent;
            }

            return await EditAsync(settings, context);
        }
    }
}
