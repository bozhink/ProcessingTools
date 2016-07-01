namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Journals
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ProcessingTools.Common.Models;
    using ProcessingTools.Documents.Data.Common.Constants;

    public class JournalViewModel : ModelWithUserInformation
    {
        public JournalViewModel()
            : base()
        {
        }

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

        public PublisherViewModel Publisher { get; set; }
    }
}
