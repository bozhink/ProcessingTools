namespace ProcessingTools.Geo.Services.Data
{
    using AutoMapper;

    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Geo.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class GeoEpithetsDataService : EfGenericCrudDataServiceFactory<GeoEpithet, IGeoEpithet, int>, IGeoEpithetsDataService
    {
        private readonly IMapper mapper;

        public GeoEpithetsDataService(IGeoDataRepository<GeoEpithet> repository)
            : base(repository, e => e.Name.Length)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<GeoEpithet, IGeoEpithet>();
                c.CreateMap<IGeoEpithet, GeoEpithet>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}