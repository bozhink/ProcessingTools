namespace ProcessingTools.Web.Api.Models.MorphologicalEpithets
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Contracts.Models;

    public class MorphologicalEpithetRequestModel : IMapFrom<MorphologicalEpithetServiceModel>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
