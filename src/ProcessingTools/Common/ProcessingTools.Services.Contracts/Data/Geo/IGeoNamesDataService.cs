namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Models.Contracts.Geo;

    public interface IGeoNamesDataService : IMultiDataServiceAsync<IGeoName, ITextFilter>
    {
    }
}
