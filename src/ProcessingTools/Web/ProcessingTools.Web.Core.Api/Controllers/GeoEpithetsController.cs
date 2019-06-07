namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Geo;
    using ProcessingTools.Services.Contracts.Geo;
    using ProcessingTools.Web.Core.Api.Abstractions;
    using ProcessingTools.Web.Models.Geo.GeoEpithets;

    [Route("api/[controller]")]
    [ApiController]
    public class GeoEpithetsController : GenericDataServiceController<IGeoEpithetsDataService, IGeoEpithet, GeoEpithetRequestModel, GeoEpithetResponseModel, ITextFilter>
    {
        private readonly IMapper mapper;

        public GeoEpithetsController(IGeoEpithetsDataService service)
            : base(service)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IGeoEpithet, GeoEpithetResponseModel>();
                c.CreateMap<GeoEpithetRequestModel, IGeoEpithet>().As<GeoEpithetResponseModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}
