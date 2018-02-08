namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    public class GeoEpithetsDataService : AbstractGeoMultiDataService<IGeoEpithetsRepository, IGeoEpithet, ITextFilter>, IGeoEpithetsDataService
    {
        public GeoEpithetsDataService(IGeoEpithetsRepository repository)
            : base(repository)
        {
        }
    }
}
