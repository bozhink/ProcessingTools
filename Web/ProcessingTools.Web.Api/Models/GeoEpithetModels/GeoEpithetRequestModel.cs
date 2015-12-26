namespace ProcessingTools.Web.Api.Models.GeoEpithetModels
{
    using System.ComponentModel.DataAnnotations;

    using Contracts.Mapping;
    using Geo.Services.Data.Models.Contracts;

    public class GeoEpithetRequestModel : IMapFrom<IGeoEpithet>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}