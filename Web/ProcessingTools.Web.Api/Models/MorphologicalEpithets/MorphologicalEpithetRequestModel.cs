namespace ProcessingTools.Web.Api.Models.MorphologicalEpithets
{
    using System.ComponentModel.DataAnnotations;

    using Bio.Services.Data.Models;
    using Mappings.Contracts;

    public class MorphologicalEpithetRequestModel : IMapFrom<MorphologicalEpithetServiceModel>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}