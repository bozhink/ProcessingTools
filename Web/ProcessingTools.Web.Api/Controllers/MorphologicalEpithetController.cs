namespace ProcessingTools.Web.Api.Controllers
{
    using Bio.Services.Data.Contracts;
    using Bio.Services.Data.Models.Contracts;
    using Factories;
    using Models.MorphologicalEpithets;

    public class MorphologicalEpithetController : GenericDataServiceControllerFactory<IMorphologicalEpithetServiceModel, MorphologicalEpithetRequestModel, MorphologicalEpithetResponseModel>
    {
        public MorphologicalEpithetController(IMorphologicalEpithetsDataService service)
            : base(service)
        {
        }
    }
}