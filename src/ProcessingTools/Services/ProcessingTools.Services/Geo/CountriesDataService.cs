namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    public class CountriesDataService : AbstractGeoSynonymisableDataService<ICountriesRepository, ICountry, ICountriesFilter, ICountrySynonym, ICountrySynonymsFilter>, ICountriesDataService
    {
        public CountriesDataService(ICountriesRepository repository)
            : base(repository)
        {
        }
    }
}
