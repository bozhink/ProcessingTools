namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Contracts;

    public class AboveGenusTaxaRankResolverDataService : IAboveGenusTaxaRankResolverDataService
    {
        private const string Rank = "above-genus";

        public Task<IQueryable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            return Task.Run(() =>
            {
                var result = new HashSet<ITaxonRank>(scientificNames
                    .Select(s => new TaxonRankServiceModel
                    {
                        ScientificName = s,
                        Rank = Rank
                    }));

                return result.AsQueryable();
            });
        }
    }
}