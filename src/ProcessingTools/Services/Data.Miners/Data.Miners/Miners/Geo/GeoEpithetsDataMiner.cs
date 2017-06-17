namespace ProcessingTools.Data.Miners.Miners.Geo
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Data.Miners.Contracts.Miners.Geo;
    using ProcessingTools.Data.Miners.Generics;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Geo.Services.Data.Models;

    public class GeoEpithetsDataMiner : SimpleServiceStringDataMiner<IGeoEpithetsDataService, GeoEpithetServiceModel, IFilter>, IGeoEpithetsDataMiner
    {
        public GeoEpithetsDataMiner(IGeoEpithetsDataService service)
            : base(service)
        {
        }
    }
}
