namespace ProcessingTools.Web.Api.Models.MorphologicalEpithets
{
    using Bio.Services.Data.Models.Contracts;
    using ProcessingTools.Contracts.Mapping;

    public class MorphologicalEpithetResponseModel : IMapFrom<IMorphologicalEpithet>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}