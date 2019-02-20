using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluid;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Liquid;
using ThisNetWorks.OrchardCore.Seo.FacebookMeta.Models;
using ThisNetWorks.OrchardCore.Seo.FacebookMeta.ViewModels;

namespace ThisNetWorks.OrchardCore.Seo.FacebookMeta.Drivers
{
    public class FacebookMetaPartDisplay : ContentPartDisplayDriver<FacebookMetaPart>
    {
        private readonly IContentManager _contentManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILiquidTemplateManager _liquidTemplatemanager;

        public FacebookMetaPartDisplay(
            IContentManager contentManager,
            IServiceProvider serviceProvider,
            ILiquidTemplateManager liquidTemplateManager
            )
        {
            _contentManager = contentManager;
            _serviceProvider = serviceProvider;
            _liquidTemplatemanager = liquidTemplateManager;
        }

        public override IDisplayResult Display(FacebookMetaPart part)
        {
            return Initialize<FacebookMetaPartViewModel>("SeoMetaPart", m => BuildDisplayViewModelAsync(m, part))
                .Location("Detail", "Content:5");
        }



        public override IDisplayResult Edit(FacebookMetaPart sitemapPart)
        {
            return Initialize<FacebookMetaPartViewModel>("SeoMetaPart_Edit", m => BuildEditViewModel(m, sitemapPart));
        }
        
        public override async Task<IDisplayResult> UpdateAsync(FacebookMetaPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix, t => t.PageTitle, t => t.MetaDescription, t => t.MetaKeywords);
            return Edit(model);
        }

        private async Task BuildDisplayViewModelAsync(FacebookMetaPartViewModel model, FacebookMetaPart part)
        {
            var templateContext = new TemplateContext();
            templateContext.SetValue("ContentItem", part.ContentItem);
            templateContext.MemberAccessStrategy.Register<FacebookMetaPartViewModel>();

            using (var writer = new StringWriter())
            {
                await _liquidTemplatemanager.RenderAsync(part.PageTitle, writer, NullEncoder.Default, templateContext);
                model.PageTitle = writer.ToString();
            }
            using (var writer = new StringWriter())
            {
                await _liquidTemplatemanager.RenderAsync(part.MetaDescription, writer, NullEncoder.Default, templateContext);
                model.MetaDescription = writer.ToString();
            }
            using (var writer = new StringWriter())
            {
                await _liquidTemplatemanager.RenderAsync(part.MetaKeywords, writer, NullEncoder.Default, templateContext);
                model.MetaKeywords = writer.ToString();
            }
            model.SeoMetaPart = part;
        }

        private void BuildEditViewModel(FacebookMetaPartViewModel model, FacebookMetaPart part)
        {
            model.PageTitle = part.PageTitle;
            model.MetaDescription = part.MetaDescription;
            model.MetaKeywords = part.MetaKeywords;
            model.SeoMetaPart = part;
        }
    }
}
