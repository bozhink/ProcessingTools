namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Types;

    public class AboveGenusTaxaRankResolverDataService : IAboveGenusTaxaRankResolverDataService
    {
        public Task<IQueryable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            var result = new HashSet<ITaxonRank>(scientificNames
                .Select(s => new TaxonRankServiceModel
                {
                    ScientificName = s,
                    Rank = TaxonRankType.AboveGenus
                }));

            return Task.FromResult(result.AsQueryable());
        }
    }
}