namespace ProcessingTools.Documents.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;
    using Constants;

    public class Institution : DocumentsAbstractEntity
    {
        private ICollection<Address> addresses;
        private ICollection<Affiliation> affiliations;

        public Institution()
        {
            this.addresses = new HashSet<Address>();
            this.affiliations = new HashSet<Affiliation>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfInstitutionName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedInstitutionName)]
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