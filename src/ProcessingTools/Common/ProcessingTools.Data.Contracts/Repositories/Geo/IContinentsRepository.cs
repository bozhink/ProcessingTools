namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Models.Geo;

    public interface IContinentsRepository : IRepositoryAsync<IContinent, IContinentsFilter>, IGeoSynonymisableRepository<IContinent, IContinentSynonym, IContinentSynonymsFilter>
    {
    }
}
