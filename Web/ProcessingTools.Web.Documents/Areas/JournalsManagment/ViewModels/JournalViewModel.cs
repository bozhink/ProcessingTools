namespace ProcessingTools.Web.Documents.Areas.JournalsManagment.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ProcessingTools.Documents.Services.Data.Models;
    using ProcessingTools.Mappings.Contracts;

    public class JournalViewModel : IMapFrom<JournalServiceModel>
    {
        private string journalId;

        public JournalViewModel()
        {
            this.DateModified = DateTime.UtcNow;
            this.DateCreated = this.DateModified;
        }

        public Guid Id { get; set; }

        [MaxLength(60)]
        public string Name { get; set; }

        [MaxLength(40)]
        public string AbbreviatedName { get; set; }

        [MaxLength(40)]
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

        [MaxLength(9)]
        public string PrintIssn { get; set; }

        [MaxLength(9)]
        public string ElectronicIssn { get; set; }

        public virtual Guid PublisherId { get; set; }

        [MaxLength(128)]
        public string CreatedByUserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [MaxLength(128)]
        public string ModifiedByUserId { get; set; }
    }
}
