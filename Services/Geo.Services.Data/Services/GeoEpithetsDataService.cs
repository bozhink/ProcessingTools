namespace ProcessingTools.Geo.Services.Data
{
    using Contracts;
    using Models;

    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Geo.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common;

    public class GeoEpithetsDataService : GenericRepositoryDataService<GeoEpithet, GeoEpithetServiceModel>, IGeoEpithetsDataService
    {
        public GeoEpithetsDataService(IGeoDataRepository<GeoEpithet> repository)
            : base(repository)
        {
        }
    }
}
