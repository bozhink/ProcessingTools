namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    public class DistrictsDataService : AbstractGeoSynonymisableDataService<IDistrictsRepository, IDistrict, IDistrictsFilter, IDistrictSynonym, IDistrictSynonymsFilter>, IDistrictsDataService
    {
        public DistrictsDataService(IDistrictsRepository repository)
            : base(repository)
        {
        }
    }
}
