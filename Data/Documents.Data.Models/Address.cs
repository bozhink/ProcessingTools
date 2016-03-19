namespace ProcessingTools.Documents.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;

    public class Address
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAddressString)]
        public string AddressString { get; set; }

        public virtual int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}