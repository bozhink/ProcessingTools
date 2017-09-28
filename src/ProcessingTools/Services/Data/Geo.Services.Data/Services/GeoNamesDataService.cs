namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Data.Contracts.Repositories.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;

    public class GeoNamesDataService : AbstractGeoMultiDataService<IGeoNamesRepository, IGeoName, ITextFilter>, IGeoNamesDataService
    {
        public GeoNamesDataService(IGeoNamesRepository repository)
            : base(repository)
        {
        }
    }
}
