namespace ProcessingTools.Web.Api.Models.TypeStatuses
{
    using System.ComponentModel.DataAnnotations;

    using Bio.Services.Data.Models.Contracts;
    using ProcessingTools.Contracts.Mapping;

    public class TypeStatusRequestModel : IMapFrom<ITypeStatusServiceModel>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
