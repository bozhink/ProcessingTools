namespace ProcessingTools.Web.Api.Models.Institutions
{
    using System.ComponentModel.DataAnnotations;

    using Contracts.Mapping;
    using Services.Data.Models.Contracts;

    public class InstitutionRequestModel : IMapFrom<IInstitutionServiceModel>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
    }
}