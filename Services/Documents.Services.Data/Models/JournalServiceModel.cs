namespace ProcessingTools.Documents.Services.Data.Models
{
    using System;

    public class JournalServiceModel
    {
        private string journalId;

        public JournalServiceModel()
        {
            this.DateModified = DateTime.UtcNow;
            this.DateCreated = this.DateModified;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string AbbreviatedName { get; set; }

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

        public string PrintIssn { get; set; }

        public string ElectronicIssn { get; set; }

        public virtual Guid PublisherId { get; set; }

        public string CreatedByUserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string ModifiedByUserId { get; set; }
    }
}
