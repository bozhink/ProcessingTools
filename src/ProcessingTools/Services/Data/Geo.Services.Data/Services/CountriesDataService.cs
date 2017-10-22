namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Data.Contracts.Repositories.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Contracts.Data.Geo;

    public class CountriesDataService : AbstractGeoSynonymisableDataService<ICountriesRepository, ICountry, ICountriesFilter, ICountrySynonym, ICountrySynonymsFilter>, ICountriesDataService
    {
        public CountriesDataService(ICountriesRepository repository)
            : base(repository)
        {
        }
    }
}
