namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Geo;

    public class ContinentSynonym
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.MinimalLengthOfContinentName)]
        [MaxLength(ValidationConstants.MaximalLengthOfContinentName)]
        public string Name { get; set; }

        public virtual Continent Continent { get; set; }
    }
}
