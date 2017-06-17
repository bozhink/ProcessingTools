namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Models.Geo;

    public interface ICitiesRepository : IRepositoryAsync<ICity, ICitiesFilter>, IGeoSynonymisableRepository<ICity, ICitySynonym, ICitySynonymsFilter>
    {
    }
}
