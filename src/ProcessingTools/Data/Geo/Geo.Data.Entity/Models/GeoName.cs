namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Common.Constants.Data.Geo;
    using ProcessingTools.Models.Contracts;

    public class GeoName : BaseModel, INameableIntegerIdentifiable, IDataModel
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfGeoName)]
        [MaxLength(ValidationConstants.MaximalLengthOfGeoName)]
        public string Name { get; set; }
    }
}
