namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Services.Contracts.Bio;
    using ProcessingTools.Services.Models.Contracts.Bio;
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
