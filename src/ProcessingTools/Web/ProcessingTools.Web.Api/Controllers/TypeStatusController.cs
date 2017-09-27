namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Models.Bio.TypeStatuses;

    public class TypeStatusController : GenericDataServiceController<ITypeStatusDataService, TypeStatusServiceModel, TypeStatusRequestModel, TypeStatusResponseModel, IFilter>
    {
        private readonly IMapper mapper;

        public TypeStatusController(ITypeStatusDataService service)
            : base(service)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<TypeStatusServiceModel, TypeStatusResponseModel>();
                c.CreateMap<TypeStatusRequestModel, TypeStatusServiceModel>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}
