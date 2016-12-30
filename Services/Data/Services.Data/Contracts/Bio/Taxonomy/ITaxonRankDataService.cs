namespace ProcessingTools.Services.Data.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Contracts.Services.Data;

    public interface ITaxonRankDataService : IAddableDataService<ITaxonRank>, IDeletableDataService<ITaxonRank>
    {
    }
}
