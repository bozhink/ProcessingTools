namespace ProcessingTools.Services.Data.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Data;

    public interface ITaxonRankDataService : IAddableDataService<ITaxonRank>, IDeletableDataService<ITaxonRank>
    {
    }
}
