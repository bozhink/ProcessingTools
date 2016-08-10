namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Models.Contracts;
    using ProcessingTools.Common.Constants;

    public class GbifTaxaClassificationResolverDataService : TaxaInformationResolverDataServiceFactory<ITaxonClassification>, IGbifTaxaClassificationResolverDataService
    {
        private IGbifApiV09DataRequester requester;

        public GbifTaxaClassificationResolverDataService(IGbifApiV09DataRequester requester)
        {
            if (requester == null)
            {
                throw new ArgumentNullException(nameof(requester));
            }

            this.requester = requester;
        }

        protected override void Delay()
        {
            Thread.Sleep(ConcurrencyConstants.DefaultDelayTime);
        }

        protected override async Task ResolveScientificName(string scientificName, ConcurrentQueue<ITaxonClassification> taxaRanks)
        {
            var response = await this.requester.RequestData(scientificName);

            if ((response != null) &&
                (!string.IsNullOrWhiteSpace(response.CanonicalName) ||
                 !string.IsNullOrWhiteSpace(response.ScientificName)))
            {
                if (response.CanonicalName.Equals(scientificName) ||
                    response.ScientificName.Contains(scientificName))
                {
                    taxaRanks.Enqueue(this.MapGbifTaxonToTaxonClassification(response));

                    if (response.Alternatives != null)
                    {
                        response.Alternatives
                            .Where(a => a.CanonicalName.Equals(scientificName) || a.ScientificName.Contains(scientificName))
                            .ToList()
                            .ForEach(a => taxaRanks.Enqueue(this.MapGbifTaxonToTaxonClassification(a)));
                    }
                }
            }
        }

        private ITaxonClassification MapGbifTaxonToTaxonClassification(IGbifTaxon taxon)
        {
            return new TaxonClassificationServiceModel
            {
                ScientificName = taxon.ScientificName,
                CanonicalName = taxon.CanonicalName,
                Rank = taxon.Rank?.ToLower(),

                Kingdom = taxon.Kingdom,
                Phylum = taxon.Phylum,
                Class = taxon.Class,
                Order = taxon.Order,
                Family = taxon.Family,
                Genus = taxon.Genus
            };
        }
    }
}