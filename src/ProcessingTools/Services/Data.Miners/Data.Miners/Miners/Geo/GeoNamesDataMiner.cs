namespace ProcessingTools.Data.Miners.Miners.Geo
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Data.Miners.Contracts.Miners.Geo;
    using ProcessingTools.Data.Miners.Generics;

    public class GeoNamesDataMiner : SimpleServiceStringDataMiner<IGeoNamesDataService, IGeoName, ITextFilter>, IGeoNamesDataMiner
    {
        public GeoNamesDataMiner(IGeoNamesDataService service)
            : base(service)
        {
        }
    }
}
