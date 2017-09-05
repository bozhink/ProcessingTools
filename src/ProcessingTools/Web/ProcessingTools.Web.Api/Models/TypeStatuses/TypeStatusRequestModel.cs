namespace ProcessingTools.Web.Api.Models.TypeStatuses
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Constants.Data.Bio;
    using ProcessingTools.Contracts.Models;

    public class TypeStatusRequestModel : IMapFrom<TypeStatusServiceModel>
    {
        public int? Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfTypeStatusName)]
        public string Name { get; set; }
    }
}
