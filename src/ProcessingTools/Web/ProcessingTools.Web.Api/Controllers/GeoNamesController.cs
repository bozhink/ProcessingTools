namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Models.Geo.GeoNames;

    public class GeoNamesController : GenericDataServiceController<IGeoNamesDataService, IGeoName, GeoNameRequestModel, GeoNameResponseModel, ITextFilter>
    {
        private readonly IMapper mapper;

        public GeoNamesController(IGeoNamesDataService service)
            : base(service)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IGeoName, GeoNameResponseModel>();
                c.CreateMap<GeoNameRequestModel, IGeoName>().ConvertUsing(g => g);
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}
