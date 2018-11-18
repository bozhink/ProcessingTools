namespace ProcessingTools.Data.Models.Entity.Documents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Documents;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Documents;

    public class Journal : ModelWithUserInformation, IJournal
    {
        private ICollection<Article> articles;

        public Journal()
        {
            this.Id = Guid.NewGuid();
            this.articles = new HashSet<Article>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfJournalName)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedJournalName)]
        public string AbbreviatedName { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfJournalId)]
        public string JournalId { get; set; }

        [MaxLength(ValidationConstants.IssnLength)]
        public string PrintIssn { get; set; }

        [MaxLength(ValidationConstants.IssnLength)]
        public string ElectronicIssn { get; set; }

        public virtual Guid PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<Article> Articles
        {
            get
            {
                return this.articles;
            }

            set
            {
                this.articles = value;
            }
        }
    }
}
