namespace ProcessingTools.Geo.Services.Data.Contracts
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Geo.Services.Data.Models;

    public interface IGeoEpithetsDataService : IMultiDataServiceAsync<GeoEpithetServiceModel, IFilter>
    {
    }
}
