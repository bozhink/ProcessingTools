namespace ProcessingTools.Documents.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Constants;

    public class Affiliation
    {
        private ICollection<Author> authors;

        public Affiliation()
        {
            this.authors = new HashSet<Author>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfAffiliationName)]
        public string Name { get; set; }

        public virtual int InstitutionId { get; set; }

        public virtual Institution Institution { get; set; }

        public virtual int AddressId { get; set; }

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