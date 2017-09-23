namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Contracts.Data.Repositories.Geo;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;

    public class GeoEpithetsDataService : AbstractGeoMultiDataService<IGeoEpithetsRepository, IGeoEpithet, ITextFilter>, IGeoEpithetsDataService
    {
        public GeoEpithetsDataService(IGeoEpithetsRepository repository)
            : base(repository)
        {
        }
    }
}
