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

        public override async Task<IDisplayResult> EditAsync(ISite model, TwitterMetaSettings settings, BuildEditorContext context)
        {


            var vm = Initialize<TwitterMetaSettingsViewModel>("TwitterMetaSettings_Edit", m =>
            {
                m.DefaultTwitterCardType = settings.DefaultTwitterCardType;
                m.TwitterSite = settings.TwitterSite;
                m.TwitterUrl = settings.TwitterUrl;
                m.DefaultTwitterImageField = settings.DefaultTwitterImageField;
                m.DefaultTwitterImageFieldDefinition = settings.DefaultTwitterImageFieldDefinition;
                m.DefaultTwitterCardType = settings.DefaultTwitterCardType;
            }).Location("Content:5").OnGroup(GroupId);

            var _fieldDefinition = new ContentFieldDefinition("MediaField");

            var t = new ContentPartFieldDefinition(_fieldDefinition, "Test", new JObject());
            //var media = Dynamic("MediaField_Edit", shape =>
            //{
            //    shape.Paths = JsonConvert.SerializeObject(new string[] { });
            //    shape.PartFieldDefinition = t;
            //    shape.Field = null;
            //    shape.Part = null;
            //}).Location("Content:5").OnGroup(GroupId);

            var m2 = Initialize<EditMediaFieldViewModel>("MediaField_Edit", m =>
            {
                m.Paths = JsonConvert.SerializeObject(new string[] { });

                m.Field = null;
                m.Part = null;
                m.PartFieldDefinition = t;
            });

            //var contentTypeDefinitions = _contentDefinitionManager.GetTypeDefinition("Doc");
            //var typeDef = contentTypeDefinitions.Parts.FirstOrDefault(x => x.Name == "TwitterMetaPart");

            //var _fieldDefinition = new ContentFieldDefinition("MediaField");

            //var t = new ContentPartFieldDefinition(_fieldDefinition, "Test", new JObject());
            //IDisplayResult result = null;
            ////foreach (var fiel in _fieldDisplayDrivers)
            ////{
            ////    result = await fiel.BuildEditorAsync(part, t, typeDef, context);
            ////    if (result != null)
            ////    {
            ////        break;
            ////    }
            ////}
            //await _fieldDisplayDrivers.InvokeAsync(async contentDisplay =>
            //{
            //    result = await contentDisplay.BuildEditorAsync(part, t, typeDef, context);
            //    if (result != null)
            //    {
            //        await result.ApplyAsync(context);
            //    }
            //}, null);

            return m2;
      //      return Combine( m2);
        }
    

        public override async Task<IDisplayResult> UpdateAsync(TwitterMetaSettings settings, BuildEditorContext context)
        {
            if (context.GroupId == GroupId)
            {
                var model = new TwitterMetaSettingsViewModel();

                await context.Updater.TryUpdateModelAsync(model, Prefix);

                settings.DefaultTwitterCardType = model.DefaultTwitterCardType;
                settings.TwitterSite = model.TwitterSite;
                settings.TwitterUrl = model.TwitterUrl;
                settings.DefaultTwitterImageField = model.DefaultTwitterImageField;
                settings.DefaultTwitterImageFieldDefinition = model.DefaultTwitterImageFieldDefinition;
                settings.DefaultTwitterCardType = model.DefaultTwitterCardType;
            }

            return await EditAsync(settings, context);
        }
    }
}
