namespace ProcessingTools.Web.Api.Controllers
{
    using Bio.Services.Data.Contracts;
    using Bio.Services.Data.Models.Contracts;
    using Factories;
    using Models.TypeStatusModels;

    public class TypeStatusController : GenericDataServiceControllerFactory<ITypeStatus, TypeStatusRequestModel, TypeStatusResponseModel>
    {
        public TypeStatusController(ITypeStatusDataService service)
            : base(service)
        {
        }
    }
}
