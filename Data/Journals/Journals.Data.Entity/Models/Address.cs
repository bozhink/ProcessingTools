namespace ProcessingTools.Journals.Data.Entity.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Models;
    using ProcessingTools.Journals.Data.Common.Constants;
    using ProcessingTools.Journals.Data.Common.Contracts.Models;

    public class Address : ModelWithUserInformation, IAddress
    {
        public Address()
            : base()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfAddressString)]
        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }
    }
}
