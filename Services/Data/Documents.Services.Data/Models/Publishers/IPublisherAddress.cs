namespace ProcessingTools.Documents.Services.Data.Models.Publishers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ProcessingTools.Documents.Data.Common.Constants;

    public class IPublisherAddress
    {
        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAddressString)]
        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }
    }
}
