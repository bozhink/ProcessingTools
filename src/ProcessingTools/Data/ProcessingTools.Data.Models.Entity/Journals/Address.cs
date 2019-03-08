﻿namespace ProcessingTools.Data.Models.Entity.Journals
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Journals;
    using ProcessingTools.Models.Contracts.Journals;

    public class Address : IAddress
    {
        public Address()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfId)]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfAddressString)]
        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }
    }
}