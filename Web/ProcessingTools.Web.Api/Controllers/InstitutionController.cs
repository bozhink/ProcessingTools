namespace ProcessingTools.Web.Api.Controllers
{
    using Factories;
    using Models.Institutions;
    using Services.Data.Contracts;
    using Services.Data.Models.Contracts;

    public class InstitutionController : GenericDataServiceControllerFactory<IInstitutionServiceModel, InstitutionRequestModel, InstitutionResponseModel>
    {
        public InstitutionController(IInstitutionsDataService service)
            : base(service)
        {
        }
    }
}