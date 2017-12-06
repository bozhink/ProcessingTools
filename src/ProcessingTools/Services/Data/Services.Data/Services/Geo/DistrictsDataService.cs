namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Contracts.Data.Repositories.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;

    public class DistrictsDataService : AbstractGeoSynonymisableDataService<IDistrictsRepository, IDistrict, IDistrictsFilter, IDistrictSynonym, IDistrictSynonymsFilter>, IDistrictsDataService
    {
        public DistrictsDataService(IDistrictsRepository repository)
            : base(repository)
        {
        }
    }
}
