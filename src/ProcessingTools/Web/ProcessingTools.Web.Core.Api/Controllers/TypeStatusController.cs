namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Services.Contracts.Bio;
    using ProcessingTools.Services.Models.Contracts.Bio;
    using ProcessingTools.Web.Core.Api.Abstractions;
    using ProcessingTools.Web.Models.Bio.TypeStatuses;

    [Route("api/[controller]")]
    [ApiController]
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
