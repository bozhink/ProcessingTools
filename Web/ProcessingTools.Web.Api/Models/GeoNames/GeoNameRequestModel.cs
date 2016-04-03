namespace ProcessingTools.Web.Api.Models.GeoNames
{
    using System.ComponentModel.DataAnnotations;

    using Geo.Services.Data.Models.Contracts;
    using Mappings.Contracts;

    public class GeoNameRequestModel : IMapFrom<IGeoNameServiceModel>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
    }
}