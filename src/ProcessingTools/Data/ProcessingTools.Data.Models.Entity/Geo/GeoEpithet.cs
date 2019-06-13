namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    public class GeoEpithet : BaseModel, INamedIntegerIdentified, IDataModel
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfGeoEpithetName)]
        [MaxLength(ValidationConstants.MaximalLengthOfGeoEpithetName)]
        public string Name { get; set; }
    }
}
