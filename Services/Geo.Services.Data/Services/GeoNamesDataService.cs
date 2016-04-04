namespace ProcessingTools.Geo.Services.Data
{
    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Geo.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common;

    public class GeoNamesDataService : GenericEfDataService<GeoName, IGeoNameServiceModel, int>, IGeoNamesDataService
    {
        public GeoNamesDataService(IGeoDataRepository<GeoName> repository)
            : base(repository, e => e.Name.Length)
        {
        }
    }
}
