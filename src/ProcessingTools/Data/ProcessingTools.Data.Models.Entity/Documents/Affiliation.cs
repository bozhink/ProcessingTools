namespace ProcessingTools.Data.Models.Entity.Documents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Documents;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Documents;

    public class Affiliation : ModelWithUserInformation, IAffiliation
    {
        private ICollection<Author> authors;

        public Affiliation()
        {
            this.Id = Guid.NewGuid();
            this.authors = new HashSet<Author>();
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAffiliationName)]
        public string Name { get; set; }

        public virtual Guid InstitutionId { get; set; }

        public virtual Institution Institution { get; set; }

        public virtual Guid AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Author> Authors
        {
            get
            {
                return this.authors;
            }

            set
            {
                this.authors = value;
            }
        }
    }
}
