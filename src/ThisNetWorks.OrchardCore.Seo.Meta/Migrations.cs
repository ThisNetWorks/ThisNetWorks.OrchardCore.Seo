using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.ContentManagement.Metadata.Settings;

namespace ThisNetWorks.OrchardCore.Seo.Meta
{
    public class Migrations : DataMigration
    {
        IContentDefinitionManager _contentDefinitionManager;

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition("SeoMetaPart", builder => builder
                .Attachable()
                .WithDescription("Provides a part that allows seo meta descriptions to be applied to a content item."));

            return 1;
        }

    }
}
