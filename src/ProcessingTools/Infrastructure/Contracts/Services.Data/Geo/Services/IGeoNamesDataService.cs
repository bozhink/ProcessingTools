namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    public interface IGeoNamesDataService : IDataServiceAsync<IGeoName, IGeoNamesFilter>
    {
    }
}
