namespace ProcessingTools.Journals.Data.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using ProcessingTools.Common.Models;
    using ProcessingTools.Journals.Data.Common.Constants;
    using ProcessingTools.Journals.Data.Common.Contracts.Models;

    public class Publisher : ModelWithUserInformation, IPublisher
    {
        private ICollection<Address> addresses;

        public Publisher()
            : base()
        {
            this.Id = Guid.NewGuid();
            this.addresses = new HashSet<Address>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedPublisherName)]
        public string AbbreviatedName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfPublisherName)]
        public string Name { get; set; }

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

        [NotMapped]
        ICollection<IAddress> IAddressable.Addresses => this.Addresses.ToList<IAddress>();
    }
}
