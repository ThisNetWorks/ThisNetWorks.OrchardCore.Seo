using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using ThisNetWorks.OrchardCore.Seo.Meta.Models;
using ThisNetWorks.OrchardCore.Seo.Meta.ViewModels;

namespace ThisNetWorks.OrchardCore.Seo.Meta.Drivers
{
    public class SeoMetaPartDisplay : ContentPartDisplayDriver<SeoMetaPart>
    {
        private readonly IContentManager _contentManager;
        private readonly IServiceProvider _serviceProvider;

        public SeoMetaPartDisplay(
            IContentManager contentManager,
            IServiceProvider serviceProvider
            )
        {
            _contentManager = contentManager;
            _serviceProvider = serviceProvider;
        }

        public override IDisplayResult Display(SeoMetaPart part)
        {
            return Initialize<SeoMetaPartViewModel>("SeoMetaPart", model => {
                model.PageTitle = part.PageTitle;
                model.MetaDescription = part.MetaDescription;
                model.MetaKeywords = part.MetaKeywords;
                model.SeoMetaPart = part;
            }).Location("Detail", "Content:5");
        }

        public override IDisplayResult Edit(SeoMetaPart sitemapPart)
        {
            return Initialize<SeoMetaPartViewModel>("SeoMetaPart_Edit", m =>
            {
                BuildViewModel(m, sitemapPart);
            });
        }
        
        public override async Task<IDisplayResult> UpdateAsync(SeoMetaPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix, t => t.PageTitle, t => t.MetaDescription, t => t.MetaKeywords);
            

            return Edit(model);
        }


        private void BuildViewModel(SeoMetaPartViewModel model, SeoMetaPart part)
        {
            model.PageTitle = part.PageTitle;
            model.MetaDescription = part.MetaDescription;
            model.MetaKeywords = part.MetaKeywords;
            model.SeoMetaPart = part;
            
        }
    }
}
