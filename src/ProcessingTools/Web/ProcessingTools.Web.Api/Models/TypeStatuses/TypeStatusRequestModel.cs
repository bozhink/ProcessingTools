namespace ProcessingTools.Web.Api.Models.TypeStatuses
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Contracts.Models;

    public class TypeStatusRequestModel : IMapFrom<TypeStatusServiceModel>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
