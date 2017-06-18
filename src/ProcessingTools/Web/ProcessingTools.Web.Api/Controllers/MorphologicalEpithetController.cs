namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Api.Models.MorphologicalEpithets;

    public class MorphologicalEpithetController : GenericDataServiceController<IMorphologicalEpithetsDataService, MorphologicalEpithetServiceModel, MorphologicalEpithetRequestModel, MorphologicalEpithetResponseModel, IFilter>
    {
        private readonly IMapper mapper;

        public MorphologicalEpithetController(IMorphologicalEpithetsDataService service)
            : base(service)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<MorphologicalEpithetServiceModel, MorphologicalEpithetResponseModel>();
                c.CreateMap<MorphologicalEpithetRequestModel, MorphologicalEpithetServiceModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}
