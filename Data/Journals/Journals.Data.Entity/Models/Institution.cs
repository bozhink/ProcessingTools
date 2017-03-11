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

    public class Institution : ModelWithUserInformation, IInstitution
    {
        private ICollection<Address> addresses;

        public Institution()
            : base()
        {
            this.Id = Guid.NewGuid();
            this.addresses = new HashSet<Address>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedInstitutionName)]
        public string AbbreviatedName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfInstitutionName)]
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
