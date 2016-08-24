namespace ProcessingTools.Documents.Services.Data.Models.Publishers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Documents.Data.Common.Constants;

    public class PublisherUpdateServiceModel
    {
        public PublisherUpdateServiceModel()
        {
            this.Addresses = new HashSet<AddressServiceModel>();
        }

        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfPublisherName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedPublisherName)]
        public string AbbreviatedName { get; set; }

        public ICollection<AddressServiceModel> Addresses { get; set; }
    }
}
