namespace ProcessingTools.Web.Areas.Data.Models.GeoEpithets
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    public class GeoEpithetRequestModel : IGeoEpithet
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfGeoEpithetName)]
        [MaxLength(ValidationConstants.MaximalLengthOfGeoEpithetName)]
        public string Name { get; set; }
    }
}
