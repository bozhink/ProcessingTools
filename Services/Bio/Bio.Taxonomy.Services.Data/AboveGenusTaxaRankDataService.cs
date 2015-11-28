namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models;

    public class AboveGenusTaxaRankDataService : ITaxaRankDataService
    {
        private const string Rank = "above-genus";

        public IQueryable<ITaxonRank> Resolve(params string[] scientificNames)
        {
            var result = new HashSet<ITaxonRank>(scientificNames
                .Select(s => new TaxonRank
                {
                    ScientificName = s,
                    Rank = Rank
                }));

            return result.AsQueryable();
        }
    }
}