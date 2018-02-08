namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Resources;
    using ProcessingTools.Services.Contracts.Resources;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Models.Resources.Institutions;

    public class InstitutionsController : GenericDataServiceController<IInstitutionsDataService, IInstitution, InstitutionRequestModel, InstitutionResponseModel, IFilter>
    {
        private readonly IMapper mapper;

        public InstitutionsController(IInstitutionsDataService service)
            : base(service)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IInstitution, InstitutionResponseModel>();
                c.CreateMap<InstitutionRequestModel, IInstitution>().ConvertUsing(i => i);
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}
