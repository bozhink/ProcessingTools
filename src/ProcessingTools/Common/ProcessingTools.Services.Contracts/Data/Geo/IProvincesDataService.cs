namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    public interface IProvincesDataService : IDataServiceAsync<IProvince, IProvincesFilter>, IGeoSynonymisableDataService<IProvince, IProvinceSynonym, IProvinceSynonymsFilter>
    {
    }
}
