namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Concurrent;

    using Infrastructure.Concurrency;
    using Models;
    using ServiceClient.Gbif;
    using Taxonomy.Contracts;

    public class GbifTaxaRankDataService : TaxaDataService<ITaxonRank>
    {
        protected override void Delay()
        {
            Delayer.Delay();
        }

        protected override void ResolveRank(string scientificName, ConcurrentQueue<ITaxonRank> taxaRanks)
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
                            taxaRanks.Enqueue(new TaxonRankDataServiceResponseModel
                            {
                                ScientificName = scientificName,
                                Rank = rank
                            });
                        }
                    }
                }
            }
        }
    }
}