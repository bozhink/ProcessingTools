namespace ProcessingTools.Web.Api.Models.GeoNames
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Geo.Services.Data.Models;

    public class GeoNameRequestModel : IMapFrom<GeoNameServiceModel>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
    }
}
