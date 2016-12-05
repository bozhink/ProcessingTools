using ProcessingTools.Data.Miners.Contracts.Miners.Geo;
using ProcessingTools.Data.Miners.Generics;
using ProcessingTools.Geo.Services.Data.Contracts;
using ProcessingTools.Geo.Services.Data.Models;

namespace ProcessingTools.Data.Miners.Miners.Geo
{
    public class GeoNamesDataMiner : SimpleServiceStringDataMiner<IGeoNamesDataService, GeoNameServiceModel>, IGeoNamesDataMiner
    {
        public GeoNamesDataMiner(IGeoNamesDataService service)
            : base(service)
        {
        }
    }
}
