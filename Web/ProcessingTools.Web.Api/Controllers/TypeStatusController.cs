namespace ProcessingTools.Web.Api.Controllers
{
    using Abstractions;
    using Bio.Services.Data.Contracts;
    using Bio.Services.Data.Models;
    using Models.TypeStatuses;

    public class TypeStatusController : GenericDataServiceController<TypeStatusServiceModel, TypeStatusRequestModel, TypeStatusResponseModel>
    {
        public TypeStatusController(ITypeStatusDataService service)
            : base(service)
        {
        }
    }
}
