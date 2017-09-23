namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    public interface IStatesRepository : IRepositoryAsync<IState, IStatesFilter>, IGeoSynonymisableRepository<IState, IStateSynonym, IStateSynonymsFilter>
    {
    }
}
