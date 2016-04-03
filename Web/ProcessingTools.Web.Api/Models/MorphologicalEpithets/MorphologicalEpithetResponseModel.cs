namespace ProcessingTools.Web.Api.Models.MorphologicalEpithets
{
    using Bio.Services.Data.Models.Contracts;
    using Mappings.Contracts;

    public class MorphologicalEpithetResponseModel : IMapFrom<IMorphologicalEpithetServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}