namespace ProcessingTools.Web.Api.Models.MorphologicalEpithets
{
    using Bio.Services.Data.Models.Contracts;
    using ProcessingTools.Contracts.Mapping;

    public class MorphologicalEpithetResponseModel : IMapFrom<IMorphologicalEpithetServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}