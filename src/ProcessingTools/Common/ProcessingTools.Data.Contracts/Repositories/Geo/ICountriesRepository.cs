namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Models.Geo;

    public interface ICountriesRepository : IRepositoryAsync<ICountry, ICountriesFilter>, IGeoSynonymisableRepository<ICountry, ICountrySynonym, ICountrySynonymsFilter>
    {
    }
}
