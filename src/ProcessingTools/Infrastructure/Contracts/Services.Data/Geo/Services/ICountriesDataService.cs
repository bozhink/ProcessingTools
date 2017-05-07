namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    public interface ICountriesDataService : IDataServiceAsync<ICountry, ICountriesFilter>, ISynonymisableDataService<ICountrySynonym, ICountrySynonymsFilter>
    {
    }
}
