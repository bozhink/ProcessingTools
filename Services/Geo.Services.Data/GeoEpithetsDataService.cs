namespace ProcessingTools.Geo.Services.Data
{
    using AutoMapper;
    using Contracts;
    using Geo.Data.Models;
    using Geo.Data.Repositories;
    using Models.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class GeoEpithetsDataService : EfGenericCrudDataServiceFactory<GeoEpithet, IGeoEpithet, int>, IGeoEpithetsDataService
    {
        public GeoEpithetsDataService(IGeoDataRepository<GeoEpithet> repository)
            : base(repository, e => e.Name.Length)
        {
            Mapper.CreateMap<GeoEpithet, IGeoEpithet>();
            Mapper.CreateMap<IGeoEpithet, GeoEpithet>();
        }
    }
}