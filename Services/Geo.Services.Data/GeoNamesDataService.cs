namespace ProcessingTools.Geo.Services.Data
{
    using AutoMapper;
    using Contracts;
    using Geo.Data.Models;
    using Geo.Data.Repositories.Contracts;
    using Models.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class GeoNamesDataService : EfGenericCrudDataServiceFactory<GeoName, IGeoName, int>, IGeoNamesDataService
    {
        public GeoNamesDataService(IGeoDataRepository<GeoName> repository)
            : base(repository, e => e.Name.Length)
        {
            Mapper.CreateMap<GeoName, IGeoName>();
            Mapper.CreateMap<IGeoName, GeoName>();
        }
    }
}