namespace ProcessingTools.Bio.Taxonomy.Services.Data.Contracts
{
    using System.Linq;

    public interface ITaxaRankDataService
    {
        IQueryable<ITaxonRank> Resolve(params string[] scientificNames);
    }
}