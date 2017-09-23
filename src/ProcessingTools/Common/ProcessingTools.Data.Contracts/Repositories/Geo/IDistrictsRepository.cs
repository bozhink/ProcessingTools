namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    public interface IDistrictsRepository : IRepositoryAsync<IDistrict, IDistrictsFilter>, IGeoSynonymisableRepository<IDistrict, IDistrictSynonym, IDistrictSynonymsFilter>
    {
    }
}
