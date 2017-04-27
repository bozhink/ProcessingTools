namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Bio.Taxonomy;
    using Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Enumerations;

    public class AboveGenusTaxaRankResolver : IAboveGenusTaxaRankResolver
    {
        public async Task<IEnumerable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            var result = new HashSet<ITaxonRank>(scientificNames
                .Select(s => new TaxonRankServiceModel
                {
                    ScientificName = s,
                    Rank = TaxonRankType.AboveGenus
                }));

            return await Task.FromResult(result);
        }
    }
}
