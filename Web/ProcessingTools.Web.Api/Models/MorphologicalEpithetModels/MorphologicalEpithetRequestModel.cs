namespace ProcessingTools.Web.Api.Models.MorphologicalEpithetModels
{
    using System.ComponentModel.DataAnnotations;

    using Bio.Services.Data.Models.Contracts;
    using ProcessingTools.Contracts.Mapping;

    public class MorphologicalEpithetRequestModel : IMapFrom<IMorphologicalEpithet>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}