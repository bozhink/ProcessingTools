namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    public interface ICountiesDataService : IDataServiceAsync<ICounty, ICountiesFilter>, ISynonymisableDataService<ICounty, ICountySynonym, ICountySynonymsFilter>
    {
    }
}
