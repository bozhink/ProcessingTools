namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    public interface IRegionsDataService : IDataServiceAsync<IRegion, IRegionsFilter>, IGeoSynonymisableDataService<IRegion, IRegionSynonym, IRegionSynonymsFilter>
    {
    }
}
