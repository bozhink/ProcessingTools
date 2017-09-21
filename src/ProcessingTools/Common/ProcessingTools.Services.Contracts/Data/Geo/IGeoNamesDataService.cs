namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Models.Geo;

    public interface IGeoNamesDataService : IMultiDataServiceAsync<IGeoName, ITextFilter>
    {
    }
}
