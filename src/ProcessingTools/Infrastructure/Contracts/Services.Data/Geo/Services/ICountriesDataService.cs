namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Models.Geo;

    public interface ICountriesDataService : IDataServiceAsync<ICountry, ICountriesFilter>, ISynonymisableDataService<ICountry, ICountrySynonym, ICountrySynonymsFilter>
    {
    }
}
