using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using ThisNetWorks.OrchardCore.Seo.TwitterMeta.Models;
using ThisNetWorks.OrchardCore.Seo.TwitterMeta.ViewModels;
using OrchardCore.Modules;
using Newtonsoft.Json;
using OrchardCore.Media.ViewModels;

namespace ThisNetWorks.OrchardCore.Seo.TwitterMeta.Drivers
{
    public class TwitterMetaSettingsDisplayDriver : SectionDisplayDriver<ISite, TwitterMetaSettings>
    {
        public const string GroupId = "twittermeta";
        private readonly IEnumerable<IContentFieldDisplayDriver> _fieldDisplayDrivers;
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public TwitterMetaSettingsDisplayDriver(
            IEnumerable<IContentFieldDisplayDriver> fieldDisplayDrivers,
            IContentDefinitionManager contentDefinitionManager) {
            _fieldDisplayDrivers = fieldDisplayDrivers;
            _contentDefinitionManager = contentDefinitionManager;
        }

        public override IDisplayResult Edit(TwitterMetaSettings settings)
        {
            return Initialize<TwitterMetaSettingsViewModel>("TwitterMetaSettings_Edit", m =>
            {
                m.TwitterSite = settings.TwitterSite;
                m.TwitterUrl = settings.TwitterUrl;
                m.TwitterCreator = settings.TwitterCreator;
                m.DefaultImageUrl = settings.DefaultImageUrl;
                m.DefaultImageAlt = settings.DefaultImageAlt;
            }).Location("Content:5").OnGroup(GroupId);
        }

        public override async Task<IDisplayResult> UpdateAsync(TwitterMetaSettings settings, BuildEditorContext context)
        {
            if (context.GroupId == GroupId)
            {
                var model = new TwitterMetaSettingsViewModel();

                await context.Updater.TryUpdateModelAsync(model, Prefix);
                
                settings.TwitterSite = model.TwitterSite;
                settings.TwitterUrl = model.TwitterUrl;
                settings.DefaultImageUrl = model.DefaultImageUrl;
                settings.DefaultImageAlt = model.DefaultImageAlt;
                settings.TwitterCreator = model.TwitterCreator;
            }

            return await EditAsync(settings, context);
        }
    }
}
