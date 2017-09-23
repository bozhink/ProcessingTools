namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    public interface ICitiesRepository : IRepositoryAsync<ICity, ICitiesFilter>, IGeoSynonymisableRepository<ICity, ICitySynonym, ICitySynonymsFilter>
    {
    }
}
