namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Concurrent;
    using System.Linq;

    using Contracts;
    using Infrastructure.Concurrency;
    using Models;
    using ServiceClient.Gbif;
    using ServiceClient.Gbif.Models.Contracts;
    using Taxonomy.Contracts;

    public class GbifTaxaClassificationDataService : TaxaDataServiceFactory<ITaxonClassification>, IGbifTaxaClassificationDataService
    {
        protected override void Delay()
        {
            Delayer.Delay();
        }

        protected override void ResolveScientificName(string scientificName, ConcurrentQueue<ITaxonClassification> taxaRanks)
        {
            var response = GbifDataRequester.SearchGbif(scientificName)?.Result;

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
            return new TaxonClassificationDataServiceResponseModel
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