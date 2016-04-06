namespace ProcessingTools.Geo.Services.Data
{
    using Contracts;
    using Models;

    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Geo.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common;

    public class GeoNamesDataService : GenericRepositoryDataService<GeoName, GeoNameServiceModel>, IGeoNamesDataService
    {
        public GeoNamesDataService(IGeoDataRepository<GeoName> repository)
            : base(repository)
        {
        }
    }
}
