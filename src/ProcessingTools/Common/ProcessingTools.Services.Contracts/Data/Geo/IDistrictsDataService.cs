namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    public interface IDistrictsDataService : IDataServiceAsync<IDistrict, IDistrictsFilter>, IGeoSynonymisableDataService<IDistrict, IDistrictSynonym, IDistrictSynonymsFilter>
    {
    }
}
