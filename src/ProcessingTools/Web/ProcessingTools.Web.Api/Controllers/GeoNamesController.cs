﻿namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Contracts.Geo;
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
                c.CreateMap<GeoNameRequestModel, IGeoName>().As<GeoNameResponseModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}
