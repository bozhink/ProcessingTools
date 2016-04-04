namespace ProcessingTools.Geo.Services.Data
{
    using Contracts;
    using Models;

    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Geo.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common;

    public class GeoEpithetsDataService : GenericEfDataService<GeoEpithet, GeoEpithetServiceModel, int>, IGeoEpithetsDataService
    {
        public GeoEpithetsDataService(IGeoDataRepository<GeoEpithet> repository)
            : base(repository, e => e.Name.Length)
        {
        }
    }
}
