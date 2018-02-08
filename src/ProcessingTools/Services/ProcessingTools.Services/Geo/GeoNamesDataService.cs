namespace ProcessingTools.Services.Geo
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Abstractions.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    public class GeoNamesDataService : AbstractGeoMultiDataService<IGeoNamesRepository, IGeoName, ITextFilter>, IGeoNamesDataService
    {
        public GeoNamesDataService(IGeoNamesRepository repository)
            : base(repository)
        {
        }
    }
}
