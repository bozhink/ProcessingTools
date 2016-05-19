namespace ProcessingTools.Web.Api.Controllers
{
    using Bio.Services.Data.Contracts;
    using Bio.Services.Data.Models;
    using Factories;
    using Models.TypeStatuses;

    public class TypeStatusController : GenericDataServiceControllerFactory<TypeStatusServiceModel, TypeStatusRequestModel, TypeStatusResponseModel>
    {
        public TypeStatusController(ITypeStatusDataService service)
            : base(service)
        {
        }
    }
}
