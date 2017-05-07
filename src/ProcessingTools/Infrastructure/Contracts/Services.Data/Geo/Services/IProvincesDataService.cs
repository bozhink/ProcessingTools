namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    public interface IProvincesDataService : IDataServiceAsync<IProvince, IProvincesFilter>, ISynonymisableDataService<IProvinceSynonym, IProvinceSynonymsFilter>
    {
    }
}
