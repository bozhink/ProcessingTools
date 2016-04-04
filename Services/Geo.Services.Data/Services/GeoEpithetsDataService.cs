namespace ProcessingTools.Geo.Services.Data
{
    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Geo.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common;

    public class GeoEpithetsDataService : GenericEfDataService<GeoEpithet, IGeoEpithetServiceModel, int>, IGeoEpithetsDataService
    {
        public GeoEpithetsDataService(IGeoDataRepository<GeoEpithet> repository)
            : base(repository, e => e.Name.Length)
        {
        }
    }
}
