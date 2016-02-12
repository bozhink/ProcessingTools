namespace ProcessingTools.Geo.Services.Data
{
    using AutoMapper;

    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Geo.Data.Models;
    using ProcessingTools.Geo.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class GeoNamesDataService : EfGenericCrudDataServiceFactory<GeoName, IGeoName, int>, IGeoNamesDataService
    {
        private readonly IMapper mapper;

        public GeoNamesDataService(IGeoDataRepository<GeoName> repository)
            : base(repository, e => e.Name.Length)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<GeoName, IGeoName>();
                c.CreateMap<IGeoName, GeoName>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}
