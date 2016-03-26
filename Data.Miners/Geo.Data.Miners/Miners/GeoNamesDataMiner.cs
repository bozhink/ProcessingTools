namespace ProcessingTools.Geo.Data.Miners
{
    using Contracts;
    using ProcessingTools.Data.Miners.Common.Factories;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Geo.Services.Data.Models.Contracts;

    public class GeoNamesDataMiner : SimpleServiceStringDataMinerFactory<IGeoNamesDataService, IGeoNameServiceModel>, IGeoNamesDataMiner
    {
        public GeoNamesDataMiner(IGeoNamesDataService service)
            : base(service)
        {
        }
    }
}