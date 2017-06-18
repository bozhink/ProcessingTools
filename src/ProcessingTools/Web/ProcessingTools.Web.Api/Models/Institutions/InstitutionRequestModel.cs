namespace ProcessingTools.Web.Api.Models.Institutions
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.DataResources;
    using ProcessingTools.Services.Data.Contracts.Models;

    public class InstitutionRequestModel : IInstitution
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.InstitutionNameMaximalLength)]
        public string Name { get; set; }
    }
}
