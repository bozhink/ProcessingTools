namespace ProcessingTools.Web.Api.Models.TypeStatuses
{
    using System.ComponentModel.DataAnnotations;

    using Bio.Services.Data.Models;
    using Mappings.Contracts;

    public class TypeStatusRequestModel : IMapFrom<TypeStatusServiceModel>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
