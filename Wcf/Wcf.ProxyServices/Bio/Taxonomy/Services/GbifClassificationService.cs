namespace ProcessingTools.Wcf.ProxyServices.Bio.Taxonomy.Services
{
    using System;
    using System.Linq;

    using DataContracts;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif;
    using ProcessingTools.Bio.Taxonomy.Services.Data;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Net.Factories;
    using ServiceContracts;

    public class GbifClassificationService : IGbifClassificationService
    {
        private readonly IGbifTaxaClassificationResolverDataService service;

        // TODO
        public GbifClassificationService()
            : this(
                  new GbifTaxaClassificationResolverDataService(
                      new GbifApiV09DataRequester(
                          new NetConnectorFactory())))
        {
        }

        public GbifClassificationService(IGbifTaxaClassificationResolverDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public TaxonClassification GetClassification(string scientificName)
        {
            var result = this.service.Resolve(scientificName).Result.FirstOrDefault();

            return new TaxonClassification
            {
                ScientificName = result.ScientificName,
                Rank = result.Rank.MapTaxonRankTypeToTaxonRankString()
            };
        }
    }
}
