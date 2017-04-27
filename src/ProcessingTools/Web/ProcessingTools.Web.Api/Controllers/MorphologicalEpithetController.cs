namespace ProcessingTools.Web.Api.Controllers
{
    using Abstractions;
    using Bio.Services.Data.Contracts;
    using Bio.Services.Data.Models;
    using Models.MorphologicalEpithets;

    public class MorphologicalEpithetController : GenericDataServiceController<MorphologicalEpithetServiceModel, MorphologicalEpithetRequestModel, MorphologicalEpithetResponseModel>
    {
        public MorphologicalEpithetController(IMorphologicalEpithetsDataService service)
            : base(service)
        {
        }
    }
}
