namespace ProcessingTools.Web.Api.Controllers
{
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Api.Models.TypeStatuses;

    public class TypeStatusController : GenericDataServiceController<ITypeStatusDataService, TypeStatusServiceModel, TypeStatusRequestModel, TypeStatusResponseModel, IFilter>
    {
        public TypeStatusController(ITypeStatusDataService service)
            : base(service)
        {
        }
    }
}
