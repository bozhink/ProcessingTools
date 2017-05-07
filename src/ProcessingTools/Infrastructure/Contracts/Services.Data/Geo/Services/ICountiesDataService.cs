namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    public interface ICountiesDataService : IDataServiceAsync<ICounty, ICountiesFilter>, ISynonymisableDataService<ICountySynonym, ICountySynonymsFilter>
    {
    }
}
