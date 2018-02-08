namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    public class GeoEpithetsDataService : AbstractGeoMultiDataService<IGeoEpithetsRepository, IGeoEpithet, ITextFilter>, IGeoEpithetsDataService
    {
        public GeoEpithetsDataService(IGeoEpithetsRepository repository)
            : base(repository)
        {
        }
    }
}
