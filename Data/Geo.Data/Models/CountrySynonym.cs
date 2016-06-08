namespace ProcessingTools.Geo.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Geo.Data.Common.Constants;

    public class CountrySynonym
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfCountryName)]
        public string Name { get; set; }

        public virtual int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
