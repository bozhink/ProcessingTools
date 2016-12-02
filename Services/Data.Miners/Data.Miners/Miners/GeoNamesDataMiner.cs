namespace ProcessingTools.Data.Miners
{
    using Contracts.Miners;
    using ProcessingTools.Data.Miners.Common;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Geo.Services.Data.Models;

    public class GeoNamesDataMiner : SimpleServiceStringDataMiner<IGeoNamesDataService, GeoNameServiceModel>, IGeoNamesDataMiner
    {
        public GeoNamesDataMiner(IGeoNamesDataService service)
            : base(service)
        {
        }
    }
}
