namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using Infrastructure.Concurrency;
    using Models;
    using ServiceClient.Aphia;
    using Taxonomy.Contracts;

    public class AphiaTaxaRankDataService : TaxaDataService<ITaxonRank>
    {
        protected override void Delay()
        {
            Delayer.Delay();
        }

        protected override void ResolveRank(string scientificName, ConcurrentQueue<ITaxonRank> taxaRanks)
        {
            using (var aphiaService = new AphiaNameService())
            {
                var aphiaRecords = aphiaService.getAphiaRecords(scientificName, false, true, false, 0);
                if (aphiaRecords != null && aphiaRecords.Length > 0)
                {
                    var ranks = new HashSet<string>(aphiaRecords
                        .Where(s => string.Compare(s.scientificname, scientificName, true) == 0)
                        .Select(s => s.rank.ToLower()));

                    foreach (var rank in ranks)
                    {
                        taxaRanks.Enqueue(new TaxonRankDataServiceResponseModel
                        {
                            ScientificName = scientificName,
                            Rank = ranks.FirstOrDefault()
                        });
                    }
                }
            }
        }
    }
}