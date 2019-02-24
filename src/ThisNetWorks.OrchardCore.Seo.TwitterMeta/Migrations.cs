using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Media.Settings;

namespace ThisNetWorks.OrchardCore.Seo.TwitterMeta
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
            _contentDefinitionManager.AlterPartDefinition("TwitterMetaPart", builder => builder
                .Attachable()
                .WithDescription("Provides a part that allows twitter card on a content item."));

            return 1;
        }

        public int UpdateFrom1()
        {
            //_contentDefinitionManager.AlterPartDefinition("TwitterMetaPart", builder => builder
            //    .WithField("TwitterUrl", field => field
            //    .OfType("MediaField")
            //    .WithDisplayName("Twitter Image")
            //        .WithSetting("MediaField.Hint", "Select a twitter image")
            //        .WithSetting("MediaField.Required", "True")
            //        .WithSetting("MediaField.Multiple", "False")));

            return 2;
        }

        public int UpdateFrom2()
        {
            _contentDefinitionManager.AlterPartDefinition("TwitterMetaPart", builder => builder
                .WithField("TwitterImage", field => field
                .OfType("MediaField")
                .WithDisplayName("Twitter Image")
                    .WithSetting("MediaField.Hint", "Select a twitter image")
                    .WithSetting("MediaField.Required", "True")
                    .WithSetting("MediaField.Multiple", "False")));

            return 3;
        }
    }
}
