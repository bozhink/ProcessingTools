namespace ProcessingTools.Documents.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;
    using Constants;

    public class Publisher : DocumentsAbstractEntity
    {
        private ICollection<Address> addresses;
        private ICollection<Journal> journals;

        public Publisher()
        {
            this.Id = Guid.NewGuid();
            this.addresses = new HashSet<Address>();
            this.journals = new HashSet<Journal>();
        }

        [Key]
        public Guid Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfPublisherName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedPublisherName)]
        public string AbbreviatedName { get; set; }

        public virtual ICollection<Address> Addresses
        {
            get
            {
                return this.addresses;
            }

            set
            {
                this.addresses = value;
            }
        }

        public virtual ICollection<Journal> Journals
        {
            get
            {
                return this.journals;
            }

            set
            {
                this.journals = value;
            }
        }
    }
}