namespace ProcessingTools.Documents.Services.Data.Models.Publishers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    using ProcessingTools.Documents.Data.Common.Constants;

    public class PublisherAddress : IPublisherAddress
    {
        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAddressString)]
        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }
    }
}
