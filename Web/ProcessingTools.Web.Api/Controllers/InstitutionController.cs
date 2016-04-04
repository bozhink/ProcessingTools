namespace ProcessingTools.Web.Api.Controllers
{
    using Factories;
    using Models.Institutions;
    using Services.Data.Contracts;
    using Services.Data.Models;

    public class InstitutionController : GenericDataServiceControllerFactory<InstitutionServiceModel, InstitutionRequestModel, InstitutionResponseModel>
    {
        public InstitutionController(IInstitutionsDataService service)
            : base(service)
        {
        }
    }
}