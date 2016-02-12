namespace ProcessingTools.Geo.Services.Data
{
    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Geo.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class GeoEpithetsDataService : EfGenericCrudDataServiceFactory<GeoEpithet, IGeoEpithet, int>, IGeoEpithetsDataService
    {
        public GeoEpithetsDataService(IGeoDataRepository<GeoEpithet> repository)
            : base(repository, e => e.Name.Length)
        {
        }
    }
}
