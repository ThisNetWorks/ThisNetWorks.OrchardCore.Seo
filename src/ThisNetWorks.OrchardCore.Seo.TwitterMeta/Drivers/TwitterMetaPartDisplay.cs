using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluid;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Liquid;
using ThisNetWorks.OrchardCore.Seo.TwitterMeta.Models;
using ThisNetWorks.OrchardCore.Seo.TwitterMeta.ViewModels;
using OrchardCore.Modules;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Models;
using Newtonsoft.Json.Linq;

namespace ThisNetWorks.OrchardCore.Seo.TwitterMeta.Drivers
{
    public class TwitterMetaPartDisplay : ContentPartDisplayDriver<TwitterMetaPart>
    {
        private readonly IContentManager _contentManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILiquidTemplateManager _liquidTemplatemanager;

        private readonly IEnumerable<IContentFieldDisplayDriver> _fieldDisplayDrivers;
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public TwitterMetaPartDisplay(
            IContentManager contentManager,
            IServiceProvider serviceProvider,
            ILiquidTemplateManager liquidTemplateManager,
            IEnumerable<IContentFieldDisplayDriver> fieldDisplayDrivers,
            IContentDefinitionManager contentDefinitionManager
            )
        {
            _contentManager = contentManager;
            _serviceProvider = serviceProvider;
            _liquidTemplatemanager = liquidTemplateManager;
            _fieldDisplayDrivers = fieldDisplayDrivers;
            _contentDefinitionManager = contentDefinitionManager;
        }

        public override IDisplayResult Display(TwitterMetaPart part)
        {
            return Initialize<TwitterMetaPartViewModel>("TwitterMetaPart", m => BuildDisplayViewModelAsync(m, part))
                    .Location("Detail", "Content:6");
        }

        //public override IDisplayResult Edit(TwitterMetaPart part, BuildPartEditorContext context)
        //{
        //    return Initialize<TwitterMetaPartViewModel>("TwitterMetaPart_Edit", m => BuildEditViewModel(m, part));
        //}


        public override async Task<IDisplayResult> EditAsync(TwitterMetaPart part, BuildPartEditorContext context)
        {
            var mainResult = Initialize<TwitterMetaPartViewModel>("TwitterMetaPart_Edit", m => BuildEditViewModel(m, part));

            //var contentTypeDefinition = _contentDefinitionManager.GetPartDefinition("TwitterMetaPart");
            var contentTypeDefinitions = _contentDefinitionManager.GetTypeDefinition(part.ContentItem.ContentType);
            var typeDef = contentTypeDefinitions.Parts.FirstOrDefault(x => x.Name == "TwitterMetaPart");
            //if (contentTypeDefinition == null)
            //    return;

            //            _contentDefinitionManager.AlterPartDefinition("TwitterMetaPart", builder => builder
            //    .Attachable()
            //    .WithDescription("Provides a part that allows twitter card on a content item.")
            //    .WithField("test", x => x.OfType(");


            //var existingField = new ContentPartFieldDefinition(null, "test", new JObject());

            //        var configurer = new FieldConfigurerImpl(existingField, _part);
            //        configuration(configurer);
            //        _fields.Add(configurer.Build());
            var _fieldDefinition = new ContentFieldDefinition("MediaField");

            var t = new ContentPartFieldDefinition(_fieldDefinition, "Test", new JObject());
            IDisplayResult result = null;
            foreach (var fiel in _fieldDisplayDrivers)
            {
                result = await fiel.BuildEditorAsync(part, t, typeDef, context);
                if (result != null)
                {
                    break;
                }
            }
            //await _fieldDisplayDrivers.InvokeAsync(async contentDisplay =>
            //{
            //    var result = await contentDisplay.BuildEditorAsync(part, t, typeDef, context);
            //    if (result != null)
            //    {
            //        await result.ApplyAsync(context);
            //    }
            //}, null);

            return Combine(mainResult, result);

            //return await Task.FromResult(mainResult);
        }
        //public override IDisplayResult Edit(TwitterMetaPart sitemapPart)
        //{
            //foreach (var partFieldDefinition in typePartDefinition.PartDefinition.Fields)
            //{
            //    var fieldName = partFieldDefinition.Name;

            //    var fieldPosition = partFieldDefinition.Settings["Position"]?.ToString() ?? "before";

            //    context.DefaultZone = $"Parts.{typePartDefinition.Name}:{fieldPosition}";

            //    await _fieldDisplayDrivers.InvokeAsync(async contentDisplay =>
            //    {
            //        var result = await contentDisplay.BuildEditorAsync(part, partFieldDefinition, typePartDefinition, context);
            //        if (result != null)
            //        {
            //            await result.ApplyAsync(context);
            //        }
            //    }, Logger);
            //}

            //return Combine(Initialize<TwitterMetaPartViewModel>("TwitterMetaPart_Edit", m => BuildEditViewModel(m, sitemapPart))
                //,

                //View("MediaField_Edit", sitemapPart.ImageUrl)
        //        );
        //}


        public override async Task<IDisplayResult> UpdateAsync(TwitterMetaPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix,
                t => t.UseDefaultTwitterCardType,
                t => t.TwitterCardType,
                t => t.UseDefaultCreator,
                t => t.Creator,
                t => t.Title,
                t => t.Description);
            return Edit(model);
        }

        private async Task BuildDisplayViewModelAsync(TwitterMetaPartViewModel model, TwitterMetaPart part)
        {
            var templateContext = new TemplateContext();
            templateContext.SetValue("ContentItem", part.ContentItem);
            templateContext.MemberAccessStrategy.Register<TwitterMetaPartViewModel>();

            using (var writer = new StringWriter())
            {
                await _liquidTemplatemanager.RenderAsync(part.Title, writer, NullEncoder.Default, templateContext);
                model.Title = writer.ToString();
            }
            using (var writer = new StringWriter())
            {
                await _liquidTemplatemanager.RenderAsync(part.Description, writer, NullEncoder.Default, templateContext);
                model.Description = writer.ToString();
            }
            model.TwitterMetaPart = part;
        }

        private void BuildEditViewModel(TwitterMetaPartViewModel model, TwitterMetaPart part)
        {
            model.UseDefaultTwitterCardType = part.UseDefaultTwitterCardType;
            model.TwitterCardType = part.TwitterCardType;
            model.UseDefaultCreator = part.UseDefaultCreator;
            model.Creator = part.Creator;
            model.Title = part.Title;
            model.Description = part.Description;
            //model.ImageUrl = part.ImageUrl;
            model.ImageAlt = part.ImageAlt;
            model.TwitterMetaPart = part;
        }
    }
}
