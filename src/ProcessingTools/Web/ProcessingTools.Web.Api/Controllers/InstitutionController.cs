namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Contracts.Models;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Api.Models.Institutions;

    public class InstitutionController : GenericDataServiceController<IInstitutionsDataService, IInstitution, InstitutionRequestModel, InstitutionResponseModel, IFilter>
    {
        private readonly IMapper mapper;

        public InstitutionController(IInstitutionsDataService service)
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
