namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.Geo;
    using ProcessingTools.Models.Contracts;

    public class GeoEpithet : SystemInformation, INameableIntegerIdentifiable, IDataModel
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfGeoEpithetName)]
        [MaxLength(ValidationConstants.MaximalLengthOfGeoEpithetName)]
        public string Name { get; set; }
    }
}
