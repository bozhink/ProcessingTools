namespace ProcessingTools.Documents.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ProcessingTools.Common.Models;
    using ProcessingTools.Data.Common.Entity.Models.Contracts;
    using ProcessingTools.Documents.Data.Common.Constants;

    public class Address : ModelWithUserInformation, IEntityWithPreJoinedFields
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
            this.PreJoinFieldNames = new string[] { };
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAddressString)]
        public string AddressString { get; set; }

        public int? CityId { get; set; }

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
        public IEnumerable<string> PreJoinFieldNames { get; private set; }
    }
}