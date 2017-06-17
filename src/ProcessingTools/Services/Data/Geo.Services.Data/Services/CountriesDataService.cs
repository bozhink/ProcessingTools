namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Contracts.Data.Repositories.Geo;
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;

    public class CountriesDataService : AbstractGeoSynonymisableDataService<ICountriesRepository, ICountry, ICountriesFilter, ICountrySynonym, ICountrySynonymsFilter>, ICountriesDataService
    {
        public CountriesDataService(ICountriesRepository repository)
            : base(repository)
        {
        }
    }
}
