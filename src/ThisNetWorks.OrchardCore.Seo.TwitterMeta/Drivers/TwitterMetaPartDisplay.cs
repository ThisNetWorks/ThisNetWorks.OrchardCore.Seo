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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using OrchardCore.Settings;
using OrchardCore.Entities;
using ThisNetWorks.OrchardCore.Seo.Meta.Models;

namespace ThisNetWorks.OrchardCore.Seo.TwitterMeta.Drivers
{
    public class TwitterMetaPartDisplay : ContentPartDisplayDriver<TwitterMetaPart>
    {
        private readonly IContentManager _contentManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILiquidTemplateManager _liquidTemplatemanager;
        private readonly ISiteService _siteService;

        public TwitterMetaPartDisplay(
            IContentManager contentManager,
            IServiceProvider serviceProvider,
            ILiquidTemplateManager liquidTemplateManager,
            ISiteService siteService
            )
        {
            _contentManager = contentManager;
            _serviceProvider = serviceProvider;
            _liquidTemplatemanager = liquidTemplateManager;
            _siteService = siteService;
        }

        public override IDisplayResult Display(TwitterMetaPart part)
        {
            return Initialize<TwitterMetaPartViewModel>("TwitterMetaPart", m => BuildDisplayViewModelAsync(m, part))
                    .Location("Detail", "Content:6");
        }

        public override IDisplayResult Edit(TwitterMetaPart part, BuildPartEditorContext context)
        {
            return Initialize<TwitterMetaPartViewModel>("TwitterMetaPart", m => BuildEditViewModel(m, part))
           .Location("Parts#Seo:5");
        }
        public override async Task<IDisplayResult> UpdateAsync(TwitterMetaPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix,
                t => t.Title,
                t => t.Description,
                t => t.ImageAlt,
                t => t.GoogleSchema);
            return Edit(model);
        }

        private async Task BuildDisplayViewModelAsync(TwitterMetaPartViewModel model, TwitterMetaPart part)
        {
            var siteSettings = await _siteService.GetSiteSettingsAsync();
            var twitterMetaSettings = siteSettings.As<TwitterMetaSettings>();

            var templateContext = new TemplateContext();
            templateContext.SetValue("ContentItem", part.ContentItem);
            templateContext.MemberAccessStrategy.Register<TwitterMetaPartViewModel>();

            using (var writer = new StringWriter())
            {
                await _liquidTemplatemanager.RenderAsync(part.Title, writer, NullEncoder.Default, templateContext);
                model.Title = writer.ToString();
            }
            if (!String.IsNullOrEmpty(part.Description))
            {
                using (var writer = new StringWriter())
                {
                    await _liquidTemplatemanager.RenderAsync(part.Description, writer, NullEncoder.Default, templateContext);
                    model.Description = writer.ToString();
                }
            }
            else
            {
                SeoMetaPart seoMetaPart = part.ContentItem.As<SeoMetaPart>();
                if (seoMetaPart != null && !String.IsNullOrEmpty(seoMetaPart.MetaDescription))
                {
                    using (var writer = new StringWriter())
                    {
                        await _liquidTemplatemanager.RenderAsync(seoMetaPart.MetaDescription, writer, NullEncoder.Default, templateContext);
                        model.Description = writer.ToString();
                    }
                }
                else
                {
                    model.Description = part.ContentItem.DisplayText;
                }
            }
            //var contentItemMetadata = await _contentManager.PopulateAspectAsync<ContentItemMetadata>(part.ContentItem);

            //var t = new StringValue(((IUrlHelper)urlHelper).RouteUrl(contentItemMetadata.DisplayRouteValues));
            model.TwitterMetaPart = part;

            model.TwitterMetaSettings = twitterMetaSettings;

            model.GoogleSchema = part.GoogleSchema;
        }

        private void BuildEditViewModel(TwitterMetaPartViewModel model, TwitterMetaPart part)
        {
            model.Title = part.Title;
            model.Description = part.Description;
            //model.ImageUrl = part.ImageUrl;
            model.ImageAlt = part.ImageAlt;
            model.GoogleSchema = part.GoogleSchema;
            model.TwitterMetaPart = part;
        }
    }
}
