namespace ProcessingTools.Data.Miners
{
    using Contracts.Miners;
    using Generics;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Geo.Services.Data.Models;

    public class GeoEpithetsDataMiner : SimpleServiceStringDataMiner<IGeoEpithetsDataService, GeoEpithetServiceModel>, IGeoEpithetsDataMiner
    {
        public GeoEpithetsDataMiner(IGeoEpithetsDataService service)
            : base(service)
        {
        }
    }
}
