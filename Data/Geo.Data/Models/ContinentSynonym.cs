namespace ProcessingTools.Geo.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Geo.Data.Common.Constants;

    public class ContinentSynonym
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.MinimalLengthOfContinentName)]
        [MaxLength(ValidationConstants.MaximalLengthOfContinentName)]
        public string Name { get; set; }

        public virtual int ContinentId { get; set; }

        public virtual Continent Continent { get; set; }
    }
}
