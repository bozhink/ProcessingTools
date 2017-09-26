namespace ProcessingTools.Documents.Data.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.Documents;
    using ProcessingTools.Data.Common.Entity.Models.Contracts;
    using ProcessingTools.Models.Abstractions;
    using ProcessingTools.Models.Contracts.Documents;

    public class Address : ModelWithUserInformation, IEntityWithPreJoinedFields, IAddress
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
            this.CreatedByUser = entity.CreatedByUser;
            this.ModifiedByUser = entity.ModifiedByUser;
            this.DateCreated = entity.DateCreated;
            this.DateModified = entity.DateModified;
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

        [NotMapped]
        public IEnumerable<string> PreJoinFieldNames => null;
    }
}
