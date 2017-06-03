namespace ProcessingTools.Web.Api.Models.GeoEpithets
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Geo.Services.Data.Models;

    public class GeoEpithetRequestModel : IMapFrom<GeoEpithetServiceModel>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
