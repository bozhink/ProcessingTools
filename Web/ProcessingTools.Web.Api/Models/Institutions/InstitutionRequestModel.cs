namespace ProcessingTools.Web.Api.Models.Institutions
{
    using System.ComponentModel.DataAnnotations;

    using Mappings.Contracts;
    using Services.Data.Models;

    public class InstitutionRequestModel : IMapFrom<InstitutionServiceModel>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
    }
}