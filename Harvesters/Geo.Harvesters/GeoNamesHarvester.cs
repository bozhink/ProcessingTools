namespace ProcessingTools.Geo.Harvesters
{
    using Contracts;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Geo.Services.Data.Models.Contracts;
    using ProcessingTools.Harvesters.Common.Factories;

    public class GeoNamesHarvester : SimpleServiceStringHarvesterFactory<IGeoNamesDataService, IGeoName>, IGeoNamesHarvester
    {
        public GeoNamesHarvester(IGeoNamesDataService service)
            : base(service)
        {
        }
    }
}