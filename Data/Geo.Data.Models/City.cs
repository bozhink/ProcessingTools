namespace ProcessingTools.Geo.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Constants;

    public class City
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfCityName)]
        public string Name { get; set; }

        public virtual int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual int? StateId { get; set; }

        public virtual State State { get; set; }
    }
}