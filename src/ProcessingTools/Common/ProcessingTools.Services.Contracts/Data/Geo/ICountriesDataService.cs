namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    public interface ICountriesDataService : IDataServiceAsync<ICountry, ICountriesFilter>, IGeoSynonymisableDataService<ICountry, ICountrySynonym, ICountrySynonymsFilter>
    {
    }
}
