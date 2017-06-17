namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Models.Geo;

    public interface IDistrictsDataService : IDataServiceAsync<IDistrict, IDistrictsFilter>, ISynonymisableDataService<IDistrict, IDistrictSynonym, IDistrictSynonymsFilter>
    {
    }
}
