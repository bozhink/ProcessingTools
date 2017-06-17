namespace ProcessingTools.Web.Api.Controllers
{
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Api.Models.MorphologicalEpithets;

    public class MorphologicalEpithetController : GenericDataServiceController<IMorphologicalEpithetsDataService, MorphologicalEpithetServiceModel, MorphologicalEpithetRequestModel, MorphologicalEpithetResponseModel, IFilter>
    {
        public MorphologicalEpithetController(IMorphologicalEpithetsDataService service)
            : base(service)
        {
        }
    }
}
