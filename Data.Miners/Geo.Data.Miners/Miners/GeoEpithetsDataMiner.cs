namespace ProcessingTools.Geo.Data.Miners
{
    using Contracts;
    using ProcessingTools.Data.Miners.Common.Factories;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Geo.Services.Data.Models.Contracts;

    public class GeoEpithetsDataMiner : SimpleServiceStringDataMinerFactory<IGeoEpithetsDataService, IGeoEpithetServiceModel>, IGeoEpithetsDataMiner
    {
        public GeoEpithetsDataMiner(IGeoEpithetsDataService service)
            : base(service)
        {
        }
    }
}