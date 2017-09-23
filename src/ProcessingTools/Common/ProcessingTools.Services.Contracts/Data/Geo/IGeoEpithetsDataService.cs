namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Models.Contracts.Geo;

    public interface IGeoEpithetsDataService : IMultiDataServiceAsync<IGeoEpithet, ITextFilter>
    {
    }
}
