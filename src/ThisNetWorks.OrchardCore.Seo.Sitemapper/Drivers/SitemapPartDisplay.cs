using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using ThisNetWorks.OrchardCore.Seo.Sitemapper.Models;
using ThisNetWorks.OrchardCore.Seo.Sitemapper.ViewModels;

namespace ThisNetWorks.OrchardCore.Seo.Sitemapper.Drivers
{
    public class SitemapPartDisplay : ContentPartDisplayDriver<SitemapPart>
    {
        private readonly IContentManager _contentManager;
        private readonly IServiceProvider _serviceProvider;

        public SitemapPartDisplay(
            IContentManager contentManager,
            IServiceProvider serviceProvider
            )
        {
            _contentManager = contentManager;
            _serviceProvider = serviceProvider;
        }

        public override IDisplayResult Edit(SitemapPart sitemapPart)
        {
            return Initialize<SitemapPartViewModel>("SitemapPart_Edit", m => BuildViewModel(m, sitemapPart));
        }

        public override async Task<IDisplayResult> UpdateAsync(SitemapPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix, t => t.ChangeFrequency, t => t.Exclude, t => t.Priority);

            return Edit(model);
        }


        private void BuildViewModel(SitemapPartViewModel model, SitemapPart part)
        {
            model.ChangeFrequency = part.ChangeFrequency;
            model.Exclude = part.Exclude;
            model.Priority = part.Priority;
            model.SitemapPart = part;
        }
    }
}
