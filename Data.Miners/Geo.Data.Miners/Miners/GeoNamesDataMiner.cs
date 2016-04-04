namespace ProcessingTools.Geo.Data.Miners
{
    using Contracts;
    using ProcessingTools.Data.Miners.Common.Factories;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Geo.Services.Data.Models;

    public class GeoNamesDataMiner : SimpleServiceStringDataMinerFactory<IGeoNamesDataService, GeoNameServiceModel>, IGeoNamesDataMiner
    {
        public GeoNamesDataMiner(IGeoNamesDataService service)
            : base(service)
        {
        }
    }
}