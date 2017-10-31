namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    public class AboveGenusTaxaRankResolver : IAboveGenusTaxaRankResolver
    {
        public Task<IEnumerable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            var data = scientificNames
                .Select(s => new TaxonRank
                {
                    ScientificName = s,
                    Rank = TaxonRankType.AboveGenus
                })
                .ToArray<ITaxonRank>();

            return Task.FromResult<IEnumerable<ITaxonRank>>(data);
        }
    }
}
