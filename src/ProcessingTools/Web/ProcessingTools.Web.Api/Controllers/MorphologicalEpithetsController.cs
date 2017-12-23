﻿namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Services.Data.Bio;
    using ProcessingTools.Contracts.Services.Data.Bio;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Models.Bio.MorphologicalEpithets;

    public class MorphologicalEpithetsController : GenericDataServiceController<IMorphologicalEpithetsDataService, IMorphologicalEpithet, MorphologicalEpithetRequestModel, MorphologicalEpithetResponseModel, IFilter>
    {
        private readonly IMapper mapper;

        public MorphologicalEpithetsController(IMorphologicalEpithetsDataService service)
            : base(service)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IMorphologicalEpithet, MorphologicalEpithetResponseModel>();
                c.CreateMap<MorphologicalEpithetRequestModel, IMorphologicalEpithet>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}
