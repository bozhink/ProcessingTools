namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Api.Models.GeoEpithets;

    public class GeoEpithetsController : GenericDataServiceController<IGeoEpithetsDataService, IGeoEpithet, GeoEpithetRequestModel, GeoEpithetResponseModel, ITextFilter>
    {
        private readonly IMapper mapper;

        public GeoEpithetsController(IGeoEpithetsDataService service)
            : base(service)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IGeoEpithet, GeoEpithetResponseModel>();
                c.CreateMap<GeoEpithetRequestModel, IGeoEpithet>().ConvertUsing(g => g);
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}
