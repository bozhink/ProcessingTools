namespace ProcessingTools.Geo.Harvesters
{
    using Contracts;
    using ProcessingTools.Geo.Services.Data.Contracts;
    using ProcessingTools.Geo.Services.Data.Models.Contracts;
    using ProcessingTools.Harvesters.Common.Factories;

    public class GeoEpithetsHarvester : SimpleServiceStringHarvesterFactory<IGeoEpithetsDataService, IGeoEpithet>, IGeoEpithetsHarvester
    {
        public GeoEpithetsHarvester(IGeoEpithetsDataService service)
            : base(service)
        {
        }
    }
}