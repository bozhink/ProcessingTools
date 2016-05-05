namespace ProcessingTools.Geo.Data.Miners
{
    using Contracts;
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