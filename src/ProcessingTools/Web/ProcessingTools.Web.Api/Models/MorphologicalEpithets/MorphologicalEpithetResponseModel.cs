namespace ProcessingTools.Web.Api.Models.MorphologicalEpithets
{
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Contracts.Models;

    public class MorphologicalEpithetResponseModel : IMapFrom<MorphologicalEpithetServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
