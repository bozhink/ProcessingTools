namespace ProcessingTools.Documents.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Constants;

    public class Journal
    {
        private string journalId;

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

        public virtual int PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }
    }
}