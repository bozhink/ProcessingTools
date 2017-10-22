namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Services.Contracts.Data.Bio;
    using ProcessingTools.Services.Models.Contracts.Data.Bio;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Models.Bio.TypeStatuses;

    public class TypeStatusController : GenericDataServiceController<ITypeStatusDataService, ITypeStatus, TypeStatusRequestModel, TypeStatusResponseModel, IFilter>
    {
        private readonly IMapper mapper;

        public TypeStatusController(ITypeStatusDataService service)
            : base(service)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<ITypeStatus, TypeStatusResponseModel>();
                c.CreateMap<TypeStatusRequestModel, ITypeStatus>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}
