namespace ProcessingTools.Web.Api.Controllers
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Contracts.Models;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Api.Models.Institutions;

    public class InstitutionController : GenericDataServiceController<IInstitutionsDataService, IInstitution, InstitutionRequestModel, InstitutionResponseModel, IFilter>
    {
        public InstitutionController(IInstitutionsDataService service)
            : base(service)
        {
        }
    }
}
