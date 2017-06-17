namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Models.Geo;

    public interface ICountiesDataService : IDataServiceAsync<ICounty, ICountiesFilter>, ISynonymisableDataService<ICounty, ICountySynonym, ICountySynonymsFilter>
    {
    }
}
