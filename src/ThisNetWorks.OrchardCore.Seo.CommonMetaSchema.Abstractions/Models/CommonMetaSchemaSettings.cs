using System;
using System.Collections.Generic;
using System.Text;

namespace ThisNetWorks.OrchardCore.Seo.CommonMetaSchema.Models
{
    public class CommonMetaSchemaSettings
    {
        //TODO move into Seo?
        public string CanonicalUrl { get; set; }

        //needs to be default, and per page
        public string OpenGraphType { get; }

        //Default social image
        public string DefaultSocialImage { get; set; }

        //TODO 

        public string DefaultTwitterImage { get; set; }


        public string AddressCountry { get; set; }

        public string AddressLocality { get; set; }

        public string AddressPostCode { get; set; }

        public string AddressStreetAddress { get; set; }

        public string AddressType { get; set; }

        public string CompanyDescription { get; set; }

        public string CompanyType { get; set; }

        public string CompanyUrl { get; set; }

        public string CorporationId { get; set; }

        public string DefaultPageDescription { get; set; }


        public string LegalName { get; set; }

        //TODO probably not
        public string LinkedInUrl { get; set; }

        public string Logo { get; set; }



        public string PotentialActionQueryInput { get; set; }

        public string PotentialActionTarget { get; set; }

        public string PotentialActionType { get; set; }

        //can't remember what this one is
        public string ScriptContext { get; set; }

        public string Telephone { get; set; }


    }
}
