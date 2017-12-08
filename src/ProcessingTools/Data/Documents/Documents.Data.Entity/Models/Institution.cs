namespace ProcessingTools.Documents.Data.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.Documents;
    using ProcessingTools.Contracts.Models.Documents;
    using ProcessingTools.Data.Common.Entity.Models.Contracts;

    public class Institution : AddressableEntity, IEntityWithPreJoinedFields, IInstitution
    {
        private ICollection<Affiliation> affiliations;

        public Institution()
        {
            this.Id = Guid.NewGuid();
            this.affiliations = new HashSet<Affiliation>();
        }

        [Key]
        public Guid Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfInstitutionName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedInstitutionName)]
        public string AbbreviatedName { get; set; }

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
        public IEnumerable<string> PreJoinFieldNames => new string[]
        {
            nameof(this.Addresses)
        };
    }
}
