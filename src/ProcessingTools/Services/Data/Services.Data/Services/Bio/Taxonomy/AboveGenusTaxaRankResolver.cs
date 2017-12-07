namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    public class AboveGenusTaxaRankResolver : IAboveGenusTaxaRankResolver
    {
        public Task<ITaxonRank[]> ResolveAsync(params string[] scientificNames)
        {
            var data = scientificNames
                .Select(s => new TaxonRank
                {
                    ScientificName = s,
                    Rank = TaxonRankType.AboveGenus
                })
                .ToArray<ITaxonRank>();

            return Task.FromResult(data);
        }
    }
}
