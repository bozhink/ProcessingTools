namespace ProcessingTools.Documents.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ProcessingTools.Documents.Data.Common.Constants;

    public class Journal : DocumentsAbstractEntity
    {
        private string journalId;

        private ICollection<Article> articles;

        public Journal()
        {
            this.Id = Guid.NewGuid();
            this.articles = new HashSet<Article>();
        }

        [Key]
        public Guid Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfJournalName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedJournalName)]
        public string AbbreviatedName { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfJournalId)]
        public string JournalId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.journalId))
                {
                    return this.AbbreviatedName;
                }

                return this.journalId;
            }

            set
            {
                this.journalId = value;
            }
        }

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