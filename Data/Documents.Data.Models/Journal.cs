namespace ProcessingTools.Documents.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Constants;

    public class Journal
    {
        private string journalId;

        private ICollection<Article> articles;

        public Journal()
        {
            this.articles = new HashSet<Article>();
        }

        [Key]
        public int Id { get; set; }

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

        public virtual int PublisherId { get; set; }

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