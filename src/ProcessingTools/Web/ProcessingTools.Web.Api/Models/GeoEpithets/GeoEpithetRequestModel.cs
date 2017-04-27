namespace ProcessingTools.Web.Api.Models.GeoEpithets
{
    using System.ComponentModel.DataAnnotations;

    using Geo.Services.Data.Models;
    using Mappings.Contracts;

    public class GeoEpithetRequestModel : IMapFrom<GeoEpithetServiceModel>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}