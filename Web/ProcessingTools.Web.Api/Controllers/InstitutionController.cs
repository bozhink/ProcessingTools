namespace ProcessingTools.Web.Api.Controllers
{
    using Abstractions;
    using Models.Institutions;
    using Services.Data.Contracts;
    using Services.Data.Contracts.Models;

    public class InstitutionController : GenericDataServiceController<IInstitution, InstitutionRequestModel, InstitutionResponseModel>
    {
        public InstitutionController(IInstitutionsDataService service)
            : base(service)
        {
        }
    }
}
