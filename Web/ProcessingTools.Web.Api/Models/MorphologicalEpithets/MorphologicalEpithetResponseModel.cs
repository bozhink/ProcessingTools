namespace ProcessingTools.Web.Api.Models.MorphologicalEpithets
{
    using Bio.Services.Data.Models;
    using Mappings.Contracts;

    public class MorphologicalEpithetResponseModel : IMapFrom<MorphologicalEpithetServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}