﻿namespace ProcessingTools.Data.Models.Entity.Documents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Documents;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Documents;

    public class Address : ModelWithUserInformation, IAddress
    {
        private ICollection<Publisher> publishers;
        private ICollection<Institution> institutions;
        private ICollection<Affiliation> affiliations;

        public Address()
        {
            this.Id = Guid.NewGuid();
            this.publishers = new HashSet<Publisher>();
            this.institutions = new HashSet<Institution>();
            this.affiliations = new HashSet<Affiliation>();
        }

        public Address(IAddress entity)
            : this()
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.AddressString = entity.AddressString;
            this.CountryId = entity.CountryId;
            this.CityId = entity.CityId;
            this.CreatedBy = entity.CreatedBy;
            this.ModifiedBy = entity.ModifiedBy;
            this.CreatedOn = entity.CreatedOn;
            this.ModifiedOn = entity.ModifiedOn;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfAddressString)]
        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }

        public virtual ICollection<Publisher> Publishers
        {
            get
            {
                return this.publishers;
            }

            set
            {
                this.publishers = value;
            }
        }

        public virtual ICollection<Institution> Institutions
        {
            get
            {
                return this.institutions;
            }

            set
            {
                this.institutions = value;
            }
        }

        public virtual ICollection<Affiliation> Affiliations
        {
            get
            {
                return this.affiliations;
            }

            set
            {
                this.affiliations = value;
            }
        }
    }
}