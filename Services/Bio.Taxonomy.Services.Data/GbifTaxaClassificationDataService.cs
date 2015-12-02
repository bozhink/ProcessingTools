namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Concurrent;

    using Infrastructure.Concurrency;
    using Models;
    using ServiceClient.Gbif;
    using Taxonomy.Contracts;

    public class GbifTaxaClassificationDataService : TaxaDataService<ITaxonClassification>
    {
        protected override void Delay()
        {
            Delayer.Delay();
        }

        protected override void ResolveRank(string scientificName, ConcurrentQueue<ITaxonClassification> taxaRanks)
        {
            var gbifResult = GbifDataRequester.SearchGbif(scientificName).Result;
            if (gbifResult != null)
            {
                if (!string.IsNullOrWhiteSpace(gbifResult.CanonicalName) ||
                    !string.IsNullOrWhiteSpace(gbifResult.ScientificName))
                {
                    if (gbifResult.CanonicalName.Equals(scientificName) ||
                        gbifResult.ScientificName.Contains(scientificName))
                    {
                        string rank = gbifResult.Rank?.ToLower();
                        if (!string.IsNullOrWhiteSpace(rank))
                        {
                            taxaRanks.Enqueue(new TaxonClassificationDataServiceResponseModel
                            {
                                ScientificName = gbifResult.ScientificName,
                                CanonicalName = gbifResult.CanonicalName,
                                Rank = rank,

                                Kingdom = gbifResult.Kingdom,
                                Phylum = gbifResult.Phylum,
                                Class = gbifResult.Class,
                                Order = gbifResult.Order,
                                Family = gbifResult.Family,
                                Genus = gbifResult.Genus
                            });
                        }
                    }
                }
            }
        }
    }
}