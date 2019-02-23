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

        public TwitterMetaPartDisplay(
            IContentManager contentManager,
            IServiceProvider serviceProvider,
            ILiquidTemplateManager liquidTemplateManager
            )
        {
            _contentManager = contentManager;
            _serviceProvider = serviceProvider;
            _liquidTemplatemanager = liquidTemplateManager;
        }

        public override IDisplayResult Display(TwitterMetaPart part)
        {
            return Initialize<TwitterMetaPartViewModel>("TwitterMetaPart", m => BuildDisplayViewModelAsync(m, part))
                    .Location("Detail", "Content:6");
        }

        public override IDisplayResult Edit(TwitterMetaPart part, BuildPartEditorContext context)
        {
            return Initialize<TwitterMetaPartViewModel>("TwitterMetaPart", m => BuildEditViewModel(m, part))
           .Location("Detail", "Content:6");
        }
        public override async Task<IDisplayResult> UpdateAsync(TwitterMetaPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix,
                t => t.Title,
                t => t.Description,
                t => t.ImageAlt);
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
            model.Title = part.Title;
            model.Description = part.Description;
            //model.ImageUrl = part.ImageUrl;
            model.ImageAlt = part.ImageAlt;
            model.TwitterMetaPart = part;
        }
    }
}
